# UserProfile DTO, Testing, and EF Core Tracking

## How the DTO relates to `UserProfile`

`UserProfile` is the EF Core entity. It represents the data persisted in the
`UserProfiles` database table and is exposed through `MessagingApiContext`.

`UserProfileDto` is the API response contract. `UserProfileService` projects
each entity into a DTO before returning it to the controller:

```csharp
.Select(profile => new UserProfileDto
{
    Id = profile.Id,
    UserName = profile.UserName,
    Email = profile.Email,
    PhoneNumber = profile.PhoneNumber,
    Country = profile.Country
})
```

The fields currently map one-to-one, but the classes have separate purposes:

- `UserProfile` is the database/persistence model.
- `UserProfileDto` is the model returned by the API.

This separation prevents future database-only entity fields from being exposed
automatically in API responses.

## How the DTO affects tests

Tests for `UserProfileService` should seed `UserProfile` entities, call the
service, and assert that the result is a correctly mapped `UserProfileDto`.

The sample `UserProfileServiceTests.GetUserProfileByIdAsync_ReturnsMappedDto`
does this using EF Core's in-memory database provider:

```csharp
var options = new DbContextOptionsBuilder<MessagingApiContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    .Options;

await using var context = new MessagingApiContext(options);
context.UserProfiles.Add(new UserProfile { /* test data */ });
await context.SaveChangesAsync();

var service = new UserProfileService(context);
var result = await service.GetUserProfileByIdAsync(1);

Assert.IsType<UserProfileDto>(result);
```

This verifies both the service query and its entity-to-DTO projection.

## Is `UseInMemoryDatabase` a substitute for Moq?

Not generally. They solve different testing needs:

- `UseInMemoryDatabase` supplies a real EF Core `DbContext` backed by an
  in-memory test database. It is a good fit for testing EF Core queries and
  projections.
- Moq creates fake implementations of interfaces or virtual members. It is a
  good fit for dependencies such as email senders, clocks, or repositories.

Mocking `DbContext` and `DbSet<T>` directly is possible, but it is awkward for
async EF Core LINQ queries because they need an async query provider. For the
current `UserProfileService`, using the EF Core in-memory provider is simpler
and exercises the actual projection.

A Moq-based test becomes much more natural if the service depends on a
repository interface, such as `IUserProfileRepository`, instead of directly on
`MessagingApiContext`.

> Note: EF Core InMemory is not a relational SQL database and does not match
> every relational-database behavior. SQLite in-memory is a stronger option
> when tests need relational behavior such as constraints or SQL-like query
> behavior.

## What `AsNoTracking()` does

`DbContext` follows the Unit of Work pattern. Normally, EF Core tracks queried
entities, detects changes to them, and writes those changes when
`SaveChanges()` or `SaveChangesAsync()` is called.

```csharp
var profile = await context.UserProfiles.FirstAsync();
profile.Email = "new@example.com";
await context.SaveChangesAsync();
```

Adding `.AsNoTracking()` signals that a query is read-only:

```csharp
var profile = await context.UserProfiles
    .AsNoTracking()
    .FirstAsync();
```

EF Core then does not add returned entity instances to its change tracker,
does not detect later changes to them, and does not save those changes. This
reduces overhead for read-only queries.

In `UserProfileService`, the query directly projects to `UserProfileDto`, so
DTOs themselves are never tracked. `AsNoTracking()` still documents that the
query is intended only for retrieval and keeps that intent explicit if the
query is later changed to return entities.

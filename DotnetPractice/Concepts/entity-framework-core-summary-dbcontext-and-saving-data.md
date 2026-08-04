# Entity Framework Core — Summary: DbContext & Saving Data

A simple, focused overview of how EF Core works and how it saves data. No DTOs here — just entities, `DbContext`, and the save pipeline.

---

## 1. What is Entity Framework Core?

EF Core is an **ORM (Object-Relational Mapper)** for .NET. It lets you work with a database using C# classes and LINQ instead of writing raw SQL by hand.

- Your C# classes ("entities") map to database tables.
- Properties on those classes map to columns.
- EF Core translates your LINQ queries into SQL, and turns your in-memory changes back into `INSERT`/`UPDATE`/`DELETE` statements.

---

## 2. What is `DbContext`?

`DbContext` is the central class you work with in EF Core. Think of it as a **session with the database** — it's the bridge between your C# objects and the actual database.

`DbContext` is responsible for:

1. **Holding the connection** to the database.
2. **Exposing `DbSet<T>` properties** — one per entity/table you want to query or save.
3. **Tracking changes** to the entities you load or add, so it knows what to save later.
4. **Translating LINQ** into SQL when you query.
5. **Coordinating `SaveChanges()`** — turning tracked changes into actual SQL commands.

### A basic `DbContext`

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}
```

In a real ASP.NET Core app, you typically don't hardcode the connection string in `OnConfiguring`. Instead, you register the context with dependency injection in `Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
```

This registers `AppDbContext` as a **Scoped** service — meaning one instance is created per web request, then disposed at the end of that request.

### A simple entity

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

No DTO involved — you're working directly with this class both when querying and when saving.

---

## 3. The Change Tracker

When you load or add entities through a `DbContext`, EF Core keeps an internal record of them called the **change tracker**. For each tracked entity, it remembers:

- The **original values** (as loaded from the database, or "none" if it's new).
- The **current values** (as they exist right now in memory).
- The entity's **state**: `Added`, `Unchanged`, `Modified`, `Deleted`, or `Detached`.

When you call `SaveChanges()`, EF Core compares current vs. original values for every tracked entity and figures out exactly what SQL needs to run.

---

## 4. Saving Data — Create, Update, Delete

All saving in EF Core follows the same basic pattern:

1. Tell the `DbContext` what changed (add, modify, or remove an entity).
2. Call `SaveChanges()` (or `SaveChangesAsync()`).
3. EF Core generates and runs the SQL for you.

### Create (Insert)

```csharp
var customer = new Customer { Name = "Alex", Email = "alex@example.com" };

context.Customers.Add(customer);   // marks entity as "Added"
await context.SaveChangesAsync();  // runs: INSERT INTO Customers ...
```

After `SaveChangesAsync()`, `customer.Id` is automatically populated with the value the database generated.

### Read (needed before Update/Delete)

```csharp
var customer = await context.Customers
    .FirstOrDefaultAsync(c => c.Id == 1);
```

Loading an entity this way automatically starts tracking it (unless you use `.AsNoTracking()`).

### Update

```csharp
var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == 1);

customer.Email = "newemail@example.com";  // just change the property
await context.SaveChangesAsync();          // runs: UPDATE Customers SET Email = ...
```

Notice there's no explicit "mark as updated" call — because the entity is already tracked, EF Core detects the changed property on its own when `SaveChangesAsync()` runs.

### Delete

```csharp
var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == 1);

context.Customers.Remove(customer);        // marks entity as "Deleted"
await context.SaveChangesAsync();          // runs: DELETE FROM Customers ...
```

---

## 5. Putting It All Together

```csharp
// Create
var customer = new Customer { Name = "Alex", Email = "alex@example.com" };
context.Customers.Add(customer);
await context.SaveChangesAsync();

// Update
customer.Email = "alex.updated@example.com";
await context.SaveChangesAsync();

// Delete
context.Customers.Remove(customer);
await context.SaveChangesAsync();
```

Every one of these ends the same way: **change the tracked entity or its state, then call `SaveChangesAsync()`.**

---

## 6. Key Takeaways

- `DbContext` = your session with the database. It tracks entities and turns changes into SQL.
- `DbSet<T>` = your entry point for querying and saving a specific entity type.
- The **change tracker** watches loaded/added entities and figures out what changed.
- `SaveChanges()` / `SaveChangesAsync()` is the single method that actually writes to the database — for Add, Update, *and* Delete.
- You don't need DTOs to do any of this — you can query, modify, and save entities directly. DTOs become useful later, mainly for shaping what data crosses API boundaries (a separate concern from how EF Core saves data internally).

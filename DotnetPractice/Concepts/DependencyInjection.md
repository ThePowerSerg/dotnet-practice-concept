# Dependency Injection (DI)

Dependency injection is a design pattern where a class receives its dependencies (the other objects/services it needs to function) from an external source, rather than creating them itself. It's a specific technique for implementing the **Dependency Inversion Principle** (the "D" in SOLID) — depending on abstractions (interfaces) rather than concrete implementations.

## The problem it solves

```csharp
// Without DI: tightly coupled
class OrderService
{
    private readonly SqlOrderRepository repository;

    public OrderService()
    {
        repository = new SqlOrderRepository(); // hardcoded dependency
    }
}
```
Here, `OrderService` is stuck with `SqlOrderRepository` forever. You can't swap it out, mock it, or test `OrderService` without also hitting a real SQL database.

## With dependency injection

```csharp
interface IOrderRepository
{
    void Save(Order order);
}

class OrderService
{
    private readonly IOrderRepository repository;

    // dependency is "injected" via the constructor
    public OrderService(IOrderRepository repository)
    {
        this.repository = repository;
    }

    public void PlaceOrder(Order order)
    {
        repository.Save(order);
    }
}
```
Now `OrderService` doesn't know or care *how* orders get saved — SQL, in-memory, a file, an API — it just depends on the `IOrderRepository` contract. The actual implementation is supplied from outside, typically by a **DI container** (built into ASP.NET Core, for example) that wires everything together at startup.

## The three common forms of injection
- **Constructor injection** (most common) — dependencies passed into the constructor, as above.
- **Property injection** — dependencies set via a public property after construction.
- **Method injection** — dependencies passed as parameters to a specific method that needs them.

## Why this matters for testing

This is where DI really pays off. Because `OrderService` depends on an *interface* rather than a concrete class, you can substitute a **fake/mock implementation** during unit tests — no real database, no network calls, no side effects.

```csharp
class FakeOrderRepository : IOrderRepository
{
    public List<Order> SavedOrders { get; } = new();

    public void Save(Order order) => SavedOrders.Add(order);
}

[Fact]
public void PlaceOrder_SavesOrderToRepository()
{
    // Arrange
    var fakeRepo = new FakeOrderRepository();
    var service = new OrderService(fakeRepo);
    var order = new Order { Id = 1 };

    // Act
    service.PlaceOrder(order);

    // Assert
    Assert.Single(fakeRepo.SavedOrders);
}
```

This test runs in milliseconds, has no external dependencies, and is fully deterministic. Without DI, testing `OrderService` would mean either hitting a real database (slow, flaky, requires setup/teardown) or resorting to fragile workarounds.

## Key testing benefits

- **Isolation** — you can test a class's logic in complete isolation from its dependencies (databases, file systems, external APIs, email services).
- **Speed** — fake/in-memory implementations run far faster than real infrastructure.
- **Determinism** — no flaky failures from network issues or shared test data.
- **Mocking frameworks** — libraries like Moq or NSubstitute let you generate fake implementations of interfaces on the fly and verify how they were called (e.g., "was `Save()` called exactly once with this specific order?"), rather than hand-writing fakes for every test.

```csharp
var mockRepo = new Mock<IOrderRepository>();
var service = new OrderService(mockRepo.Object);

service.PlaceOrder(new Order { Id = 1 });

mockRepo.Verify(r => r.Save(It.IsAny<Order>()), Times.Once);
```

**Bottom line:** DI decouples *what* a class needs from *how* that need is fulfilled, which is exactly what makes a class testable in isolation — a cornerstone of writing unit tests that are fast, reliable, and focused on one thing at a time.
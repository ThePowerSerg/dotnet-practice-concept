Type: a type defines the blueprint for a value.

In object-oriented programming, a **type** is a classification that defines what kind of data a value holds and what operations can be performed on it. It's the blueprint that describes the shape and behavior of something — its structure (fields/properties) and its capabilities (methods).

## Key ideas

- **A type describes both data and behavior.** For example, an `int` type defines that a value is a whole number and supports operations like addition and comparison. A `Customer` class defines that an object has a `Name`, an `Email`, and behaviors like `PlaceOrder()`.
- **Every value has a type.** Whether it's a primitive (`int`, `bool`), a built-in reference type (`string`, an array), or a user-defined `class`/`struct`/`interface`/`enum`, the type determines what you can legally do with that value.
- **Types enable compile-time checking.** In statically typed languages like C#, the compiler uses types to catch errors early — e.g., you can't assign a `string` to an `int` variable, or call a method that doesn't exist on that type.

## Categories of types (in C#, as an example)

- **Class** — a reference type; a blueprint for creating objects with identity, state, and behavior, supporting inheritance.
- **Struct** — a value type; typically small, lightweight data structures.
- **Interface** — a contract describing behavior without implementation or state.
- **Enum** — a type representing a fixed set of named constants.
- **Delegate** — a type that represents a reference to a method (used for callbacks/events).

## Type vs. Class vs. Object

These three terms are related but distinct:

- **Type** is the general/umbrella term — any classification of data (class, struct, interface, enum, primitive, etc.)
- **Class** is one specific _kind_ of type — a blueprint for objects.
- **Object** is an _instance_ of a type (usually a class) created at runtime, with its own actual data in memory.

So you could say: `Customer` is a **class**, which is a kind of **type**, and `var c = new Customer();` creates an **object** of that type.

## Interface

An interface defines a contract — a set of method/property signatures with no implementation and no state of its own. Any class or struct that implements it must provide the actual implementation. Interfaces answer the question "what can this type do?" rather than "what is this type?"

```csharp
interface IShape
{
    double GetArea();
}

class Circle : IShape
{
    public double Radius { get; set; }
    public double GetArea() => Math.PI * Radius * Radius;
}
```

## How it differs from an abstract class

|                             | Interface                                               | Abstract Class                                                     |
| --------------------------- | ------------------------------------------------------- | ------------------------------------------------------------------ |
| State (fields)              | No — cannot hold instance fields                        | Yes — can hold and initialize fields                               |
| Constructors                | No                                                      | Yes                                                                |
| Multiple inheritance        | Yes — a class can implement many interfaces             | No — a class can only inherit from one base class                  |
| Method implementations      | Optional default methods (C# 8+), but historically none | Mix of fully implemented methods and abstract (unimplemented) ones |
| Access modifiers on members | Implicitly public                                       | Can be public, protected, private, etc.                            |
| Purpose                     | Defines a capability/contract ("can do X")              | Defines a shared identity/base with common behavior ("is a X")     |

**Rule of thumb:**

- Use an **interface** when unrelated types need to share a common capability (e.g., `IComparable`, `IDisposable`) — especially if a class already needs to inherit from something else and still needs the contract.
- Use an **abstract class** when you have a family of closely related types that share common state and some common implementation, and you want to enforce a shared base while leaving some behavior for subclasses to fill in.

A practical signal: if you find yourself wanting to give some subclasses a default implementation and shared fields, reach for an abstract class. If you just need to say "this type supports operation X," reach for an interface.


The four pillars of object-oriented programming are the core principles that guide how OOP languages structure code around objects. Here's each one:

## 1. Encapsulation
Bundling data (fields) and the methods that operate on that data together into a single unit (a class), while restricting direct access to some of the object's internal state. This is typically done using access modifiers (`private`, `public`, `protected`) and exposing controlled access through properties or methods.

```csharp
class BankAccount
{
    private decimal balance; // hidden from outside

    public void Deposit(decimal amount)
    {
        if (amount > 0) balance += amount; // controlled access
    }

    public decimal GetBalance() => balance;
}
```
**Why it matters:** protects an object's internal state from being put into an invalid or inconsistent condition by outside code.

## 2. Abstraction
Hiding complex implementation details and exposing only the essential features of an object. It focuses on *what* an object does rather than *how* it does it. Interfaces and abstract classes are common tools for this.

```csharp
interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}
```
A caller just calls `ProcessPayment()` without needing to know whether it talks to Stripe, PayPal, or a bank API under the hood.

## 3. Inheritance
Allowing a new class (derived/child class) to acquire the fields and methods of an existing class (base/parent class), enabling code reuse and the creation of hierarchical relationships ("is-a" relationships).

```csharp
class Animal
{
    public void Eat() => Console.WriteLine("Eating...");
}

class Dog : Animal
{
    public void Bark() => Console.WriteLine("Barking...");
}
```
A `Dog` automatically has `Eat()` and adds its own `Bark()`.

## 4. Polymorphism
Allowing objects of different types to be treated through a common interface, with each type providing its own specific behavior for shared operations. This usually comes in two forms:
- **Runtime (dynamic) polymorphism** — via method overriding (`virtual`/`override`), where the correct method is chosen based on the actual object type at runtime.
- **Compile-time (static) polymorphism** — via method overloading, where multiple methods share a name but differ in parameters.

```csharp
class Animal
{
    public virtual string MakeSound() => "Some sound";
}

class Dog : Animal
{
    public override string MakeSound() => "Bark";
}

Animal a = new Dog();
Console.WriteLine(a.MakeSound()); // "Bark" — resolved at runtime
```

**How they work together:** encapsulation protects state, abstraction hides complexity, inheritance promotes reuse, and polymorphism allows flexible, extensible code that can work with new types without modification — together forming the foundation for building maintainable, modular software.

**Acronyms:**

Computer programming relies on a vast array of acronyms and initialisms to describe languages, architectures, methodologies, and design principles. [1, 2]  
Here is a breakdown of the most essential programming acronyms every developer should know, categorized by function: 
🛠️ Core Concepts & Architecture 

• API: Application Programming Interface — A set of rules allowing different software applications to communicate. 
• DOM: Document Object Model — A programming interface for web documents that structures elements so they can be modified. 
• GUI: Graphical User Interface — A visual way for users to interact with a computer. 
• IDE: Integrated Development Environment — A software suite that consolidates basic tools for writing and testing software. [2, 3, 4, 5, 6]  

💻 Languages & Data 

• CSS: Cascading Style Sheets — A style sheet language used for describing the presentation of a document. 
• HTML: HyperText Markup Language — The standard markup language for creating web pages. 
• JSON: JavaScript Object Notation — A lightweight data-interchange format. 
• SQL: Structured Query Language — A domain-specific language used in programming and designed for managing data held in a relational database management system. [2, 3, 4, 5, 7]  

⚙️ Methodologies & Best Practices 

• DRY: Don't Repeat Yourself — A principle of software development aimed at reducing repetition of information. 
• KISS: Keep It Simple, Stupid — A design principle emphasizing that systems should work best if they are kept simple rather than made complex. 
• OOP: Object-Oriented Programming — A programming paradigm based on the concept of "objects". 
• SOLID: Single responsibility, Open-closed, Liskov substitution, Interface segregation, Dependency inversion — Five design principles intended to make software designs more understandable, flexible, and maintainable. 
• YAGNI: You Aren't Gonna Need It — A principle of extreme programming that states a capability should not be added until it is deemed necessary. [1, 11, 12, 13]  

🗄️ Databases & Data Handling 

• ACID: Atomicity, Consistency, Isolation, Durability — A set of properties of database transactions ensuring data validity. 
• CRUD: Create, Read, Update, Delete — The four major functions implemented in database applications. [3, 14, 15]  

If you're studying for an exam or trying to decipher a specific codebase, tell me the subfield or language you are focusing on (e.g., Web Development, C++, or Databases), and I can give you a custom list tailored to your needs. 


# SOLID Principles

SOLID is an acronym for five design principles that help make object-oriented code more maintainable, flexible, and easier to extend over time. Each addresses a common way software tends to become fragile or hard to change.

## S — Single Responsibility Principle
A class should have only one reason to change — it should do one thing and do it well.

```csharp
// Violates SRP: handles both business logic AND file I/O
class Report
{
    public string GenerateContent() { /* ... */ }
    public void SaveToFile(string path) { /* ... */ }
}

// Follows SRP: responsibilities split
class Report
{
    public string GenerateContent() { /* ... */ }
}

class ReportSaver
{
    public void SaveToFile(Report report, string path) { /* ... */ }
}
```
**Why:** if a class does multiple unrelated things, a change to one responsibility risks breaking the others.

## O — Open/Closed Principle
Classes should be open for extension but closed for modification — you should be able to add new behavior without changing existing, tested code.

```csharp
interface IDiscount
{
    decimal Apply(decimal price);
}

class NoDiscount : IDiscount
{
    public decimal Apply(decimal price) => price;
}

class SeasonalDiscount : IDiscount
{
    public decimal Apply(decimal price) => price * 0.9m;
}
```
Adding a new discount type means creating a new class, not editing an existing `switch` statement full of discount logic.

## L — Liskov Substitution Principle
Objects of a derived class should be substitutable for objects of the base class without breaking the program's correctness — a subclass shouldn't change the expected behavior of the parent.

```csharp
// Classic violation: Square "is-a" Rectangle mathematically,
// but breaks behavior if Width/Height are meant to vary independently
class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
}

class Square : Rectangle
{
    public override int Width { set { base.Width = base.Height = value; } }
    public override int Height { set { base.Width = base.Height = value; } }
}
```
Code expecting a `Rectangle` to let width/height vary independently would break if handed a `Square`.

## I — Interface Segregation Principle
Clients shouldn't be forced to depend on methods they don't use — prefer several small, specific interfaces over one large, general-purpose one.

```csharp
// Violates ISP: forces all workers to implement Eat(), even robots
interface IWorker
{
    void Work();
    void Eat();
}

// Follows ISP: split into focused interfaces
interface IWorkable
{
    void Work();
}

interface IFeedable
{
    void Eat();
}
```

## D — Dependency Inversion Principle
High-level modules shouldn't depend on low-level modules directly — both should depend on abstractions (interfaces). This also enables dependency injection.

```csharp
// Violates DIP: tightly coupled to a concrete class
class OrderService
{
    private SqlOrderRepository repository = new SqlOrderRepository();
}

// Follows DIP: depends on an abstraction, injected in
class OrderService
{
    private readonly IOrderRepository repository;

    public OrderService(IOrderRepository repository)
    {
        this.repository = repository;
    }
}
```
Now `OrderService` can work with any `IOrderRepository` implementation (SQL, in-memory, mock for testing) without changing its own code.

---

**Why SOLID matters overall:** these principles work together to reduce coupling, increase cohesion, and make code easier to test, extend, and maintain as requirements evolve — especially valuable in larger codebases where uncontrolled dependencies and rigid structures become expensive to change.


# ACID Properties

ACID isn't a programming methodology exactly — it's a set of four properties that guarantee reliable processing of database transactions. The acronym describes what a transaction management system must guarantee so that data stays accurate and consistent, even in the face of errors, power failures, or concurrent access.

## A — Atomicity
A transaction is treated as a single, indivisible unit of work — it either completes entirely, or it doesn't happen at all. If any part of the transaction fails, the entire transaction is rolled back, leaving the database as if it never started.

```sql
BEGIN TRANSACTION;
    UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1; -- withdraw
    UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2; -- deposit
COMMIT;
```
If the second `UPDATE` fails, the first one is rolled back too — you never end up with money deducted from one account without it appearing in the other.

## C — Consistency
A transaction must bring the database from one valid state to another, respecting all defined rules — constraints, cascades, triggers, and data integrity rules. A transaction that would violate these rules (e.g., a foreign key pointing to a non-existent row, or a negative balance where that's disallowed) is rejected.

## I — Isolation
Concurrent transactions shouldn't interfere with each other — the intermediate state of one transaction should be invisible to other transactions until it commits. This prevents problems like:
- **Dirty reads** — reading uncommitted data from another transaction
- **Non-repeatable reads** — getting different results reading the same row twice within a transaction
- **Phantom reads** — a query returning different rows on re-execution because another transaction inserted/deleted matching rows

Isolation is usually configurable via **isolation levels** (e.g., in SQL Server: `Read Uncommitted`, `Read Committed`, `Repeatable Read`, `Serializable`), trading off consistency guarantees against performance/concurrency.

## D — Durability
Once a transaction is committed, its changes are permanent — they survive system crashes, power failures, or restarts. This is typically achieved through write-ahead logging, where changes are recorded to a durable log before being confirmed to the client.

---

## Why ACID matters
These four properties together guarantee that a database remains reliable and predictable even under failures or heavy concurrent load — critical for systems like banking, e-commerce, or anything where partial or corrupted transactions could cause real damage (e.g., money vanishing between accounts, or duplicate orders being placed).

**Note:** ACID is most associated with relational databases (SQL Server, PostgreSQL, MySQL). Many NoSQL databases intentionally relax some ACID guarantees (favoring the **BASE** model — Basically Available, Soft state, Eventual consistency) in exchange for higher availability and scalability across distributed systems.

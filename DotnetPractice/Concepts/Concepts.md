Type: a type defines the blueprint for a value. 


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

| | Interface | Abstract Class |
|---|---|---|
| State (fields) | No — cannot hold instance fields | Yes — can hold and initialize fields |
| Constructors | No | Yes |
| Multiple inheritance | Yes — a class can implement many interfaces | No — a class can only inherit from one base class |
| Method implementations | Optional default methods (C# 8+), but historically none | Mix of fully implemented methods and abstract (unimplemented) ones |
| Access modifiers on members | Implicitly public | Can be public, protected, private, etc. |
| Purpose | Defines a capability/contract ("can do X") | Defines a shared identity/base with common behavior ("is a X") |

**Rule of thumb:**
- Use an **interface** when unrelated types need to share a common capability (e.g., `IComparable`, `IDisposable`) — especially if a class already needs to inherit from something else and still needs the contract.
- Use an **abstract class** when you have a family of closely related types that share common state and some common implementation, and you want to enforce a shared base while leaving some behavior for subclasses to fill in.

A practical signal: if you find yourself wanting to give some subclasses a default implementation and shared fields, reach for an abstract class. If you just need to say "this type supports operation X," reach for an interface.


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
- **Class** is one specific *kind* of type — a blueprint for objects.
- **Object** is an *instance* of a type (usually a class) created at runtime, with its own actual data in memory.

So you could say: `Customer` is a **class**, which is a kind of **type**, and `var c = new Customer();` creates an **object** of that type.
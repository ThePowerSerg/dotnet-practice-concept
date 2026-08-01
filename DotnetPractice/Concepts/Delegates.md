# Delegates in C#

A **delegate** is a type-safe function pointer — an object that holds a reference to a method (or multiple methods) matching a specific signature, which can then be invoked, passed as a parameter, or combined with other delegates, all without knowing at compile time exactly which method will run.

## 1. Declaring and Using a Basic Delegate

```csharp
// Declare a delegate type — describes a signature: takes an int, returns bool
public delegate bool NumberPredicate(int number);

class Program
{
    static bool IsEven(int n) => n % 2 == 0;
    static bool IsPositive(int n) => n > 0;

    static void Main()
    {
        // Assign a method matching the signature to the delegate
        NumberPredicate predicate = IsEven;

        Console.WriteLine(predicate(4));  // True
        Console.WriteLine(predicate(7));  // False

        // Reassign to a different matching method at runtime
        predicate = IsPositive;
        Console.WriteLine(predicate(-3)); // False
    }
}
```

The delegate `NumberPredicate` doesn't care *which* method it holds — only that the method's signature (`int → bool`) matches. This is the essence of "type-safe function pointer."

## 2. Why Delegates Matter: Passing Behavior as Data

Delegates let you pass a *method* as an argument to another method — enabling patterns like callbacks and strategy-style logic without writing a class/interface for every variation.

```csharp
public static void ProcessNumbers(int[] numbers, NumberPredicate condition)
{
    foreach (var n in numbers)
    {
        if (condition(n))
            Console.WriteLine($"{n} matches the condition");
    }
}

// Usage
ProcessNumbers(new[] { 1, 2, 3, 4, 5, 6 }, IsEven);
```

## 3. Multicast Delegates

Delegates can reference *more than one* method — invoking all of them in sequence when called. This is done with `+=` and `-=`.

```csharp
public delegate void Notify(string message);

static void LogToConsole(string msg) => Console.WriteLine($"Console: {msg}");
static void LogToFile(string msg) => Console.WriteLine($"File: {msg}"); // simplified

static void Main()
{
    Notify notify = LogToConsole;
    notify += LogToFile;      // combine — both will run
    notify -= LogToConsole;   // remove one

    notify("System started"); // only LogToFile runs now
}
```

For delegates with return values, invoking a multicast delegate directly only gives you the return value of the *last* method invoked — the earlier ones' return values are discarded. This is one reason multicast delegates are typically used with `void`-returning methods (e.g., events).

## 4. Built-in Generic Delegate Types

You rarely need to declare a custom `delegate` type in modern C# — the BCL provides generic ones that cover almost every case:

```csharp
Func<int, int, int> add = (a, b) => a + b;         // takes params, returns a value
Action<string> print = msg => Console.WriteLine(msg); // takes params, returns void
Predicate<int> isEven = n => n % 2 == 0;            // takes one param, returns bool
```

- **`Action<T1, T2, ...>`** — represents a method with no return value (`void`).
- **`Func<T1, T2, ..., TResult>`** — represents a method with a return value (the last type parameter is always the return type).
- **`Predicate<T>`** — a special case of `Func<T, bool>`, commonly used in collection filtering (`List<T>.FindAll`, etc.).

```csharp
List<int> numbers = new() { 1, 2, 3, 4, 5, 6 };

// Func used directly with LINQ
Func<int, bool> isEvenFunc = n => n % 2 == 0;
var evens = numbers.Where(isEvenFunc).ToList();

// Action used with List<T>.ForEach
Action<int> printNumber = n => Console.WriteLine(n);
numbers.ForEach(printNumber);
```

## 5. Delegates and Lambda Expressions

Lambdas are just a concise syntax for creating a method that gets assigned to a delegate. These are all equivalent:

```csharp
// Named method
Func<int, int> square1 = Square;
static int Square(int x) => x * x;

// Anonymous method (older syntax)
Func<int, int> square2 = delegate (int x) { return x * x; };

// Lambda expression (modern, most common)
Func<int, int> square3 = x => x * x;
```

## 6. Delegates and Events

**Events** are built on top of delegates. An event wraps a delegate but restricts external code so it can only subscribe (`+=`) or unsubscribe (`-=`) — not invoke the delegate directly or clear other subscribers. This enforces a clean publisher/subscriber pattern.

```csharp
public class Button
{
    // The delegate type describing the event handler's signature
    public event Action<string> Clicked;

    public void SimulateClick()
    {
        // Only the class itself can invoke the event
        Clicked?.Invoke("Button was clicked!");
    }
}

class Program
{
    static void Main()
    {
        var button = new Button();

        // Subscribers can only add/remove handlers, not invoke Clicked directly
        button.Clicked += message => Console.WriteLine($"Handler 1: {message}");
        button.Clicked += message => Console.WriteLine($"Handler 2: {message}");

        button.SimulateClick();
        // Output:
        // Handler 1: Button was clicked!
        // Handler 2: Button was clicked!
    }
}
```

Notice `Clicked?.Invoke(...)` — the null-conditional operator guards against calling `Invoke` when there are no subscribers (an unsubscribed event is `null` by default), avoiding a `NullReferenceException`.

## 7. Delegates Are Reference Types

Since delegates are reference types (they inherit from `System.Delegate`/`System.MulticastDelegate` under the hood), assigning one delegate variable to another copies the reference, and combining delegates with `+=` actually creates a *new* delegate instance internally representing the combined invocation list — the original delegate object itself is immutable once created.

---

## Summary Table

| Concept | Purpose |
|---|---|
| `delegate` keyword | Declares a custom type-safe function pointer |
| `Func<...>` | Built-in delegate for methods that return a value |
| `Action<...>` | Built-in delegate for methods that return `void` |
| `Predicate<T>` | Built-in delegate for `T → bool` (used heavily in collection filtering) |
| Multicast (`+=`/`-=`) | Combine/remove multiple method references on one delegate |
| `event` | A restricted delegate exposing only subscribe/unsubscribe to outside code |

**Where you'll see delegates in real code:** LINQ (`Where`, `Select`, `OrderBy` all take `Func`/`Predicate` delegates), event handling (button clicks, async completions), callback patterns (e.g., a method that reports progress via an `Action<int>` callback), and dependency injection scenarios where a factory method is registered as a delegate (`Func<IServiceProvider, T>`).


Correct — `Func`, `Action`, and `Predicate` are all built-in generic delegate types provided by the .NET base class library (in `System`), so you don't have to declare your own custom `delegate` type for most common scenarios. A quick recap of what distinguishes them:

| Delegate | Returns a value? | Example signature |
|---|---|---|
| `Action<T1, T2, ...>` | No (`void`) | `Action<string> print` |
| `Func<T1, T2, ..., TResult>` | Yes (last type param is the return type) | `Func<int, int, int> add` |
| `Predicate<T>` | Yes, but always `bool` | `Predicate<int> isEven` |

A couple of nuances worth knowing:

- **`Predicate<T>` is functionally identical to `Func<T, bool>`** — they're interchangeable in terms of what they can hold, but `Predicate<T>` predates the generic `Func` family and is mostly seen in older collection APIs like `List<T>.Find`, `List<T>.FindAll`, and `List<T>.RemoveAll`.
- Both `Action` and `Func` come in multiple generic overloads — `Action` supports 0 to 16 parameters (`Action`, `Action<T>`, `Action<T1,T2>`, ... up to `Action<T1,...,T16>`), and `Func` supports 0 to 16 input parameters plus the return type.
- You *can* still declare a custom `delegate` type (like the `NumberPredicate` example earlier) when you want a more descriptive, domain-specific name for the signature — but in practice, most C# code reaches for `Func`/`Action`/`Predicate` rather than defining new delegate types.

# Another Example of `Func<>` in Action

Let's use a practical scenario: validating and transforming user input with different `Func` delegates.

## Basic Example — Temperature Conversion

```csharp
using System;

class Program
{
    static void Main()
    {
        // Func<double, double> takes one double parameter, returns a double
        Func<double, double> celsiusToFahrenheit = celsius => (celsius * 9 / 5) + 32;
        Func<double, double> fahrenheitToCelsius = fahrenheit => (fahrenheit - 32) * 5 / 9;

        double tempC = 25;
        double tempF = celsiusToFahrenheit(tempC);
        Console.WriteLine($"{tempC}°C = {tempF}°F"); // 25°C = 77°F

        double tempF2 = 98.6;
        double tempC2 = fahrenheitToCelsius(tempF2);
        Console.WriteLine($"{tempF2}°F = {tempC2}°C"); // 98.6°F = 37°C
    }
}
```

## A More Involved Example — Passing `Func` as a Parameter

This is where `Func` really shines: passing behavior into a method so the method stays generic and reusable.

```csharp
using System;
using System.Collections.Generic;

class Program
{
    // This method accepts a Func<int, int, int> — any two-int-in, one-int-out operation
    static int ApplyOperation(int a, int b, Func<int, int, int> operation)
    {
        return operation(a, b);
    }

    static void Main()
    {
        int x = 10, y = 5;

        int sum = ApplyOperation(x, y, (a, b) => a + b);
        int difference = ApplyOperation(x, y, (a, b) => a - b);
        int product = ApplyOperation(x, y, (a, b) => a * b);

        Console.WriteLine($"Sum: {sum}");             // Sum: 15
        Console.WriteLine($"Difference: {difference}"); // Difference: 5
        Console.WriteLine($"Product: {product}");       // Product: 50
    }
}
```

Here, `ApplyOperation` doesn't know or care *what* math it's doing — it just knows it will receive a `Func<int, int, int>` (two `int` inputs, one `int` output) and calls whatever logic is handed to it. You could pass in addition, subtraction, multiplication, or any other two-number operation without ever modifying `ApplyOperation` itself.

## `Func` with LINQ (very common real-world usage)

```csharp
List<string> names = new() { "Alice", "Bob", "Charlie", "Dave", "Eve" };

// Func<string, bool> — takes a string, returns a bool
Func<string, bool> startsWithVowel = name => "AEIOU".Contains(name[0]);

var namesStartingWithVowel = names.Where(startsWithVowel).ToList();

foreach (var name in namesStartingWithVowel)
    Console.WriteLine(name); // Alice, Eve
```

`Where` internally expects a `Func<TSource, bool>` — this is exactly why lambdas plug so naturally into LINQ methods: they're just being assigned to a `Func` delegate parameter under the hood.

---

**Key takeaway:** `Func<T1, T2, ..., TResult>` is just a generic delegate where the *last* type parameter is always the return type, and everything before it is an input parameter. This flexibility is what makes it so common for passing calculations, transformations, and filters around as values.

Exactly right — you've got it.

`Func<double, double>` means:
- **Input:** one `double` parameter
- **Output:** a `double` return value

The general rule for `Func<>` is: **every type parameter except the last one is an input, and the last type parameter is always the return type.**

A few more examples to reinforce the pattern:

```csharp
Func<double, double> square = x => x * x;
// 1 double in, 1 double out

Func<int, int, int> add = (a, b) => a + b;
// 2 ints in, 1 int out

Func<string, int, bool> hasMinLength = (text, minLen) => text.Length >= minLen;
// string + int in, bool out

Func<int> getRandomNumber = () => new Random().Next();
// no parameters, just 1 int out
```

So counting the type parameters: if there are **N** type parameters total, the first **N-1** are inputs, and the **last one** is always the return type. With only one type parameter (like `Func<int>`), that means zero inputs and just a return value.


Delegates: Function pointers — you assign a method to a delegate as long as the method's signature and return type match. Delegates are multicast (can hold multiple methods) and can be invoked directly by anyone holding a reference. .NET provides built-in delegates (Func, Action, Predicate) used heavily in LINQ.
Events: Delegates with restricted access — outside code can only subscribe/unsubscribe (+=/-=), never invoke or overwrite directly; only the declaring class can raise the event. Most commonly seen (as a consumer) in desktop UI frameworks like WinForms/WPF.
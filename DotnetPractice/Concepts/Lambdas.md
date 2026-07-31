# Lambda Expressions in C# — Detailed Explanation

## What Is a Lambda Expression?

A **lambda expression** is a concise, inline way to write an anonymous method — a function without a name that can be assigned to a delegate, passed as an argument, or stored in a variable. Lambdas are essentially syntactic sugar that let you write the *behavior* you want directly at the point where it's needed, instead of declaring a separate named method elsewhere.

## Basic Syntax

```csharp
(parameters) => expression_or_statement_block
```

The `=>` is read as "goes to." Everything to the left is the input; everything to the right is what happens with it.

```csharp
Func<int, int> square = x => x * x;
Console.WriteLine(square(5)); // 25
```

## 1. Evolution — From Named Method to Lambda

It helps to see the progression that lambdas grew out of:

```csharp
// 1. Named method
static bool IsEven(int n) => n % 2 == 0;
Func<int, bool> check1 = IsEven;

// 2. Anonymous method (C# 2.0 — pre-lambda)
Func<int, bool> check2 = delegate (int n) { return n % 2 == 0; };

// 3. Lambda expression (C# 3.0+, modern standard)
Func<int, bool> check3 = n => n % 2 == 0;
```

All three are functionally identical — the lambda is just the most concise form.

## 2. Lambda Syntax Variations

### No parameters
```csharp
Func<string> greet = () => "Hello!";
Console.WriteLine(greet()); // Hello!
```

### One parameter (parentheses optional)
```csharp
Func<int, int> square = x => x * x;
Func<int, int> squareAlt = (x) => x * x; // equivalent
```

### Multiple parameters (parentheses required)
```csharp
Func<int, int, int> add = (a, b) => a + b;
```

### Expression body vs. statement body
```csharp
// Expression lambda — single expression, implicit return
Func<int, int> square = x => x * x;

// Statement lambda — a block with braces, explicit return needed
Func<int, int> squareVerbose = x =>
{
    int result = x * x;
    Console.WriteLine($"Squaring {x}");
    return result;
};
```

### Explicit parameter types (usually unnecessary, but sometimes required)
```csharp
Func<int, int> square = (int x) => x * x; // explicit type
```
The compiler can almost always infer parameter types from the delegate's signature, so this is rarely needed — but it can help resolve ambiguity in some overload scenarios.

### No parameters with a statement body
```csharp
Action logStartup = () =>
{
    Console.WriteLine("App starting...");
    Console.WriteLine("Loading config...");
};
logStartup();
```

## 3. Lambdas Are Just Values Assigned to Delegates

This is the core mental model: a lambda has no meaning on its own — it only exists because it's being assigned to (or passed as) a delegate type, which defines its expected signature.

```csharp
public delegate int Operation(int a, int b);

Operation multiply = (a, b) => a * b;
Console.WriteLine(multiply(3, 4)); // 12
```

The compiler looks at the target delegate type (`Operation`, or `Func<int,int,int>`, etc.) to figure out what types `a` and `b` should be — this is called **type inference from the target type**.

## 4. Lambdas as Method Arguments (Most Common Real-World Use)

This is where lambdas really shine — passing behavior directly into a method call without declaring a separate named method.

```csharp
List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8 };

// LINQ methods accept Func/Predicate delegates — lambdas plug right in
var evens = numbers.Where(n => n % 2 == 0).ToList();
var doubled = numbers.Select(n => n * 2).ToList();
var total = numbers.Aggregate((sum, n) => sum + n);
var firstBig = numbers.FirstOrDefault(n => n > 5);

Console.WriteLine(string.Join(", ", evens));   // 2, 4, 6, 8
Console.WriteLine(string.Join(", ", doubled)); // 2, 4, 6, 8, 10, 12, 14, 16
Console.WriteLine(total);                       // 36
Console.WriteLine(firstBig);                    // 6
```

## 5. Closures — Lambdas Capturing Outer Variables

A lambda can "capture" variables from the surrounding scope — this is called a **closure**. The lambda keeps a reference to the variable, not a snapshot of its value at creation time.

```csharp
int threshold = 5;
Func<int, bool> isAboveThreshold = n => n > threshold;

Console.WriteLine(isAboveThreshold(10)); // True
Console.WriteLine(isAboveThreshold(3));  // False

threshold = 8; // change the captured variable
Console.WriteLine(isAboveThreshold(6));  // False — now uses the updated threshold value!
```

### The classic closure pitfall — capturing a loop variable

```csharp
var actions = new List<Action>();

for (int i = 0; i < 3; i++)
{
    actions.Add(() => Console.WriteLine(i));
}

foreach (var action in actions)
    action();
```

In modern C# (C# 5+), the `foreach`-style capture issue with `for` loops using `int i` was fixed for `foreach` loops specifically, but a `for` loop with a shared `i` variable can still behave unexpectedly depending on the exact C# version and loop type. The safe habit is to capture a local copy inside the loop if you want each lambda to keep its own value:

```csharp
for (int i = 0; i < 3; i++)
{
    int captured = i; // create a new variable each iteration
    actions.Add(() => Console.WriteLine(captured));
}
```

This guarantees each lambda closes over its *own* independent `captured` variable rather than a shared one.

## 6. Lambdas with `Action`, `Func`, and `Predicate`

```csharp
// Action — no return value
Action<string> log = message => Console.WriteLine($"[LOG] {message}");
log("Application started");

// Func — has a return value
Func<int, int, int> add = (a, b) => a + b;
Console.WriteLine(add(3, 4)); // 7

// Predicate — always returns bool
Predicate<string> isEmpty = s => string.IsNullOrEmpty(s);
Console.WriteLine(isEmpty(""));    // True
Console.WriteLine(isEmpty("hi"));  // False
```

## 7. Lambdas and Expression Trees

Lambdas serve two very different purposes depending on what they're assigned to:

```csharp
// Assigned to a delegate — compiles to executable IL, runs in-process
Func<int, bool> isEvenDelegate = n => n % 2 == 0;

// Assigned to an Expression<T> — compiles to a data structure representing the code
Expression<Func<int, bool>> isEvenExpression = n => n % 2 == 0;
```

When assigned to `Expression<Func<T>>`, the C# compiler doesn't generate executable code directly — instead it builds a tree of objects describing the lambda's logic (parameters, operators, method calls). This is what makes Entity Framework possible: EF Core takes the expression tree from a lambda like `.Where(x => x.Age > 18)` and translates it into SQL, rather than executing the C# in memory.

## 8. Async Lambdas

Lambdas can be marked `async` just like regular methods, useful for inline asynchronous callbacks.

```csharp
Func<Task> loadData = async () =>
{
    await Task.Delay(1000);
    Console.WriteLine("Data loaded");
};

await loadData();
```

## 9. Lambdas vs. Local Functions

C# also has **local functions**, which look similar but are a distinct feature — worth knowing the difference:

```csharp
// Lambda — assigned to a delegate variable
Func<int, int> squareLambda = x => x * x;

// Local function — a named method defined inside another method
int SquareLocal(int x) => x * x;
```

Local functions can be more efficient (they can avoid delegate allocation entirely in simple cases) and support recursion more naturally, while lambdas are more flexible for being passed around as values (e.g., into LINQ methods or stored in fields).

---

## Summary

| Concept | Explanation |
|---|---|
| Lambda syntax | `(parameters) => expression_or_block` |
| Purpose | Concise inline anonymous method, assigned to a delegate |
| Type inference | Parameter types inferred from the target delegate signature |
| Closures | Lambdas can capture and reference outer variables, including later changes to them |
| Common use | LINQ (`Where`, `Select`, etc.), event handlers, callbacks, `Func`/`Action`/`Predicate` assignments |
| Expression trees | Lambdas assigned to `Expression<T>` become inspectable code-as-data (used by EF Core) |

**Bottom line:** a lambda is just a shorthand way of writing a method body inline, wherever a delegate type is expected — its real power comes from combining that conciseness with closures (capturing surrounding context) and being passed seamlessly into methods like LINQ operators.
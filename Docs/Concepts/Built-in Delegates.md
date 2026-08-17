## Func, Action, Predicate — simple definitions

**Func** — takes input(s), **returns a value**. Last type parameter = return type.
```csharp
Func<int, int> square = x => x * x;
Console.WriteLine(square(5)); // 25
```

**Action** — takes input(s), **returns nothing** (void).
```csharp
Action<string> print = msg => Console.WriteLine(msg);
print("Hello"); // Hello
```

**Predicate** — takes **one input**, always **returns bool**. Basically `Func<T, bool>` with a friendlier name.
```csharp
Predicate<int> isEven = x => x % 2 == 0;
Console.WriteLine(isEven(4)); // True
```

### Quick comparison

| Delegate | Returns | Simplest way to remember |
|---|---|---|
| `Func<T, TResult>` | a value | "give me something back" |
| `Action<T>` | nothing | "just do something" |
| `Predicate<T>` | `bool` | "yes or no answer" |

**One-sentence summary:** `Func` returns a value, `Action` returns nothing, `Predicate` returns true/false — all three are just pre-built delegate shapes so you don't have to define your own.
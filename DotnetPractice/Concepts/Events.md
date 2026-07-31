# Events in C# — Detailed Explanation

## What Is an Event?

An **event** is a member of a class that allows the class to notify other code when something happens — without the class needing to know who (if anyone) is listening. Under the hood, an event is built on top of a delegate, but with important restrictions layered on top that make it safer for the publisher/subscriber pattern.

Think of it like a radio broadcast: the broadcaster (publisher) sends out a signal without knowing who's tuned in. Listeners (subscribers) can tune in or tune out at any time, but they can't hijack the broadcast tower and start broadcasting themselves.

## The Problem Events Solve

Let's start with a plain delegate — no `event` keyword — to see what goes wrong without it:

```csharp
public class Button
{
    public Action<string> Clicked; // just a public delegate field, no 'event' keyword
}

class Program
{
    static void Main()
    {
        var button = new Button();
        button.Clicked += msg => Console.WriteLine($"Handler A: {msg}");
        button.Clicked += msg => Console.WriteLine($"Handler B: {msg}");

        // PROBLEM: any external code can do this...
        button.Clicked = msg => Console.WriteLine("I replaced everything!"); 
        // This wipes out Handler A and Handler B entirely!

        // PROBLEM: any external code can also invoke it directly
        button.Clicked?.Invoke("Fired from outside the class!");
    }
}
```

Without `event`, the delegate field is just a normal public field. External code can overwrite it (`=` instead of `+=`), clear all other subscribers, or invoke it whenever it wants — even though logically only the `Button` class itself should decide when a "click" actually happens.

## How `event` Fixes This

Adding the `event` keyword restricts what outside code can do with that delegate field:

```csharp
public class Button
{
    public event Action<string> Clicked; // now restricted

    public void SimulateClick()
    {
        Clicked?.Invoke("Button was clicked!"); // only the Button class can invoke it
    }
}
```

Now, external code:
- ✅ **Can** subscribe: `button.Clicked += handler;`
- ✅ **Can** unsubscribe: `button.Clicked -= handler;`
- ❌ **Cannot** invoke it directly: `button.Clicked("test");` → compile error
- ❌ **Cannot** overwrite it: `button.Clicked = handler;` → compile error

This enforces the publisher/subscriber contract at compile time — only `Button` decides when `Clicked` fires.

## Full Working Example

```csharp
using System;

public class Button
{
    public event Action<string> Clicked;

    public void SimulateClick()
    {
        Console.WriteLine("Button logic executing...");
        Clicked?.Invoke("Button was clicked!");
    }
}

class Program
{
    static void Main()
    {
        var button = new Button();

        // Subscribe two handlers
        button.Clicked += OnButtonClicked;
        button.Clicked += msg => Console.WriteLine($"Lambda handler: {msg}");

        button.SimulateClick();
        // Output:
        // Button logic executing...
        // Standard handler received: Button was clicked!
        // Lambda handler: Button was clicked!

        // Unsubscribe one handler
        button.Clicked -= OnButtonClicked;

        button.SimulateClick();
        // Output:
        // Button logic executing...
        // Lambda handler: Button was clicked!
    }

    static void OnButtonClicked(string message)
    {
        Console.WriteLine($"Standard handler received: {message}");
    }
}
```

## The Standard .NET Event Pattern: `EventHandler`

While `Action<T>` works fine for custom events, .NET has a long-standing convention using `EventHandler` and `EventHandler<TEventArgs>`, especially in UI frameworks (WinForms, WPF) and many library APIs:

```csharp
using System;

// Custom EventArgs to carry extra data about the event
public class OrderPlacedEventArgs : EventArgs
{
    public int OrderId { get; }
    public decimal Total { get; }

    public OrderPlacedEventArgs(int orderId, decimal total)
    {
        OrderId = orderId;
        Total = total;
    }
}

public class OrderService
{
    // Standard signature: (object sender, TEventArgs e)
    public event EventHandler<OrderPlacedEventArgs> OrderPlaced;

    public void PlaceOrder(int orderId, decimal total)
    {
        Console.WriteLine($"Processing order {orderId}...");

        // Raise the event, passing 'this' as sender
        OrderPlaced?.Invoke(this, new OrderPlacedEventArgs(orderId, total));
    }
}

class Program
{
    static void Main()
    {
        var service = new OrderService();

        // Subscribe — note the (sender, e) signature
        service.OrderPlaced += (sender, e) =>
        {
            Console.WriteLine($"Notification: Order #{e.OrderId} placed for ${e.Total}");
        };

        service.OrderPlaced += SendEmailReceipt;

        service.PlaceOrder(1001, 49.99m);
    }

    static void SendEmailReceipt(object sender, OrderPlacedEventArgs e)
    {
        Console.WriteLine($"Emailing receipt for order #{e.OrderId}...");
    }
}
```

**Why `object sender, TEventArgs e`?** This convention lets a single handler be reused across multiple event sources — the `sender` parameter tells the handler *which* object raised the event, and `e` carries the data specific to that occurrence.

## Key Details Worth Knowing

### 1. `?.Invoke()` guards against no subscribers
```csharp
Clicked?.Invoke("message");
```
If nobody has subscribed, the event field is `null` by default. Calling `Invoke` on `null` directly would throw a `NullReferenceException` — the null-conditional operator (`?.`) avoids that.

### 2. Multicast — multiple subscribers all get called
Just like multicast delegates, `+=` adds another handler to the invocation list; all subscribed handlers run in the order they were added when the event fires.

### 3. Thread-safety nuance
In multithreaded code, there's a subtle race: between checking `Clicked != null` and actually invoking it, another thread could unsubscribe, making it `null` again. The safest pattern is:
```csharp
var handler = Clicked; // copy the reference first
handler?.Invoke("message");
```
This avoids the race because you're checking/invoking the *copy*, not the live field.

### 4. `add`/`remove` accessors (advanced)
Just like properties have `get`/`set`, events can define custom `add`/`remove` logic instead of relying on the compiler-generated default:
```csharp
private Action<string> _clicked;
public event Action<string> Clicked
{
    add { _clicked += value; Console.WriteLine("Subscriber added"); }
    remove { _clicked -= value; Console.WriteLine("Subscriber removed"); }
}
```
This is rarely needed but useful for logging subscriptions or implementing custom storage (e.g., a dictionary-backed event system with many events, like WinForms does internally).

---

## Summary

| Concept | Delegate | Event |
|---|---|---|
| Can be invoked by outside code? | Yes | No — only by the declaring class |
| Can be overwritten (`=`) by outside code? | Yes | No — only `+=`/`-=` allowed externally |
| Purpose | General-purpose "pass behavior around" | Specifically for publisher/subscriber notifications |

**In short:** an event is a delegate with guardrails — it keeps the "who can trigger this" power inside the class that owns it, while still letting any number of outside listeners subscribe and react when it happens.
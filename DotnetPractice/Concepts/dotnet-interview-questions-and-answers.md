# .NET Interview Questions & Answers (Junior to Senior)

---

## JUNIOR LEVEL

### C# Fundamentals

**1. What's the difference between value types and reference types?**
Value types (structs, `int`, `bool`, `enum`, etc.) store their data directly and are typically allocated on the stack (or inline in containing objects); copying a value type copies the data. Reference types (`class`, `string`, arrays, delegates) store a reference to data on the heap; copying a reference type copies the reference, so both variables point to the same object.

**2. Explain the difference between `==` and `.Equals()`.**
For reference types, `==` by default compares references (identity) unless overloaded, while `.Equals()` is meant to compare logical equality and can be overridden. `string` overrides both to compare content. For value types, `==` typically compares values (if overloaded/defined), and `.Equals()` (inherited from `ValueType`) does a member-wise comparison by default via reflection, which is slower — custom structs should override `Equals`/`GetHashCode` for performance and correctness.

**3. What is boxing/unboxing?**
Boxing is converting a value type to a reference type (`object` or an interface) by wrapping it on the heap. Unboxing is extracting the value type back out. Both incur performance cost (allocation, copying) and should be avoided in hot paths — generics (`List<int>` instead of `ArrayList`) help avoid it.

**4. Difference between `string` and `StringBuilder` — why does it matter in loops?**
`string` is immutable — every modification (concatenation) creates a new string object. In a loop, repeated concatenation causes many intermediate allocations, leading to O(n²) behavior. `StringBuilder` maintains a mutable buffer, so appending in a loop is much more efficient.

**5. What are access modifiers in C# (`public`, `private`, `protected`, `internal`)?**
- `public`: accessible from anywhere.
- `private`: accessible only within the containing type.
- `protected`: accessible within the containing type and derived types.
- `internal`: accessible within the same assembly.
- `protected internal`: union of protected and internal.
- `private protected`: intersection — derived types in the same assembly only.

**6. What is the difference between an abstract class and an interface?**
An abstract class can have state (fields), constructors, and both implemented and abstract members; a class can inherit only one abstract class. An interface (traditionally) defines a contract with no state, and a class can implement multiple interfaces. Since C# 8, interfaces can have default method implementations, narrowing the gap, but interfaces still can't hold instance fields or constructors.

**7. Explain `null`, nullable types (`int?`), and the null-coalescing operator (`??`).**
Reference types can be `null` by default (absence of a reference). Value types cannot be `null` unless wrapped in `Nullable<T>` (`int?`), which adds a `HasValue`/`Value` pair. The `??` operator returns the left operand if not null, otherwise the right (`x ?? defaultValue`); `??=` assigns only if the left side is null.

**8. What is a namespace, and why do we use it?**
A namespace is a logical grouping of types to organize code and avoid naming collisions across libraries or modules — e.g., `System.Collections.Generic` vs. a custom `MyApp.Collections`.

**9. Difference between `const` and `readonly`?**
`const` is a compile-time constant, must be initialized at declaration, and is implicitly static. `readonly` is a runtime constant, can be set in the constructor, and can vary per instance.

**10. What are properties, and how do they differ from fields?**
A field is a raw variable holding data. A property wraps access to a field (or computed value) with `get`/`set` accessors, allowing validation, computed logic, or encapsulation without changing the public API. Auto-properties (`public int Age { get; set; }`) generate a hidden backing field automatically.

### .NET Basics

**11. What is the CLR, and what does it do?**
The Common Language Runtime is the execution engine for .NET. It handles JIT compilation of IL to native code, memory management (garbage collection), exception handling, type safety, and thread management.

**12. What is the difference between .NET Framework, .NET Core, and .NET (5+)?**
.NET Framework is the original, Windows-only runtime (up to 4.8, in maintenance mode). .NET Core was the cross-platform, open-source rewrite (versions 1.x–3.x). Starting with .NET 5, Microsoft unified everything into a single ".NET" release line (dropping "Core" from the name) that is cross-platform and is the actively developed path forward.

**13. What is a NuGet package?**
A NuGet package is a distributable unit of reusable .NET code (a `.nupkg` file) containing compiled assemblies, metadata, and dependencies, managed via the NuGet package manager.

**14. What's the difference between `Debug` and `Release` build configurations?**
`Debug` builds include full symbol/debugging info and disable most compiler optimizations for easier step-through debugging. `Release` builds are optimized for performance and size and strip most debug symbols, intended for production deployment.

**15. What is exception handling, and how do `try`/`catch`/`finally` work?**
`try` wraps code that might throw. `catch` blocks handle specific (or general) exception types when thrown. `finally` runs regardless of whether an exception occurred, typically used for cleanup (though `using`/`IDisposable` is often preferred for resource cleanup).

### Basic OOP

**16. Explain the four pillars of OOP with examples.**
- **Encapsulation**: hiding internal state behind properties/methods (e.g., a `BankAccount` exposing `Deposit()` instead of a public balance field).
- **Abstraction**: exposing only relevant details via interfaces/abstract classes (e.g., `IPaymentProcessor` hiding Stripe/PayPal specifics).
- **Inheritance**: a class reusing/extending behavior from a base class (e.g., `Manager : Employee`).
- **Polymorphism**: treating different types uniformly through a common interface, with behavior resolved at runtime (e.g., a list of `Shape` objects each implementing `Area()` differently).

**17. What is method overloading vs overriding?**
Overloading: multiple methods with the same name but different parameter signatures in the same scope, resolved at compile time. Overriding: a derived class provides a new implementation of a base class's `virtual`/`abstract` method, resolved at runtime (polymorphism).

**18. What does the `virtual` and `override` keyword pair do?**
`virtual` on a base class method allows derived classes to replace its implementation using `override`. Without `virtual`, a derived class can only hide the base method using `new`, which doesn't participate in polymorphic dispatch.

---

## MID LEVEL

### C# Language

**19. Explain `IEnumerable<T>` vs `List<T>`.**
`IEnumerable<T>` is an interface representing a forward-only, lazily-evaluated sequence — it only guarantees iteration (`GetEnumerator`). `List<T>` is a concrete, in-memory collection with indexing, `Count`, `Add`/`Remove`, etc. Returning `IEnumerable<T>` from a method signals "this is just a sequence you can iterate," while `List<T>` signals a concrete, mutable, materialized collection.

**20. What are delegates and events? How do they relate to each other?**
A delegate is a type-safe function pointer — it references a method matching a specific signature and can be invoked, combined, or passed around. An event is a wrapper around a delegate (typically `Action` or a custom delegate) that restricts external code to only subscribing (`+=`)/unsubscribing (`-=`), not invoking it directly — enforcing the publisher/subscriber pattern.

**21. What is LINQ, and how does deferred execution work?**
LINQ (Language Integrated Query) provides a unified, declarative syntax for querying collections, databases (via EF), XML, etc. Deferred execution means many LINQ operators (`Where`, `Select`) build up an expression/query but don't execute until the result is enumerated (e.g., via `foreach`, `.ToList()`, `.Count()`). This means the underlying data source can change between query definition and execution, which can cause subtle bugs if not understood.

**22. Explain extension methods — how are they implemented?**
Extension methods let you "add" methods to existing types without modifying them, by defining a `static` method in a `static` class where the first parameter is prefixed with `this` (e.g., `public static bool IsValidEmail(this string s)`). Under the hood, it's just syntactic sugar — the compiler translates `str.IsValidEmail()` into `MyExtensions.IsValidEmail(str)`.

**23. What are generics, and why use them over `object`?**
Generics allow types/methods to be parameterized by type (`List<T>`, `Dictionary<TKey, TValue>`), providing compile-time type safety and avoiding boxing/casting overhead that would occur with `object`-based collections. They also improve performance since the JIT can generate specialized code per value-type instantiation.

**24. Difference between `Task`, `Thread`, and `async`/`await`?**
A `Thread` is a raw OS-level thread — expensive to create, and you manage it directly. A `Task` is a higher-level abstraction representing an asynchronous operation, usually run on the ThreadPool (for CPU work) or backed by I/O completion (for I/O work) — much cheaper than a dedicated thread. `async`/`await` is compiler syntax for writing asynchronous code that reads like synchronous code, built on top of `Task`, freeing the calling thread while waiting rather than blocking it.

**25. What's the difference between `IDisposable` and garbage collection — why do we still need `using` statements?**
The GC reclaims managed memory but does not know how to release unmanaged resources (file handles, sockets, DB connections) deterministically. `IDisposable.Dispose()` gives you a deterministic way to release those resources as soon as you're done, rather than waiting for a GC cycle (which may never happen for that object, or may happen too late). `using` ensures `Dispose()` is called even if an exception occurs.

**26. Explain exception filtering (`catch (Exception ex) when (...)`).**
The `when` clause lets you conditionally catch an exception only if a predicate is true, without unwinding the stack to evaluate the condition (important for accurate stack traces and logging), e.g., `catch (HttpRequestException ex) when (ex.StatusCode == 404)`.

**27. What are anonymous types and tuples used for?**
Anonymous types (`new { Name = "Alex", Age = 30 }`) let you create ad-hoc, read-only objects (commonly used in LINQ projections) without declaring a formal class. Tuples (`(string Name, int Age)`) let you group multiple values without a dedicated type, useful for returning multiple values from a method concisely.

**28. Explain the difference between shallow copy and deep copy.**
A shallow copy duplicates an object's top-level fields — if a field is a reference type, both the original and the copy point to the same nested object. A deep copy recursively duplicates all referenced objects as well, so the copy is fully independent.

### .NET / Framework

**29. What is Dependency Injection, and why is it built into ASP.NET Core?**
DI is a pattern where a class's dependencies are provided (injected) from the outside rather than created internally, improving testability, decoupling, and configurability. ASP.NET Core has DI built in via `IServiceCollection`/`IServiceProvider` so that services (loggers, DbContexts, HttpClients, custom services) can be registered once and resolved automatically wherever needed (constructors of controllers, middleware, etc.).

**30. Explain the three DI lifetimes: Singleton, Scoped, Transient.**
- **Singleton**: one instance for the entire application lifetime.
- **Scoped**: one instance per request (in a web app) or per defined scope.
- **Transient**: a new instance every time it's requested.

**31. What is middleware in ASP.NET Core, and how does the pipeline work?**
Middleware are components chained together to process HTTP requests/responses. Each middleware can perform logic before and after calling the next component in the chain (`await next(context)`), or short-circuit the pipeline by not calling `next` at all (e.g., returning a 401 early). Order of registration in `Program.cs` matters significantly.

**32. What's the difference between `appsettings.json` environments (Development, Production)?**
ASP.NET Core supports environment-specific configuration overlays — `appsettings.json` provides base config, and `appsettings.{Environment}.json` (e.g., `appsettings.Development.json`) overrides values for that specific environment, selected via the `ASPNETCORE_ENVIRONMENT` variable.

**33. What is Entity Framework Core, and what's the difference between Code First and Database First?**
EF Core is an ORM (Object-Relational Mapper) that maps .NET classes to database tables and translates LINQ queries into SQL. Code First means you define C# entity classes and let EF generate/migrate the database schema from them. Database First means you generate C# entity classes from an existing database schema (often via scaffolding).

**34. How do you handle configuration and secrets (e.g., `IConfiguration`, user secrets, environment variables)?**
`IConfiguration` aggregates settings from multiple providers (JSON files, environment variables, command-line args, Azure Key Vault, etc.) with a defined precedence order. For local development, the Secret Manager tool (`dotnet user-secrets`) stores sensitive values outside source control; in production, environment variables or a secret store (Key Vault, AWS Secrets Manager) are used instead of committing secrets to `appsettings.json`.

**35. What's the difference between REST and SOAP? What is a RESTful API?**
SOAP is a protocol with a strict XML-based message format, typically requiring a WSDL contract. REST is an architectural style built on standard HTTP verbs (GET/POST/PUT/DELETE), typically using JSON, and is stateless, resource-oriented (URLs represent resources), and more lightweight/flexible than SOAP.

**36. Explain HTTP status codes commonly used in APIs (200, 201, 400, 401, 403, 404, 500).**
- `200 OK`: success.
- `201 Created`: resource successfully created.
- `400 Bad Request`: malformed/invalid request.
- `401 Unauthorized`: authentication required/failed.
- `403 Forbidden`: authenticated but not authorized for this action.
- `404 Not Found`: resource doesn't exist.
- `500 Internal Server Error`: unhandled server-side failure.

### Testing

**37. What is unit testing, and which frameworks have you used (xUnit, NUnit, MSTest)?**
Unit testing verifies small, isolated units of code (typically a single method/class) behave correctly, independent of external dependencies (DB, network, filesystem) — those are typically mocked. xUnit, NUnit, and MSTest are the three major .NET testing frameworks; xUnit is currently the most widely used in modern .NET projects, favoring constructor-based setup and `[Theory]`/`[InlineData]` for parameterized tests.

**38. What is mocking, and why do we use it in tests?**
Mocking replaces a real dependency (e.g., a database repository or an HTTP client) with a fake, controllable substitute so tests can run in isolation, deterministically, and quickly — without needing a real network call or database. Common libraries: Moq, NSubstitute, FakeItEasy.

---

## SENIOR LEVEL

### C# / CLR Internals

**39. Explain `Task` vs `ValueTask`, and when to use each.**
`Task` is a reference type — every async call allocates a `Task` object on the heap, even for already-completed/synchronous paths. `ValueTask` is a struct that can wrap either a result directly (no allocation) or an underlying `Task`, avoiding allocation in the common case where the operation completes synchronously (e.g., a cache hit). Use `ValueTask` in hot paths with frequent synchronous completion; stick with `Task` for general-purpose async APIs, since `ValueTask` has restrictions (can't be awaited twice, can't check `.IsCompleted` after being awaited, etc.) that make it easy to misuse.

**40. How does garbage collection work in .NET — generations, mark-and-sweep, and what triggers a Gen2 collection?**
The GC automatically manages heap memory for reference types by identifying objects no longer reachable from the running program and reclaiming their memory, so there's no manual `free()`.

- **Identifying garbage (mark-and-sweep):** starting from "roots" (static fields, local variables on the stack, CPU registers), the GC traces every reachable object graph. Anything not reached is garbage and can be reclaimed.
- **Generational model:** Gen0 (short-lived objects — most objects), Gen1 (a buffer generation), Gen2 (long-lived objects like caches/singletons), plus a separate **Large Object Heap (LOH)** for objects ≥ 85KB. New objects start in Gen0; if they survive a collection, they're promoted to Gen1, then Gen2. This rests on the generational hypothesis that most objects die young, so focusing collections on Gen0 is cheap and efficient.
- **Compaction:** surviving Gen0/Gen1 objects are compacted (moved together) to reduce fragmentation and keep future allocation cheap (just bumping a pointer). The LOH isn't compacted by default, since moving large blocks is expensive.
- **What triggers a Gen2 (full) collection:** Gen2 exceeding an internal threshold, an explicit `GC.Collect()` call, the system reporting memory pressure, or the LOH needing collection. Gen2 collections are the most expensive since they scan the whole heap.

The GC only manages *managed* memory — it doesn't know how to release unmanaged resources (file handles, sockets, DB connections), which is why `IDisposable`/`using` still matter: `Dispose()` gives a deterministic cleanup point instead of waiting on a GC cycle that may come late or never for that object.

**41. Walk through how `async`/`await` compiles into a state machine. What happens when you block on a `Task` with `.Result`?**
The compiler transforms an `async` method into a compiler-generated state machine (a struct or class implementing `IAsyncStateMachine`) with a `MoveNext()` method. Each `await` point becomes a state transition — when an awaited task isn't complete, the method registers a continuation and returns control to the caller; when the awaited task completes, `MoveNext()` resumes execution at the correct state. Calling `.Result` or `.Wait()` synchronously blocks the calling thread until the task completes. In contexts with a `SynchronizationContext` (like older ASP.NET or WPF/WinForms UI threads), this can deadlock: the continuation needs to resume on that same captured context, but the thread is blocked waiting for the continuation, which can never run — classic deadlock. ASP.NET Core doesn't have this specific context by default, but blocking still wastes threads and hurts scalability.

**42. What are `Span<T>` and `Memory<T>`, and what performance problems do they solve?**
`Span<T>` is a stack-only (`ref struct`) type representing a contiguous, type-safe view over memory (array, stack-allocated memory, or unmanaged memory) without copying it. `Memory<T>` is its heap-allocatable counterpart (usable in async methods/fields, since `Span<T>` cannot be). They allow slicing and processing buffers (e.g., parsing strings, working with byte arrays) without allocating new arrays/substrings, significantly reducing GC pressure in performance-critical code like parsers and serializers.

**43. Explain covariance and contravariance in generics.**
Covariance (`out T`) allows a generic interface/delegate to return a more derived type than specified — e.g., `IEnumerable<string>` can be treated as `IEnumerable<object>` because it only produces `T`. Contravariance (`in T`) allows accepting a less derived type than specified — e.g., `Action<object>` can be used where `Action<string>` is expected, because it only consumes `T`. These only apply to reference types and to interfaces/delegates marked with `out`/`in`.

**44. Difference between `ref`, `out`, and `in` parameters?**
`ref` passes a variable by reference, requiring it to be initialized before the call, allowing the method to read and modify it. `out` also passes by reference but doesn't require prior initialization, and the method must assign it before returning. `in` passes by reference but is read-only inside the method — used to avoid copying large structs while guaranteeing the callee can't modify the argument.

### Concurrency

**45. Difference between `lock`, `SemaphoreSlim`, `Mutex`, and `ReaderWriterLockSlim` — when would you use each?**
- **`lock`** (`Monitor`): simplest in-process mutual exclusion for a critical section; can't be used across processes and doesn't support `async`.
- **`SemaphoreSlim`**: limits concurrent access to N callers (not just 1); supports `WaitAsync()`, making it the standard choice for throttling concurrent async operations.
- **`Mutex`**: OS-level, can be named and used for cross-process synchronization, but heavier-weight than `lock`.
- **`ReaderWriterLockSlim`**: allows multiple concurrent readers but exclusive writer access — ideal when reads vastly outnumber writes and you want more throughput than a plain `lock`.

**46. How would you diagnose a deadlock or race condition in a production system?**
For deadlocks: capture a memory dump (`dotnet-dump`) during the hang and inspect thread stacks to find threads blocked waiting on each other's locks (circular wait), or use tools like WinDbg/`!syncblk`. For race conditions: they're often intermittent and hard to reproduce — look for shared mutable state accessed without synchronization, add logging/telemetry around suspect code paths, use stress testing/`Task.WhenAll` under load to increase reproducibility, and consider tools like the Concurrency Visualizer or thread-safety analyzers.

**47. How does the Thread Pool manage worker threads vs I/O completion ports?**
The CLR Thread Pool maintains a pool of worker threads for CPU-bound work queued via `Task.Run`/`ThreadPool.QueueUserWorkItem`, and separately uses I/O completion ports (IOCP) for asynchronous I/O operations (file, network) so that no thread is consumed while waiting on I/O — the OS notifies the runtime when the I/O completes, and a thread pool thread picks up the continuation at that point. This is why truly async I/O (`await httpClient.GetAsync(...)`) doesn't block or consume a thread while waiting.

**48. How would you throttle or limit concurrent async operations?**
Use `SemaphoreSlim` to cap concurrency: acquire the semaphore before starting each operation and release it in a `finally`, combined with `Task.WhenAll` over a set of tasks. Alternatively, use `Parallel.ForEachAsync` (available in .NET 6+) with a configured `MaxDegreeOfParallelism`, or a dedicated library like `System.Threading.RateLimiting` for more advanced policies (token bucket, sliding window, etc.).

### Architecture & Design

**49. Explain CQRS and event sourcing — what problems do they solve and what complexity do they add?**
CQRS (Command Query Responsibility Segregation) separates the write model (commands that change state) from the read model (queries), allowing each to be optimized/scaled independently — useful when read and write workloads have very different patterns. Event sourcing stores state as a sequence of immutable events rather than a current snapshot, enabling full audit history and rebuilding state at any point in time. Together they solve scalability and auditability problems but add significant complexity: eventual consistency between read/write models, event schema versioning, and a steeper learning curve — not appropriate for simple CRUD systems.

**50. How do you design idempotent operations in a distributed system (e.g., payments)?**
Require clients to send a unique idempotency key per logical operation; the server persists a record of keys it has already processed (with the result), and if the same key arrives again (e.g., due to a retry after a timeout), it returns the previously stored result rather than re-executing the operation. This is typically implemented with a unique constraint on the idempotency key in the database to prevent race conditions between concurrent duplicate requests.

**51. How would you version a public API without breaking existing clients?**
Common strategies: URI versioning (`/v1/orders`, `/v2/orders`), header-based versioning (custom header or `Accept` header with a version), or query-string versioning. Favor additive, backward-compatible changes (new optional fields) over breaking changes where possible, and maintain deprecated versions for a defined sunset period with clear communication to consumers.

**52. What's your approach to designing a caching strategy (invalidation, stampede protection, distributed vs local)?**
Choose local (in-memory, `IMemoryCache`) caching for single-instance or non-critical-consistency data, and distributed caching (Redis, `IDistributedCache`) when multiple instances need a shared, consistent cache. For invalidation, use short TTLs plus explicit invalidation on writes (cache-aside pattern), rather than relying on TTL alone. For stampede protection (many requests hitting an expired key simultaneously and all missing the cache), use a lock/semaphore per key so only one request repopulates the cache while others wait, or serve slightly stale data while refreshing in the background.

**53. Repository/Unit of Work pattern — still relevant with EF Core's `DbContext`?**
`DbContext` already implements both patterns internally — it's a Unit of Work (tracks changes, commits via `SaveChanges()`), and `DbSet<T>` acts like a repository. Wrapping it in an additional custom Repository layer is often unnecessary abstraction that can obscure EF Core's capabilities (like `Include`, projections, and query composition) without adding real testability benefit, since `DbContext` can already be abstracted/mocked via interfaces if truly needed. It can still be justified when you need to fully decouple from EF Core (e.g., swapping data access technology) or centralize complex query logic.

### Data & Performance

**54. How do you diagnose and fix an N+1 query problem in EF Core?**
Diagnosis: enable EF Core logging (`.LogTo(Console.WriteLine)`) or use SQL Server Profiler/MiniProfiler to observe repeated, near-identical queries executed inside a loop (typically from lazy-loaded navigation properties accessed per-row). Fix: use eager loading (`.Include()`/`.ThenInclude()`) to fetch related data in a single query, or project directly into a DTO with `.Select()` to fetch only what's needed in one round trip.

**55. Optimistic vs pessimistic concurrency — how do you implement each?**
Optimistic concurrency assumes conflicts are rare: EF Core implements it via a concurrency token (a `RowVersion`/`[Timestamp]` column), and `SaveChanges()` throws a `DbUpdateConcurrencyException` if the row changed since it was read, allowing you to reconcile or retry. Pessimistic concurrency locks the row at read time (e.g., `SELECT ... FOR UPDATE` or explicit transaction-level locking hints) to prevent other transactions from modifying it until the lock is released — safer under high contention but reduces throughput and risks holding locks too long.

**56. How would you investigate a memory leak in a long-running .NET service?**
Capture memory dumps at different points in time (`dotnet-dump collect`) and compare object counts/retained sizes using `dotnet-dump analyze` or WinDbg with SOS to find types growing unexpectedly. Common causes: static event handlers holding references to subscribers that are never unsubscribed, caches without eviction policies, undisposed `IDisposable` objects (especially `HttpClient`, DB connections), or captured closures keeping large objects alive longer than expected.

**57. What tools would you use to profile CPU/memory in production (dotnet-trace, dotnet-counters, PerfView)?**
`dotnet-counters` gives a live, low-overhead view of key metrics (GC heap size, thread pool queue length, exception count, request rate) — good for a quick health check. `dotnet-trace` captures detailed ETW/EventPipe traces for deeper CPU/allocation analysis, viewable in PerfView or Visual Studio. PerfView is particularly strong for analyzing GC behavior and CPU sampling on Windows. For production, these tools are designed to be low-overhead enough to run safely without a full debugger attached.

**58. Why shouldn't you `new HttpClient()` per request, and how does `IHttpClientFactory` solve it?**
Each `HttpClient` instance owns its own underlying socket/connection pool; creating and disposing many instances rapidly can exhaust available sockets (socket exhaustion) because the underlying connections linger in a `TIME_WAIT` state even after disposal. `IHttpClientFactory` manages a pool of `HttpMessageHandler` instances behind the scenes, recycling them on a rotation (default 2 minutes) to balance connection reuse with picking up DNS changes, while giving you a fresh logical `HttpClient` per request without the socket exhaustion risk.

### Behavioral (Senior-specific)

**59. Tell me about a production incident you diagnosed — walk through your process.**
*(Open-ended — look for: how they gathered data before acting, use of logs/metrics/dumps rather than guessing, communication during the incident, root cause identification vs just patching symptoms, and follow-up/prevention steps like added monitoring or tests.)*

**60. Describe an architectural trade-off you made under a deadline, and how it played out.**
*(Open-ended — look for: awareness of the trade-off at the time (not just hindsight), reasoning about cost/benefit, whether technical debt was tracked/communicated, and what they'd do differently with more time.)*

**61. How do you mentor junior developers on writing idiomatic C#?**
*(Open-ended — look for: code review approach, balancing correction with encouragement, teaching principles/patterns rather than just fixing code, and how they calibrate feedback to the person's experience level.)*

---

*Document generated as a senior-to-junior .NET interview reference guide.*

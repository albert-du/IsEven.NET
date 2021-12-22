# IsEven.NET
Tell if an integer is even using [isEven API](https://isevenapi.xyz/) with .NET.

## Usage
### C#
IsEven.NET provides a static class `IsEven`, containing all parity finding APIs. The static method `IsEven` is used to find if a number is even.
```csharp
using System;
using static IsEven;

var result = IsEven(2);

Console.WriteLine(result.Even);
Console.WriteLine(result.Ad);

// True
// FOR SALE: Complete set of Encyclopedia Britannica, 45 volumes. $1000. No longer needed. Got married, wife knows everything. Call 5435553442.
```
An asynchronous API is also provided with `IsEvenAsync` using [async/await](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/).
```csharp
using System;
using static IsEven;

var random = new Random();

var result = await IsEvenAsync(random.Next(100));

Console.WriteLine(result.Ad);

// Looking for someone to do yard work. Must have a hoolahoop. 760-555-7562
```
### F#
IsEven.NET provides a module `IsEven`, containing all parity finding APIs. The function `isEven` is used to find if a number is even.
```fsharp
open IsEven

let result = isEven 2

printfn "%b" result.Even
printfn "%s" result.Ad

// true
// HONDA CIVIC '96, AM/FM/CD, low miles, Good condition. Speaks Spanish $3500 339-555-6289
```

An asynchronous API is also provided with `isEvenAsync` using [task expressions](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/task-expressions).
```fsharp
open System
open IsEven

let main =
    task {
        let random = Random()

        let! result = random.Next 100 |> isEvenAsync

        printfn "%s" result.Ad
    }

main.Wait()

// HELP WANTED: Child Care provider. Apply in person, Jack & Kill Childcare, 1905 NW Smith. NO PHONE CALLS
```

## Remarks
Only the free tier of [isEven API](https://isevenapi.xyz/) is currently available with IsEven.NET. Numbers between 0 and 999,999 are supported.  
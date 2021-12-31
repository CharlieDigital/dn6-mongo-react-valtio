# .NET6 / MongoDB / React / Vite / MUI / Valtio

This project demonstrates the usage of:

- Web
  - React
  - Vite
  - MUI
  - Valtio
- API
  - .NET 6
  - MongoDB

The code will run on Windows, MacOS, Linux, and ARM architectures; you can work with it with only VS Code (and probably works with JetBrains Rider).

Use this as a starting point for your own code.

## Rationale

The key objective of this stack is to fulfill the following:

### Secure Runtime

According to [GitHub's State of the Octoverse Security Report from 2020](https://octoverse.github.com/static/github-octoverse-2020-security-report.pdf), the NPM package ecosystem has:

- The highest number of transitive dependencies [by an order of magnitude]
- The highest percentage of advisories
- The highest number of critical and high advisories
- The highest number of Dependabot alerts
- A lag time of *218 weeks* before a vulnerability is detected and patched

### Performance

Multiple benchmarks show that .NET Core is now in the same performance tier as Go and Rust in real-world workloads while still being highly accessible and easy to hire for.

- [The Computer Language Benchmarks Game comparing Go vs C# in math heavy computations](https://benchmarksgame-team.pages.debian.net/benchmarksgame/fastest/go-csharpcore.html)
- [TechEmpower Round 20 Benchmarks with ASP.NET Core beating Fiber (Go) and absolutely stomping Node.js and Nest.js](https://www.techempower.com/benchmarks/#section=data-r20&hw=ph&test=composite)
- [Alex Yakunin's article exploring Go vs .NET garbage collection and memory allocation throughput](https://medium.com/servicetitan-engineering/go-vs-c-part-3-compiler-runtime-type-system-modules-and-everything-else-faa423dddb34)
- [Aleksandr Filichkin's benchmarks comparing AWS Lambda performance of various languages](https://filia-aleks.medium.com/aws-lambda-battle-x86-vs-arm-graviton2-perfromance-3581aaef75d9)

[.NET 6 brings additional performance improvements](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-6/) which will continue to push the boundaries.

While Node, Go, and Rust show better cold-start performance, this is generally a small penalty that is only paid once on the initial startup of a container instance.  It can be optimized for [by using pre-JIT techniques](https://medium.com/@tainguyenbui/optimizing-c-aws-lambda-with-custom-runtime-2f1f69f0431a).  [.NET 7 addresses this with NativeAOT](https://github.com/dotnet/runtime/issues/61231).

The same is true of [using vite](https://dev.to/alvarosaburido/vite-2-a-speed-comparison-in-vue-1f5j).  Vite uses esbuild underneath [and its performance puts webpack out to pasture (at least for dev builds!)](https://developpaper.com/ask-if-you-dont-understand-where-is-esbuild/).

### Easy(-ish) to Transition

[Both C# and TypeScript were designed by Anders Hejlsberg of Microsoft](https://en.wikipedia.org/wiki/Anders_Hejlsberg).  And while TypeScript offers a wide degree of freedom with how one uses it, developers familiar with TypeScript's features like generics, interfaces, and inheritance will have a very short ramp to C# compared to Go or Rust.

TypeScript:

```ts
interface IRepository<T> {
    Save(entity: T): void;

    List(): T[];
}

class Person {
    public firstName: string;
    public lastName: string;

    constructor(firstName: string, lastName: string) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}

class PersonRepository implements IRepository<Person> {
    public Save(instance: Person): void {
        // Do save here...
    }

    List = (): Person[] => [];

    public static Init(): void {
        var person = new Person("Amy", "Lee");
        var repository = new PersonRepository();

        repository.Save(person);
    }
}
```

C#

```csharp
interface IRepository<T> {
    void Save(T entity);

    T[] List();
}

class Person {
    public string FirstName;
    public string LastName;

    public Person(string firstName, string lastName) {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}

class PersonRepository : IRepository<Person> {
    public void Save(Person instance) {
        // Do save here...
    }

    public Person[] List() => new Person[] {};

    public static void Init() {
        var person = new Person("Amy", "Lee");
        var repository = new PersonRepository();

        repository.Save(person);
    }
}
```

While it's true that JavaScript and TypeScript have a spectrum of styles from very functional to semi-object-oriented, C#; JavaScript; and TypeScript do share a lot of syntactic and stylistic similarities as well as general functionality because the .NET runtime has come to support functional programming languages (F#) over the years.

The TypeScript example above could obviously be far more functional in style as well.  But if you are already planning on adopting stronger typing on the server, it seems like a good opportunity to simply step up to C# instead.

### Productivity with .NET MongoDB Driver LINQ

At scale, teams *need* to have a strongly typed data model in the API.  Starting from a loosely/untyped data model in the early stages of a project can be critical for speed, but as the team grows, as customers seek APIs for integration, as the complexity of the domain space increases, the lack of a strongly-typed data model at the API is a bottleneck for growth and leads to slapdash code, high duplication, and high rates of defects that eventually start to hamper growth.

Whether TypeScript or C# or Go, having a typed data model and well-defined API is a long term necessity for any project that needs to scale as the number of individuals working in the codebase increases and as downstream consumers (other teams, customers, partners) increases.

If you are using MongoDB, the LINQ implementation in the .NET MongoDB driver is a huge productivity boost and provides a strongly typed query mechanism (given an application layer data model).

- [LINQ](https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/crud/linq/)
- [Object Mapping](https://mongodb.github.io/mongo-csharp-driver/2.14/reference/bson/mapping/#the-id-member)

Given this model:

```csharp
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public IEnumerable<Pet> Pets { get; set; }
    public int[] FavoriteNumbers { get; set; }
    public HashSet<string> FavoriteNames { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}

class Pet
{
    public string Name { get; set; }
}
```

We can write strongly typed queries like this:

```csharp
var query = from p in collection.AsQueryable()
            where p.Age > 21
            select new { p.Name, p.Age };

// or, using method syntax

var query = collection.AsQueryable()
    .Where(p => p.Age > 21)
    .Select(p => new { p.Name, p.Age });
```

No guessing what the schema is in the database.

### Productivity with OpenAPI TypeScript Client Generation

Like my earlier project [dotnet6-openapi](https://github.com/CharlieDigital/dotnet6-openapi), this project demonstrates generation of front-end TypeScript clients using the OpenAPI specification.

The project includes the Swagger middleware for .NET that produces schema documentation:

![Swagger Schema](static/swagger-ui.png)

Every time the .NET project is built, it generates a new OpenAPI schema output which can be used to generate a client for the React front-end. The TypeScript front-end even includes the comments from the server side.

```ts
// web/src/services/models/Company.ts

/**
 * Models a Company entity.
 */
export type Company = {
    id?: string | null;
    label?: string | null;
    /**
     * The address of the company.
     */
    address?: string | null;
    /**
     * The URL of the website for the given company.
     */
    webUrl?: string | null;
}
```

This provides a contract-based development experience that increases productivity by taking the guesswork out of calling APIs.

Once the schema is generated, running:

```
cd web
yarn run codegen
```

Will re-generate the client TypeScript bindings.  Any schema changes will cause build time errors.

### "Magically Simple" React State with Valtio

Daishi Kato, author of valtio, [describes the decision tree for selecting it as "magically simple"](https://twitter.com/dai_shi/status/1348257768130560008?s=20).

Indeed, its use of a proxy-based state model feels more natural in JavaScript compared to the immutable state model of Redux.

## Getting Started

This repo builds up on earlier work in my other projects:

- [react-valtio-example](https://github.com/CharlieDigital/react-valtio-example)
- [dotnet6-openapi](https://github.com/CharlieDigital/dotnet6-openapi)

### Running the API

To run the API, use the following commands:

```
cd api
dotnet run
```

Or

```
cd pi
dotnet watch
```

This latter one will watch for changes and automatically rebuild the solution.  It will also launch the UI for the swagger endpoint at: `https://localhost:7293/swagger/index.html`.

### Running the Front-End

To run the front-end, use the following commands:

```
cd web
yarn                # Pull the dependencies
yarn run codegen    # Generate the client code; will fail if server is already running
yarn run dev        # Starts the server.
```

The UI is accessible at `http://localhost:3000` by default.

### Testing the API

To test the API, use the following CURL commands:

```bash
curl --location --request POST 'http://localhost:5009/api/company/add' --header 'Content-Type: application/json' --data-raw '{ "Id": "", "Label": "Test" }'
curl --location --request GET 'http://localhost:5009/api/company/61cf5f0b414b44c2eb8c6f8c'
curl --location --request DELETE 'http://localhost:5009/api/company/delete/61cf5f0b414b44c2eb8c6f8c'
```

```powershell
$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Content-Type", "application/json")

$body = "{ `"Id`": `"`", `"Label`": `"Test`" }"

$response = Invoke-RestMethod 'http://localhost:5009/api/company/add' -Method 'POST' -Headers $headers -Body $body
$response = Invoke-RestMethod 'http://localhost:5009/api/company/61cf5f0b414b44c2eb8c6f8c' -Method 'GET' -Headers $headers
$response = Invoke-RestMethod 'http://localhost:5009/api/company/delete/61cf5f0b414b44c2eb8c6f8c' -Method 'DELETE' -Headers $headers
```

## Adding Tests

To add the tests, run:

```
mkdir tests
cd tests
dotnet new mstest
dotnet add reference ../api/Api.csproj
```

To get the intellisense to work correctly, we'll need to add a solution file:

```
cd ../
dotnet new sln
```

Then use the `dotnet sln` command (see [here](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln)):

```
dotnet sln add api
dotnet sln add tests
```

Finally, point OmniSharp to the `.sln` file by typing `CTRL+SHIFT+P` and then `OmniSharp: Select Project`.

## Logging

[Serilog](https://serilog.net/) has been injected in `Program.cs`.

The reason to consider Serilog is that it provides [many different sinks](https://github.com/serilog/serilog/wiki/Provided-Sinks) which can be written to simultaneously for comprehensive logging.

The [structured log output](https://github.com/serilog/serilog/wiki/Structured-Data) allows better data representation in the log output.

## Real Time

To add real time interactions to this, the easiest path is to incorporate [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-6.0).

In production, you're better off using Azure SignalR as this provides a low-cost, highly scalable web-sockets as a service capability.

Use the local hub for development and switch to Azure SignalR in production.

## Resources

- [.NET Core Web API MongoDB Tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio-code)
- [.NET Core Web API Attribute Routing](https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2)
- [.NET Core Model Binding](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-6.0#sources)
- [MongoDB .NET Driver Documentation](http://mongodb.github.io/mongo-csharp-driver/)
  - [LINQ](https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/crud/linq/)
  - [Advanced Class Mapping](http://mongodb.github.io/mongo-csharp-driver/2.10/reference/bson/mapping/)
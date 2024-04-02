# EDK.PartialRequest

[![NuGet Version](https://img.shields.io/nuget/v/EDK.PartialRequest.svg)](https://www.nuget.org/packages/EDK.PartialRequest/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

EDK.PartialRequest is a .NET library designed to simplify patch requests in ASP.NET Core.

## Usage

Define a request model that inherits from `PartialRequest<T>` where `T` is the type of the request model itself.

```csharp
using EDK.PartialRequest;

public class ExampleRequest : PartialRequest<ExampleRequest>
{
    public string? Field1 { get; set; }
    public int? Field2 { get; set; }
    public bool? Field3 { get; set; }
    public object? Field4 { get; set; }
}
```

Make sure to include Newtonsoft as your json serializer in your `Program.cs` or `Startup.cs`
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
```

In your controller, you can use this request model to check if properties are defined or not:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace EDK.PartialRequest.Example
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly Dictionary<string, object?> _data = new()
        {
            { "prop1", "original" },
            { "prop2", -1 },
            { "prop3", false },
            { "prop4", new { Name = "name" } }
        };

        [HttpPost("post")]
        public object Post([FromBody] ExampleRequest request)
        {
            return new {
                IsField1Defined = request.IsDefined(x => x.Field1),
                DefinedProperties = request.GetDefinedProperties(),   
            };
        }

        [HttpPatch("patch")]
        public object Patch([FromBody] ExampleRequest request)
        {
            var result = new Dictionary<string, object?>(_data);
            if (request.IsDefined(x => x.Field1))
            {
                result["prop1"] = request.Field1;
            }
            if (request.IsDefined(x => x.Field2))
            {
                result["prop2"] = request.Field2;
            }
            if (request.IsDefined(x => x.Field3))
            {
                result["prop3"] = request.Field3;
            }
            if (request.IsDefined(x => x.Field4))
            {
                result["prop4"] = request.Field4;
            }
            return result;
        }
    }
}
```
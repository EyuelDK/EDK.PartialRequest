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

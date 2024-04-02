
namespace EDK.PartialRequest.Example
{
    public class ExampleRequest : PartialRequest<ExampleRequest>
    {
        public string? Field1 { get; set; }
        public int? Field2 { get; set; }
        public bool? Field3 { get; set; }
        public object? Field4 { get; set; }
    }
}

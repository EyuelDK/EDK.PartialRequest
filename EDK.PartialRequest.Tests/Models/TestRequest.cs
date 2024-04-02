namespace EDK.PartialRequest.Tests
{
    public class TestRequest : PartialRequest<TestRequest>
    {
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public string? Field4 { get; set; }
        public string? Field5 { get; set; }
    }
}
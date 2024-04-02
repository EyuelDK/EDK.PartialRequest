using Newtonsoft.Json;

namespace EDK.PartialRequest.Tests
{
    public partial class JsonConverterTests
    {
        [Test]
        public void DeserializeJson_HasSomeUndefinedFields_IdentifiesUndefinedFields()
        {
            var json = """
                {
                    "field1": "value1",
                    "field2": "value2",
                    "field5": "value5"
                }
            """;
            var partialObject = JsonConvert.DeserializeObject<TestRequest>(json);
            Assert.That(partialObject, Is.Not.Null);
            var definedProperties = new string[] { 
                nameof(TestRequest.Field1), 
                nameof(TestRequest.Field2), 
                nameof(TestRequest.Field5),
            };
            Assert.That(partialObject.GetDefinedProperties(), Is.EqualTo(definedProperties));
        }

        [Test]
        public void DeserializeJson_HasNoUndefinedFields_IdentifiesNoUndefinedFields()
        {
            var json = """
                {
                    "field1": "value1",
                    "field2": "value2",
                    "field3": "value3",
                    "field4": "value4",
                    "field5": "value5"
                }
            """;
            var partialObject = JsonConvert.DeserializeObject<TestRequest>(json);
            Assert.That(partialObject, Is.Not.Null);
            var definedProperties = new string[] {
                nameof(TestRequest.Field1),
                nameof(TestRequest.Field2),
                nameof(TestRequest.Field3),
                nameof(TestRequest.Field4),
                nameof(TestRequest.Field5),
            };
            Assert.That(partialObject.GetDefinedProperties(), Is.EqualTo(definedProperties));
        }

        [Test]
        public void DeserializeJson_HasAllUndefinedFields_IdentifiesAllUndefinedFields()
        {
            var json = """
                {
                }
            """;
            var partialObject = JsonConvert.DeserializeObject<TestRequest>(json);
            Assert.That(partialObject, Is.Not.Null);
            Assert.That(partialObject.GetDefinedProperties(), Is.Empty);
        }
    }
}
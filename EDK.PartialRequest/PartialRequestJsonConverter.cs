using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace EDK.PartialRequest
{
    public class PartialRequestJsonConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return PartialRequestUtils.IsPartialRequest(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            // mark defined fields
            var root = JObject.Load(reader);
            var obj = Activator.CreateInstance(objectType);
            foreach (var prop in objectType.GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(prop, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;
                var jsonPropertyName = attribute == null ? prop.Name : attribute.PropertyName;
                if (jsonPropertyName != null)
                {
                    if (root.TryGetValue(jsonPropertyName, StringComparison.OrdinalIgnoreCase, out var jsonPropertyValue))
                    {
                        var val = jsonPropertyValue.ToObject(prop.PropertyType, serializer);
                        prop.SetValue(obj, val);
                        var methodFlags = BindingFlags.NonPublic | BindingFlags.Instance;
                        var methodName = nameof(PartialRequest<object>.AddDefinedProperty);
                        var method = objectType.GetMethod(methodName, methodFlags);
                        method.Invoke(obj, new object[] { prop.Name });
                    }
                }
            }
            return obj;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

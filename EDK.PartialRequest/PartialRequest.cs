using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EDK.PartialRequest
{
    [JsonConverter(typeof(PartialRequestJsonConverter))]
    public class PartialRequest<T> where T : class
    {
        private readonly ISet<string> _definedProperties = new HashSet<string>();

        public bool IsDefined(string propertyName)
        {
            return _definedProperties.Contains(propertyName);
        }

        public bool IsDefined<TReturn>(Expression<Func<T, TReturn>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return IsDefined(memberExpression.Member.Name);
            }
            else
            {
                throw new ArgumentException("Invalid expression. Must be a member access lambda expression.");
            }
        }

        public string[] GetDefinedProperties()
        {
            return _definedProperties.ToArray();
        }

        internal void AddDefinedProperty(string propertyName)
        {
            _definedProperties.Add(propertyName);
        }
    }
}

using System;

namespace EDK.PartialRequest
{
    public static class PartialRequestUtils
    {
        public static bool IsPartialRequest<T>()
        {
            return IsPartialRequest(typeof(T));
        }

        public static bool IsPartialRequest(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(PartialRequest<>);
        }
    }
}

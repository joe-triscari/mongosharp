using System;
using System.Linq;
using MongoDB.Bson;

namespace MongoSharp.Model
{
    public static class TypeExtensions
    {
        public static bool IsSimpleType(
            this Type type)
        {
            if (type.IsSubclassOf(typeof(BsonValue)) && type != typeof(BsonDocument))
                return true;

            return
                type.IsValueType ||
                type.IsPrimitive ||
                new []
                    {
                        typeof (String),
                        typeof (Decimal),
                        typeof (DateTime),
                        typeof (DateTimeOffset),
                        typeof (TimeSpan),
                        typeof (Guid),
                        typeof (BsonValue)
                    }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}

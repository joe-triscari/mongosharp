using System;

namespace MongoSharp.Model
{
    public class PropertyData
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool IsNullable
        {
            get { return Type.IsGenericType && Type.GetGenericTypeDefinition() == typeof (Nullable<>); }
        }

        public Type GetUnderLyingType()
        {
            return IsNullable ? Type.GetGenericArguments()[0] : Type;
        }

        public string FriendlyTypeName
        {
            get { return IsNullable ? GetUnderLyingType().Name + "?" : Type.Name; }
        }

        public override string ToString()
        {
            return String.Format("{0} (Type: {1})", Path, FriendlyTypeName);
        }
    }
}

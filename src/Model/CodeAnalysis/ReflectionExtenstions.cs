using System;
using System.Linq;
using System.Reflection;

namespace MongoSharp.Model.CodeAnalysis
{
    public static class ReflectionExtensions
    {
        public static string MethodSignature(this MethodInfo mi)
        {
            string[] param = mi.GetParameters()
                          .Select(p => $"{p.ParameterType.Name} {p.Name}")
                          .ToArray();

            string signature = $"{mi.ReturnType.Name} {mi.Name}({string.Join(",", param)})";

            return signature;
        }

        public static string ConstructorSignature(this ConstructorInfo ci)
        {
            string[] param = ci.GetParameters()
                          .Select(p => $"{p.ParameterType.Name} {p.Name}")
                          .ToArray();

            string signature = $"{ci.Name}({string.Join(",", param)})";

            return signature;
        }
    }
}

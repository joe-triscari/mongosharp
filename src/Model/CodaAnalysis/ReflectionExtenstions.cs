using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MongoSharp.Model.CodaAnalysis
{
    public static class ReflectionExtensions
    {
        public static string MethodSignature(this MethodInfo mi)
        {
            String[] param = mi.GetParameters()
                          .Select(p => String.Format("{0} {1}", p.ParameterType.Name, p.Name))
                          .ToArray();

            string signature = String.Format("{0} {1}({2})", mi.ReturnType.Name, mi.Name, String.Join(",", param));

            return signature;
        }

        public static string ConstructorSignature(this ConstructorInfo ci)
        {
            String[] param = ci.GetParameters()
                          .Select(p => String.Format("{0} {1}", p.ParameterType.Name, p.Name))
                          .ToArray();

            string signature = String.Format("{0}({1})", ci.Name, String.Join(",", param));

            return signature;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MongoSharp.Model.CodaAnalysis
{
    public class TypeHelper
    {
        private static Dictionary<string, Type> _typeCache = new Dictionary<string,Type>();

        public bool TryFindType(string typeName, out Type t)
        {
            lock (_typeCache)
            {
                //System.Collections.Generic.IEnumerable<int> df;
                //AppDomain.CurrentDomain.Load("System.Collections.Generic");
                //var asses = AppDomain.CurrentDomain.GetAssemblies();
                //var xx = (from asm in asses
                //          from type in asm.GetTypes()
                //          where type.IsClass && type.GetInterfaces().Any(intf => intf.IsGenericType)                          
                //          select type).ToList();
                //var xxx = xx.Where(x => x.FullName.Contains("IEnumerable")).ToList();

                if (!_typeCache.TryGetValue(typeName, out t))
                {
                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        t = a.GetType(typeName);                        
                        if (t != null)
                            break;
                    }
                    _typeCache[typeName] = t; // perhaps null
                }
            }
            return t != null;
        }
    }
}

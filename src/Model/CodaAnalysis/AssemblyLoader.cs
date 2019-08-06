using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MongoSharp.Model.CodaAnalysis
{
    public class AssemblyLoader
    {
        static private Dictionary<string, Assembly>  _assemblies = new Dictionary<string, Assembly>();

        static AssemblyLoader()
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                _assemblies.Add(a.FullName, a);
            }
        }

        public Assembly GetAssembly(string name)
        {
            Assembly a;

            if (_assemblies.TryGetValue(name, out a))
                return a;

            a = Assembly.Load(name);

            return a;
        }
    }
}

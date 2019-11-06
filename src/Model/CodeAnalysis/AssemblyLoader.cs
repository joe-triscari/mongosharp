using System;
using System.Collections.Generic;
using System.Reflection;

namespace MongoSharp.Model.CodeAnalysis
{
    public class AssemblyLoader
    {
        private static readonly Dictionary<string, Assembly>  _assemblies = new Dictionary<string, Assembly>();

        static AssemblyLoader()
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                _assemblies.Add(a.FullName, a);
            }
        }

        public Assembly GetAssembly(string name)
        {
            if (_assemblies.TryGetValue(name, out var a))
                return a;

            a = Assembly.Load(name);

            return a;
        }
    }
}

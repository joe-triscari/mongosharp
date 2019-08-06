using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSharp
{
    public class CustomDataTable : System.Data.DataTable
    {
        public object OriginalObject { get; set; }
        public bool IsLoaded { get; set; }
    }
}

namespace MongoSharp
{
    public class CustomDataTable : System.Data.DataTable
    {
        public object OriginalObject { get; set; }
        public bool IsLoaded { get; set; }
    }
}

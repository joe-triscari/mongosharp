namespace MongoSharp.Model.CodeGen
{
    public class CodeGenModel
    {
        public MongoDatabaseInfo MongoDatabaseInfo { get; set; }
        public MongoCollectionInfo MongoCollectionInfo { get; set; }
        public string LinqQuery { get; set; }
        public string Mode { get; set; }
        public string ModelType { get; set; }
    }
}
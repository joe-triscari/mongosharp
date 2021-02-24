namespace MongoSharp.Model.CodeGen
{
    public partial class MongoCodeBehindGen
    {
        public MongoCodeBehindGen(CodeGenModel model)
        {
            Model = model;
        }

        public CodeGenModel Model { get; private set; }
    }
}
namespace MongoSharp.Model.Interface
{
    public interface IOutputWindow
    {
        string Output { get; set; }
        void AppendOutput(string text);
        void Show();
    }
}

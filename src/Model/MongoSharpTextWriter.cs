using System.ComponentModel;
using System.Text;

namespace MongoSharp.Model
{
    public class MongoSharpTextWriter : System.IO.TextWriter
    {
        private readonly BackgroundWorker _backgroundWorker;
        public MongoSharpTextWriter(BackgroundWorker bgw)
        {
            _backgroundWorker = bgw;
        }

        public override Encoding Encoding => Encoding.Unicode;

        public override void Write(char value)
        {
            _backgroundWorker?.ReportProgress(0, value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if (index < 0)
                return;
            if (count < 0)
                return;
            if (buffer.Length - index < count)
                return;

            string value = new string(buffer).Substring(index, count);
            _backgroundWorker?.ReportProgress(0, value);
        }

        public override void WriteLine(string value)
        {
            if (value == null)
            {
                WriteLine();
            }
            else
            {
                _backgroundWorker?.ReportProgress(0, value);
            }
        }
    }
}

using System.ComponentModel;
using System.Text;
using MongoSharp.Model.Interface;

namespace MongoSharp.Model
{
    public class MongoSharpTextWriter : System.IO.TextWriter
    {
        private readonly BackgroundWorker _backgroundWorker;
        public MongoSharpTextWriter(BackgroundWorker bgw)
        {
            _backgroundWorker = bgw;
        }

        public override Encoding Encoding
        {
            get { return System.Text.UTF8Encoding.Unicode; }
        }

        public override void Write(char value)
        {
            if (_backgroundWorker != null)
                _backgroundWorker.ReportProgress(0, value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if (buffer == null)
                return;
            if (index < 0)
                return;
            if (count < 0)
                return;
            if (buffer.Length - index < count)
                return;

            string value = new string(buffer).Substring(index, count);
            if (_backgroundWorker != null)
                _backgroundWorker.ReportProgress(0, value);
        }

        public override void WriteLine(string value)
        {
            if (value == null)
            {
                WriteLine();
            }
            else
            {
                if (_backgroundWorker != null)
                    _backgroundWorker.ReportProgress(0, value);
            }
        }
    }
}

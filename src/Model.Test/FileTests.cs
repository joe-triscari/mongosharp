using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoSharp.Model;

namespace Model.Test
{
    [TestClass]
    public class FileTests
    {
        [TestClass]
        public class ToSaveFormat
        {
            [TestMethod]
            public void ShouldConvertObjectToFileFormatThatGetsSavedToDisk()
            {
                var linqQueryFile = new MongoSharpFile
                    {
                        ConnectionName = "Conn1",
                        DatabaseName = "db1",
                        CollectionName = "Col1",
                        QueryMode = "Statements",
                        QueryText = "from x in collection select x"
                    };
                string saveFormat = linqQueryFile.ToSaveFormat();

                string expected = "<Settings ConnectionName='Conn1' DatabaseName='db1' CollectionName='Col1' Mode='Statements'/>" 
                    + "from x in collection select x";
                Assert.AreEqual(expected, linqQueryFile.ToSaveFormat());
            }
        }

        [TestClass]
        public class FromSaveFile
        {
            [TestMethod]
            public void ShouldParseFileIntoProperties()
            {
                string savedFile = "<Settings ConnectionName='Conn1' DatabaseName='db1' CollectionName='Col1' Mode='Statements'/>"
                    + "from x in collection select x";
                var linqQueryFile = new MongoSharpFile();
                linqQueryFile.FromSaveFormat(savedFile);
            }
        }
    }
}

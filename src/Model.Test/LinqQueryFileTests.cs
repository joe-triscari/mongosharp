using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoSharp.Model;
using NUnit.Framework;

namespace Model.Test
{
    [TestFixture]
    public class LinqQueryFileTests
    {
        [TestFixture]
        public class ToSaveFormat
        {
            [Test]
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

        [TestFixture]
        public class FromSaveFile
        {
            [Test]
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

using System.Linq;
using System.Text;
using System.Xml;

namespace MongoSharp.Model
{
    public class MongoSharpFile
    {
        public string ConnectionName { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string QueryMode { get; set; }
        public string QueryText { get; set; }

        public MongoSharpFile() { }
        public MongoSharpFile(string contents)
        {
            FromSaveFormat(contents);
        }

        public string ToSaveFormat()
        {
            string xml =
                $"<Settings ConnectionName='{ConnectionName}' DatabaseName='{DatabaseName}' CollectionName='{CollectionName}' Mode='{QueryMode}'/>";
            return xml + "<![CDATA[" + QueryText + "]]>";
        }

        public void FromSaveFormat(string contents)
        {
            ConnectionName = "";
            DatabaseName = "";
            CollectionName = "";
            QueryMode = "";

            var xmlDoc = ConvertToXmlDocument(contents);
            if (xmlDoc != null && xmlDoc.DocumentElement.Name.ToLower() == "query")
            {
                QueryText = xmlDoc.DocumentElement.InnerText;

                var dataNode = (from XmlNode x in xmlDoc.DocumentElement.ChildNodes
                                where x.Name == "Settings"
                                select x).FirstOrDefault();
                if (dataNode != null)
                {
                    ConnectionName = dataNode.Attributes["ConnectionName"].InnerText;
                    DatabaseName = dataNode.Attributes["DatabaseName"].InnerText;
                    CollectionName = dataNode.Attributes["CollectionName"].InnerText;
                    QueryMode = dataNode.Attributes["Mode"].InnerText;
                    if(QueryMode.ToLower().Contains("statements") && !QueryMode.ToLower().Contains("c$"))
                    {
                        QueryMode = MongoSharpQueryMode.CSharpStatements;
                    }
                }
            }
            else                
            {
                QueryText = contents;                
            }
        }

        private XmlDocument ConvertToXmlDocument(string text)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<query>");
            sb.AppendLine(text);
            sb.AppendLine("</query>");

            var xml = sb.ToString();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return xmlDocument;
        }
    }
}

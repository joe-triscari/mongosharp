using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoSharp.Model;

namespace MongoSharp.Console
{
    class Program
    {


        [BsonIgnoreExtraElements]
        public class DocKey
        {
            public string CompanyCode { get; set; }
            public string DocumentCode { get; set; }
            public string DocumentNumber { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class PostedDocKey
        {
            public string CompanyCode { get; set; }
            public string DocumentCode { get; set; }
            public string DocumentNumber { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class InputResult
        {
            public DocKey DocKey { get; set; }
            public int? FinalDestination { get; set; }
            public string Messages { get; set; }
            public PostedDocKey PostedDocKey { get; set; }
            public int? PostingDestination { get; set; }
            public bool? WasSuccessfull { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class Header
        {
            public string Authoriser { get; set; }
            public string CmpCode { get; set; }
            public BsonValue CrossReference { get; set; }
            public string CurCode { get; set; }
            public DateTime? Date { get; set; }
            public string Description { get; set; }
            public string DocCode { get; set; }
            public string DocNumber { get; set; }
            public DateTime? InputDate { get; set; }
            public DateTime? InputTime { get; set; }
            public int? Period { get; set; }
            public int? TimeStamp { get; set; }
            public BsonValue UserCode { get; set; }
            public int? Year { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class Currency
        {
            public string Code { get; set; }
            public BsonValue FullValue { get; set; }
            public BsonValue Rate { get; set; }
            public BsonValue Value { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class ElmQuantities4
        {
            public string Quantity1 { get; set; }
            public string Quantity2 { get; set; }
            public string Quantity3 { get; set; }
            public string Quantity4 { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class Line
        {
            public string AccountCode { get; set; }
            public BsonValue BankCode { get; set; }
            public int? CalcDisc { get; set; }
            public BsonValue Comments { get; set; }
            public Currency[] Currencies { get; set; }
            public BsonValue CustomerSupplier { get; set; }
            public string Description { get; set; }
            public BsonValue DestCode { get; set; }
            public BsonValue DisableSummaries { get; set; }
            public BsonValue Discounts { get; set; }
            public BsonValue DiscsAllowed { get; set; }
            public BsonValue DocRate { get; set; }
            public BsonValue DocSumTax { get; set; }
            public BsonValue DocTaxTurnover { get; set; }
            public string DocValue { get; set; }
            public BsonValue DualFullValue { get; set; }
            public BsonValue DualRate { get; set; }
            public BsonValue DualValue { get; set; }
            public BsonValue DueDate { get; set; }
            public BsonValue DueTerms { get; set; }
            public BsonValue ElmAddrName { get; set; }
            public BsonValue ElmAddrTag { get; set; }
            public BsonValue ElmBankAccount { get; set; }
            public BsonValue ElmBankTag { get; set; }
            public BsonValue ElmQuantities1 { get; set; }
            public BsonValue ElmQuantities2 { get; set; }
            public BsonValue ElmQuantities3 { get; set; }
            public BsonValue ElmQuantities5 { get; set; }
            public BsonValue ElmQuantities6 { get; set; }
            public BsonValue ElmQuantities7 { get; set; }
            public BsonValue ElmQuantities8 { get; set; }
            public string ExtRef1 { get; set; }
            public string ExtRef2 { get; set; }
            public string ExtRef3 { get; set; }
            public string ExtRef4 { get; set; }
            public string ExtRef5 { get; set; }
            public BsonValue ExtRef6 { get; set; }
            public BsonValue HomeFullValue { get; set; }
            public BsonValue HomeSumTax { get; set; }
            public BsonValue HomeValue { get; set; }
            public int? LineNumber { get; set; }
            public int? LineSense { get; set; }
            public int? LineType { get; set; }
            public BsonValue MatchableElmLevel { get; set; }
            public BsonValue MediaCode { get; set; }
            public BsonValue ParentCurrency { get; set; }
            public BsonValue ParentFullValue { get; set; }
            public BsonValue ParentRate { get; set; }
            public BsonValue ParentValue { get; set; }
            public BsonValue PartPayment { get; set; }
            public BsonValue PayDate { get; set; }
            public BsonValue PayNumber { get; set; }
            public int? PayStatus { get; set; }
            public BsonValue RecNumber { get; set; }
            public BsonValue RecStatus { get; set; }
            public BsonValue TaxInclusive { get; set; }
            public BsonValue TaxLineCode { get; set; }
            public int? TaxMethod { get; set; }
            public BsonValue Taxes { get; set; }
            public BsonValue Ten99Taxes { get; set; }
            public BsonValue TimeStamp { get; set; }
            public BsonValue TraderCode { get; set; }
            public BsonValue UserRef1 { get; set; }
            public BsonValue UserRef2 { get; set; }
            public BsonValue UserRef3 { get; set; }
            public BsonValue UserStatus { get; set; }
            public BsonValue ValueDate { get; set; }
            public BsonValue ValueTerms { get; set; }
            public ElmQuantities4 ElmQuantities4 { get; set; }
        }

        [BsonIgnoreExtraElements]
        public class DocumentTransaction
        {
            public Header Header { get; set; }
            public Line[] Lines { get; set; }
            public int[] Lines2 { get; set; }


        }

        [BsonIgnoreExtraElements]
        public class Doc0
        {
            public string _id { get; set; }
            public string ImportId { get; set; }
            public string CompanyCode { get; set; }
            public string DocumentCode { get; set; }
            public string DocumentNumber { get; set; }
            public DateTime? CreatedDate { get; set; }
            public InputResult InputResult { get; set; }
            public DocumentTransaction DocumentTransaction { get; set; }
        }

        public class MongoSharpTextWriter : System.IO.TextWriter
        {
            public override Encoding Encoding
            {
                get { return System.Text.UTF8Encoding.Unicode; }
            }

            public override void Write(char value)
            {

            }
        }

        static void Main()
        {
            try
            {
                System.Console.SetOut(null);

                var ass2 = Assembly.Load("System.Core");

                var ass = new MongoSharp.Model.CodaAnalysis.AssemblyLoader().GetAssembly("System.Core");
            }
            catch(Exception e)
            {
                string sdf = e.Message;
            }
            

            var d = ObjectHelper.ToPropertyPaths(typeof (Doc0));
            var sb = new StringBuilder();
            foreach (PropertyData propertyPath in d)
            {
                string prop = String.Format("{0}\t{1}", propertyPath.Path, propertyPath.FriendlyTypeName);
                sb.AppendLine(propertyPath.ToString());
            }
            var props = sb.ToString();

            //String joe = "cool";
            //string r = joe.PadRight(12);
            //string l = joe.PadLeft(12);
            //var props = joe.GetType().GetProperties();
            //var sim = joe.GetType().IsSimpleType();
            //var xx = new {Test = "asdf"};
            //var sim2 = xx.GetType().IsSimpleType();



            //TestMongo();
            //var ba = new BsonArray(new [] {1,2,3,4,5,6});
            //var j = Helper.JoinBsonArray(ba);

            //var conn = new MongoConnectionInfo
            //    {
            //        Username = "accounting_",
            //        Password = "4PWMnQky",
            //        ServerString = "qgmongodata1:30100,qgmongodata2:30100",
            //        Database = "accounting",
            //        CollectionName = "JournalImports"
            //    };
            //const string linqQuery = 
            //    "from x in collection where x.IsError == false select new {x.UserName, x.LineCount, x.Model.Year, x.Model.Period}";

            //string code = new MongoCodeGenerator().GenerateMongoCode(conn, linqQuery, "Single Query");
            //List<QueryResult> stuff = new MongoDynamicCodeRunner().CompileAndRun(code);

            //var rows = "";
            //foreach (var thing in stuff[0].Results)
            //{
            //    string row = thing.Aggregate("", (current, value) => current + (value == null ? "NULL" : value.ToString()) + "\t");
            //    rows += row + "\r\n";
            //}

            //System.Console.WriteLine(rows);
        }

        static readonly List<QueryResult> _queryResults = new List<QueryResult>();
        static void Dump(IEnumerable<Object> results)
        {
            var queryResult = new QueryResult();
            var list = results.ToList();

            queryResult.Results = list.Select(result => result.GetType().GetProperties().Select(p => (Object)p.GetValue(result)).ToList()).ToList();
            if (list.Any())
            {
                var firstObj = list.First();
                //queryResult.Properties = firstObj.GetType().GetProperties().Select(x => x.Name).ToList();
            }

            _queryResults.Add(queryResult);
        }

    }
}

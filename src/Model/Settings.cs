using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Drawing;

namespace MongoSharp.Model
{
    [Serializable]
    public class Settings
    {
        [XmlIgnore]
        private static Settings _instance;

        private Settings()
        {
            Connections = new List<MongoConnectionInfo>();
            RecentlyUsed = new List<string>();
            Preferences = new Preferences();
            CodeSnippets = null;            
        }

        public List<MongoConnectionInfo> Connections { get; set; }
        public List<string> RecentlyUsed { get; set; }
        public Preferences Preferences { get; set; }
        public List<CodeSnippet> CodeSnippets { get; set; }

        [XmlIgnore]
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                       "MongoSharp");
                    var settingsFile = Path.Combine(appData, "settings.xml");

                    _instance = File.Exists(settingsFile)
                                    ? new Serializer().DeSerializeObject<Settings>(settingsFile)
                                    : new Settings();

                    foreach (MongoConnectionInfo connection in _instance.Connections)
                    {
                        foreach (MongoDatabaseInfo database in connection.Databases)
                        {
                            database.Connection = connection;
                            foreach (var collection in database.Collections)
                            {
                                collection.Database = database;
                                foreach (var model in collection.Models)
                                {
                                    model.Collection = collection;
                                }
                            }
                        }
                    }

                    if(_instance.CodeSnippets == null)
                    {
                        _instance.CodeSnippets = new List<CodeSnippet>();
                        _instance.CodeSnippets.Add(new CodeSnippet
                        {
                            Name = "group by",
                            Description = "template for creating group by queries",
                            Code = @"// Make sure to switch to STATEMENTS mode.
var ungrouped = (from x in collection.AsQueryable()
                   //where x.
                 select x).ToList();

var grouped = (from x in ungrouped
               group x by new
               {
               } into grp
               select new
               {
               }).ToList();

Dump(grouped);"
                        });
                    }
                }
                return _instance;
            }
        }

        public string GetSettingsFile()
        {
            var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                       "MongoSharp");
            var settingsFile = Path.Combine(appData, "settings.xml");
            return settingsFile;
        }

        public void Clean()
        {
            foreach(var connection in Instance.Connections)
            {
                foreach (var database in connection.Databases)
                    database.Collections.RemoveAll(x => !x.IsValid());

                connection.Databases.RemoveAll(x => !x.IsValid());
            }
            Save();
        }

        public void Save()
        {
            var appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                       "MongoSharp");
            if (!Directory.Exists(appData))
            {
                Directory.CreateDirectory(appData);
            }

            var settingsFile = Path.Combine(appData, "settings.xml");
            new Serializer().SerializeObject(settingsFile, Instance);
        }
    
        public void AddRecentlyUsed(string file)
        {
            RecentlyUsed.RemoveAll(f => f.Equals(file, StringComparison.CurrentCultureIgnoreCase));
            RecentlyUsed.Insert(0, file);
            if (RecentlyUsed.Count > 10)
            {
                RecentlyUsed = RecentlyUsed.Take(10).ToList();
            }
        }
    }

    public interface IEditorPreferences
    {
        string EditorSyntaxLanguage { get; set; }
        decimal EditorFontSize { get; set; }
        Color EditorBackColor { get; set; }
        bool ShowEditorLineNumbers { get; set; }
    }

    [Serializable]
    public class Preferences : IEditorPreferences
    {
        public Preferences()
        {
            EditorBackColor = Color.White;
            EditorSyntaxLanguage = "cs";
        }

        public string EditorSyntaxLanguage { get; set; }
        public decimal EditorFontSize { get; set; }
        [XmlIgnore]
        public Color EditorBackColor { get; set; }
        [XmlElement("EditorBackColor")]
        public int EditorBackColorAsArgb
        {
            get => EditorBackColor.ToArgb();
            set => EditorBackColor = Color.FromArgb(value);
        }

        public SerializableFont ResultGridFont { get; set; }

        public bool OutputShowTimestamp { get; set; }
        public string OutputTimestampFormat { get; set; }
        public bool OutputClearOnExecute { get; set; }

        public bool AllowAutoGeneratedModels { get; set; }
        public bool ShowEditorLineNumbers { get; set; }
        public int DefaultResultsView { get; set; }

        public Preferences Clone()
        {
            return new Preferences
                {
                    EditorSyntaxLanguage = EditorSyntaxLanguage,
                    EditorBackColorAsArgb = EditorBackColorAsArgb,
                    EditorFontSize = EditorFontSize,
                    ResultGridFont = ResultGridFont == null ? null : ResultGridFont.Clone(),
                    OutputShowTimestamp = OutputShowTimestamp,
                    OutputTimestampFormat = OutputTimestampFormat,
                    OutputClearOnExecute = OutputClearOnExecute,
                    AllowAutoGeneratedModels = AllowAutoGeneratedModels,
                    ShowEditorLineNumbers = ShowEditorLineNumbers,
                    DefaultResultsView = DefaultResultsView
                };
        }
    }

}

using ScintillaNET;

namespace MongoSharp
{
    public static class Extensions
    {
        public static void SetLanguage(this Scintilla scintilla, string lang)
        {
            scintilla.ConfigurationManager.Language = lang;
            scintilla.Lexing.Keywords[0] += " var select orderby from group into where";
            scintilla.Lexing.Keywords[1] += " Dump Print client server database collection Console";
        }

        public static void ShowLineNumbers(this Scintilla scintilla, bool show)
        {
            if (show)
            {
                scintilla.Margins[0].Width = 20;
                scintilla.Caret.Style = CaretStyle.Line;
            }
            else
                scintilla.Margins[0].Width = 0;
        }

        public static bool IsShowingLineNumbers(this Scintilla scintilla)
        {
            return scintilla.Margins[0].Width == 20;
        }
    }
}

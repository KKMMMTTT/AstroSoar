using System;
using System.IO;

namespace DefinitionTool
{
    public class Globals
    {
        private static string _definitionPath;
        public static string DefinitionPath {
            get => _definitionPath;
        }

        public static MainWindow MainWindow;
        public static MainDefinitionEditor QuestlineEditor;
        public static MainDefinitionEditor ConversationEditor;

        [STAThread]
        public static void Main(string[] args) {
            MainWindow = new MainWindow();
            _definitionPath = Path.Combine(Annex.Paths.SolutionFolder, "assets/definitions/");
            MainWindow.ShowDialog();
        }
    }
}

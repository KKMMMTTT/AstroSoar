using Game.Definitions.Questlines;
using Game.Questlines;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuestlineDefinitionTool
{
    public class Globals
    {
        public static List<QuestlineDefinition> QuestlineDefinitions = new List<QuestlineDefinition>();

        private static string _definitionPath;
        public static string DefinitionPath {
            get => _definitionPath;
            set {
                _definitionPath = value;
                OnDefinitionDirectoryChanged(DefinitionPath);
                MainWindow.lblFolderLocation.Content = $"{DefinitionPath}";
            }
        }

        public static MainWindow MainWindow;

        private static void OnDefinitionDirectoryChanged(string path) {
            Directory.CreateDirectory(path);

            foreach (var definition in Directory.GetFiles(path)) {
            }
        }


        [STAThread]
        public static void Main(string[] args) {
            MainWindow = new MainWindow();
            DefinitionPath = Path.Combine(Annex.Paths.SolutionFolder, "assets/definitions/questline");
            MainWindow.ShowDialog();
        }
    }
}

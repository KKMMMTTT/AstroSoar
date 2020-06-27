using Annex;
using Annex.Assets;
using Game.Questlines;
using Game.Scenes;
using Game.Scenes.MainMenu;
using System.IO;
using System.Text;
using static Annex.Paths;

namespace Game
{
    public static class Program
    {
        public static QuestlineJournal Journal = new QuestlineJournal();

        private static void Main(string[] args) {
            AnnexGame.Initialize();
            Debug.PackageAssetsToBinaryFrom(AssetType.Texture, Path.Combine(SolutionFolder, "assets/textures/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Audio, Path.Combine(SolutionFolder, "assets/music/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "assets/font/"));

            Debug.AddDebugOverlayCommand("signal", args => {
                string flag = args[0];
                int increment = int.Parse(args[1]);
                Journal.SignalProgress(flag, increment);
            });

            Debug.AddDebugOverlayCommand("start", args =>
            {
                string name = args[0];
                Journal.GetQuestline(name).StartQuestline();
            });

            Debug.AddDebugOverlayInformation(() => {
                var quest = Journal.GetQuestline("test");
                var sb = new StringBuilder();

                sb.AppendLine($"State: {quest.State.ToString()}");

                if (quest.State != QuestlineProgressState.Finished) {
                    foreach (var entry in quest.CurrentStep.Tasks) {
                        char group = entry.Key;
                        var tasks = entry.Value;

                        foreach (var task in tasks) {
                            sb.AppendLine($"Group:{group}\tTask:{task.Flag}\tGoal:{task.Goal}\tCount:{task.Count}");
                        }
                    }
                }
                return sb.ToString();
            });

            AnnexGame.Start<MainMenu>();
        }
    }
}

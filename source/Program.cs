using Annex;
using Annex.Assets;
using Game.Questlines;
using Game.Scenes.Demo.Conversations;
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

            Debug.AddDebugOverlayCommand("start", args => {
                string name = args[0];
                Journal.GetQuestline(name).StartQuestline();
            });

            Debug.AddDebugOverlayCommand("convo", args => {
                ServiceProvider.SceneService.LoadNewScene<ConversationScene>();
            });

            Debug.AddDebugOverlayInformation(() => {
                var sb = new StringBuilder();
                foreach (var entry in Journal.AllQuestlines) {
                    string name = entry.Key;
                    var quest = entry.Value;

                    sb.AppendLine($"Quest:{name}\tState:{quest.State.ToString()}\t");
                    sb.AppendLine($"{quest.Description}");
                    sb.AppendLine();

                    if (quest.State == QuestlineProgressState.InProgress) {
                        sb.AppendLine($"Step: {quest.CurrentStep!.Description}");
                        sb.AppendLine();
                        foreach (var taskEntry in quest.CurrentStep!.Tasks) {
                            char group = taskEntry.Key;
                            var tasks = taskEntry.Value;

                            foreach (var task in tasks) {
                                sb.AppendLine($"Description:{task.Description}");
                                sb.AppendLine($"Group:{group}\tTask:{task.Flag}\tGoal:{task.Goal}\tCount:{task.Count}");
                                sb.AppendLine();
                            }
                        }
                    }

                    sb.AppendLine();
                    sb.AppendLine();
                }
                return sb.ToString();
            });

            AnnexGame.Start<MainMenu>();
        }
    }
}

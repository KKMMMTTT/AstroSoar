using Annex;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using Game.Entities;
using Game.Questlines;
using System.Text;

namespace Game.Scenes.Demo
{
    public class QuestlineDemoScene : Scene, ISceneWithPlayer
    {
        private static QuestlineJournal Journal => (ServiceProvider.SceneService.CurrentScene as ISceneWithPlayer)!.Player!.QuestlineJournal;

        public Player? Player { get; set; }

        static QuestlineDemoScene() {
            Debug.AddDebugOverlayCommand("signal", args => {
                string flag = args[0];
                int increment = int.Parse(args[1]);

                AstroSoarServiceProvider.FlagHandlerService.SignalProgress(flag, increment);
            });

            Debug.AddDebugOverlayCommand("start", args => {
                string name = args[0];
                Journal.GetQuestline(name).StartQuestline();
            });

            Debug.AddDebugOverlayInformation(() => {
                var sb = new StringBuilder();
                foreach (var entry in Journal.AllQuestlines) {
                    string name = entry.Key;
                    var quest = entry.Value;

                    sb.AppendLine($"Quest:{name}\tState:{quest.State}\t");
                    sb.AppendLine($"{quest.Description}");
                    sb.AppendLine();

                    if (quest.State == QuestlineProgressState.InProgress) {
                        sb.AppendLine($"Step: {quest.CurrentStep!.Description}");
                        sb.AppendLine();
                        foreach (var taskEntry in quest.CurrentStep!.TaskGroups) {
                            string group = taskEntry.Key;
                            var taskgroup = taskEntry.Value;

                            foreach (var task in taskgroup.Tasks) {
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
        }

        public QuestlineDemoScene() {
            this.Player = new Player();
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (e.Key == Annex.Scenes.KeyboardKey.Tilde) {
                Debug.ToggleDebugOverlay();
                return;
            }
            base.HandleKeyboardKeyPressed(e);
        }
    }
}

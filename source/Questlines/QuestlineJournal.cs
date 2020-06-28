using System.Collections.Generic;
using System.Linq;

namespace Game.Questlines
{
    public class QuestlineJournal
    {
        private readonly Dictionary<string, QuestlineProgression> _progress;
        public IEnumerable<KeyValuePair<string, QuestlineProgression>> AllQuestlines => this._progress;

        public QuestlineJournal() {
            this._progress = new Dictionary<string, QuestlineProgression>();

            foreach (var entry in AstroSoarServiceProvider.QuestlineService.LoadAll()) {
                this._progress.Add(entry.Name, new QuestlineProgression(entry));
            }
        }

        public QuestlineProgression GetQuestline(string questlineName) {
            return this._progress[questlineName];
        }

        public void SignalProgress(string flag, long increment) {
            foreach (var entry in this._progress.Values.Where(questline => questline.State == QuestlineProgressState.InProgress)) {
                if (entry.SignalProgress(flag, increment)) {
                    if (entry.CurrentStep == null) {
                        entry.FinishQuestline();
                    }
                }
            }
        }
    }
}
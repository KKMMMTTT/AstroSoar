using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Game.Questlines
{
    [Serializable]
    public class QuestlineJournal
    {
        private Dictionary<string, QuestlineProgression> _progress;

        [JsonIgnore]
        public IEnumerable<KeyValuePair<string, QuestlineProgression>> AllQuestlines => this._progress;
        
        public Dictionary<string, QuestlineProgression> SERIALIZATION_ENTRY_QUESTLINE_PROGRESS {
            get => this._progress;
            set => this._progress = value;
        }

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
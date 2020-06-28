using Game.Definitions.Questlines;
using System.Collections.Generic;
using System.Linq;

namespace Game.Questlines
{
    public enum QuestlineProgressState
    {
        Unstarted,
        InProgress,
        Finished
    }

    public class QuestlineProgression
    {
        public QuestlineProgressState State { get; private set; }
        private Queue<StepProgression> _remainingSteps;
        public readonly string Description;

        public StepProgression? CurrentStep => _remainingSteps.Count != 0 ? _remainingSteps.Peek() : null;
        
        public QuestlineProgression(QuestlineDefinition questline) {
            this.State = QuestlineProgressState.Unstarted;
            this._remainingSteps = new Queue<StepProgression>();
            this.Description = questline.Description!;

            foreach (var step in questline.Steps) {
                this._remainingSteps.Enqueue(new StepProgression(step));
            }
        }

        public bool SignalProgress(string flag, long increment) {
            if (CurrentStep!.SignalProgress(flag, increment)) {
                this._remainingSteps.Dequeue();
                return true;
            }
            return false;
        }

        public void StartQuestline() {
            this.State = QuestlineProgressState.InProgress;
        }

        public void FinishQuestline() {
            this.State = QuestlineProgressState.Finished;
        }
    }

    public class StepProgression
    {
        private Dictionary<char, IEnumerable<TaskProgression>> _tasks;
        public IEnumerable<KeyValuePair<char, IEnumerable<TaskProgression>>> Tasks => this._tasks;
        public readonly string Description;

        public StepProgression(StepDefinition step) {
            this.Description = step.Description!;
            this._tasks = new Dictionary<char, IEnumerable<TaskProgression>>();
            foreach (var task in step.Task) {
                if (!this._tasks.ContainsKey(task.Group)) {
                    this._tasks[task.Group] = new List<TaskProgression>();
                }
                (this._tasks[task.Group] as IList<TaskProgression>)!.Add(new TaskProgression(task));
            }
        }

        public bool SignalProgress(string flag, long increment) {
            var groups = string.Join("", this._tasks.Select(entry => entry.Key.ToString()));

            foreach (var group in groups) {

                if (!this._tasks.ContainsKey(group)) {
                    continue;
                }

                var newLst = new List<TaskProgression>();

                foreach (var entry in this._tasks[group]) {
                    if (!entry.SignalProgress(flag, increment)) {
                        newLst.Add(entry);
                    }
                }

                this._tasks[group] = newLst;
            }

            return this._tasks.Any(entry => entry.Value.Count() == 0);
        }
    }

    public class TaskProgression
    {
        public readonly string Flag;
        public readonly long Goal;
        public long Count;
        public readonly string Description;

        public TaskProgression(TaskDefinition task) {
            this.Description = task.Description;
            this.Flag = task.Flag;
            this.Goal = task.Goal;
            this.Count = 0;
        }

        public bool SignalProgress(string flag, long increment) {
            if (flag == this.Flag) {
                this.Count += increment;

                if (this.Count == this.Goal) {
                    return true;
                }
            }
            return false;
        }
    }
}
using Game.Definitions.Questlines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Game.Questlines
{
    public enum QuestlineProgressState
    {
        Unstarted,
        InProgress,
        Finished
    }

    [Serializable]
    public class QuestlineProgression
    {
        private QuestlineProgressState _state;
        private Queue<StepProgression> _remainingSteps;
        private string _description;

        public string SERIALZIATION_DESCRIPTION { get => this.Description; set => this._description = value; }
        public QuestlineProgressState SERIALIZATION_STATE { get => this.State; set => this._state = value; }
        public Queue<StepProgression> SERIALIZATION_ENTRY_REMAINING_STEPS {
            get => this._remainingSteps;
            set => this._remainingSteps = value;
        }

        [JsonIgnore]
        public StepProgression? CurrentStep => _remainingSteps.Count != 0 ? _remainingSteps.Peek() : null;
        [JsonIgnore]
        public QuestlineProgressState State => this._state;
        [JsonIgnore]
        public string Description => this._description;

        [Obsolete("Should only be used only for serialization")]
        public QuestlineProgression() {

        }

        public QuestlineProgression(QuestlineDefinition questline) {
            this._state = QuestlineProgressState.Unstarted;
            this._remainingSteps = new Queue<StepProgression>();
            this._description = questline.Description!;

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
            this._state = QuestlineProgressState.InProgress;
        }

        public void FinishQuestline() {
            this._state = QuestlineProgressState.Finished;
        }
    }

    [Serializable]
    public class StepProgression
    {
        private string _description;
        private Dictionary<string, TaskGroup> _taskGroup;

        public string SERIALIZATION_DESCRIPTION { get => this.Description; set => this._description = value; }
        public Dictionary<string, TaskGroup> SERIALIZATION_TASK_GROUPS {
            get => this._taskGroup;
            set => this._taskGroup = value;
        }

        [JsonIgnore]
        public IEnumerable<KeyValuePair<string, TaskGroup>> TaskGroups => this._taskGroup;
        [JsonIgnore]
        public string Description => this._description;

        [Obsolete("Should only be used only for serialization")]
        public StepProgression() {

        }

        public StepProgression(StepDefinition step) {
            this._description = step.Description!;
            this._taskGroup = new Dictionary<string, TaskGroup>();
            foreach (var task in step.Task) {
                if (!this._taskGroup.ContainsKey(task.Group.ToString())) {
                    this._taskGroup[task.Group.ToString()] = new TaskGroup();
                }
                this._taskGroup[task.Group.ToString()]!.Add(new TaskProgression(task));
            }
        }

        public bool SignalProgress(string flag, long increment) {
            var groups = string.Join("", this._taskGroup.Select(entry => entry.Key.ToString()));

            foreach (var group in groups) {

                if (!this._taskGroup.ContainsKey(group.ToString())) {
                    continue;
                }

                this._taskGroup[group.ToString()].Signal(flag, increment);
            }

            return this._taskGroup.Any(entry => entry.Value.Tasks.Count() == 0);
        }
    }

    [Serializable]
    public class TaskGroup
    {
        private List<TaskProgression> _tasks;

        public List<TaskProgression> SERIALIZATION_TASKS { get => this.Tasks; set => this._tasks = value; }

        [JsonIgnore]
        public List<TaskProgression> Tasks => this._tasks;

        public TaskGroup() {
            this._tasks = new List<TaskProgression>();
        }

        public void Add(TaskProgression taskProgression) {
            this.Tasks.Add(taskProgression);
        }

        internal void Signal(string flag, long increment) {
            var newLst = new List<TaskProgression>();

            foreach (var entry in this.Tasks) {
                if (!entry.SignalProgress(flag, increment)) {
                    newLst.Add(entry);
                }
            }

            this._tasks = newLst;
        }
    }

    [Serializable]
    public class TaskProgression
    {
        private string _flag;
        private long _goal;
        private long _count;
        private string _description;

        [JsonIgnore]
        public string Flag => this._flag;
        [JsonIgnore]
        public long Goal => this._goal;
        [JsonIgnore]
        public long Count => this._count;
        [JsonIgnore]
        public string Description => this._description;

        public string SERIALIZATION_FLAG { get => this.Flag; set => this._flag = value; }
        public long SERIALIZATION_GOAL { get => this.Goal; set => this._goal = value; }
        public long SERIALIZATION_COUNT { get => this.Count; set => this._count = value; }
        public string SERIALIZATION_DESCRIPTION { get => this.Description; set => this._description = value; }

        [Obsolete("Should only be used only for serialization")]
        public TaskProgression() {

        }

        public TaskProgression(TaskDefinition task) {
            this._description = task.Description;
            this._flag = task.Flag;
            this._goal = task.Goal;
            this._count = 0;
        }

        public bool SignalProgress(string flag, long increment) {
            if (flag == this.Flag) {
                this._count += increment;

                if (this.Count == this.Goal) {
                    return true;
                }
            }
            return false;
        }
    }
}
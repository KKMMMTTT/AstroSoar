using System;
using static Game.Definitions.Strings;

namespace Game.Definitions.Questlines
{
    public class StepDefinition : IDescribable
    {
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        // Array of possible tasks to complete before completing the step
        private TaskDefinition[]? _task;
        public TaskDefinition[] Task {
            get => this._task ?? Array.Empty<TaskDefinition>();
            set => this._task = value;
        }
    }
}

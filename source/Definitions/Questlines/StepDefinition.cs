using System;
using static Game.Definitions.Strings;

namespace Game.Definitions.Questlines
{
    [Serializable]
    public class StepDefinition : IDescribable
    {
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        // Array of possible tasks to complete before completing the step
        public TaskDefinition[] Task {  get;  set;  }  = Array.Empty<TaskDefinition>();
    }
}

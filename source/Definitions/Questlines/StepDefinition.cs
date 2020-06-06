using static Game.Definitions.Strings;

namespace Game.Definitions.Questlines
{
    public class StepDefinition : IDescribable
    {
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        /// <summary>
        /// Array of possible tasks to complete before completing the step
        /// </summary>
        public TaskDefinition[] Task { get; set; }
    }
}

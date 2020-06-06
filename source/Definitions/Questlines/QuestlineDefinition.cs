using Game.Definitions;
using Game.Definitions.Questlines;
using static Game.Definitions.Strings;

namespace Game.Questlines
{
    public class QuestlineDefinition : IDescribable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        /// <summary>
        /// Array of steps to be completed in linear order
        /// </summary>
        public StepDefinition[] Steps { get; set; }
    }
}

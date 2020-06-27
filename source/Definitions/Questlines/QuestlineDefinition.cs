using System;
using static Game.Definitions.Strings;

namespace Game.Definitions.Questlines
{
    [Serializable]
    public class QuestlineDefinition : IDescribable
    {
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        public string Name { get; set; } = "New Questline";

        // Array of steps to be completed in linear order
        public StepDefinition[] Steps { get; set; } = Array.Empty<StepDefinition>();
    }
}

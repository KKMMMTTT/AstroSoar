using Game.Definitions;
using Game.Definitions.Questlines;
using System;
using static Game.Definitions.Strings;

namespace Game.Questlines
{
    public class QuestlineDefinition : IDescribable
    {
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        private string? _name;
        public string Name {
            get => this._name ?? string.Empty;
            set => this._name = value;
        }

        // Array of steps to be completed in linear order
        private StepDefinition[]? _steps;
        public StepDefinition[] Steps {
            get => this._steps ?? Array.Empty<StepDefinition>();
            set => this._steps = value;
        }
    }
}

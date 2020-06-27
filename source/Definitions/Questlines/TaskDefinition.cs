using System;
using static Game.Definitions.Strings;

namespace Game.Definitions.Questlines
{
    [Serializable]
    public class TaskDefinition : IDescribable
    {
        public string? Description { get; set; }
        public string FullDescription => this.Description ?? NoDescription;

        /// Active questline task goals can be implemented using a flag and an increment.
        ///
        /// e.g.  A task to kill 5 chickens { Flag: "KILL_CHICKEN", Goal: 5 } would require you to
        ///
        ///         Signal("KILL_CHICKEN", 1)
        ///
        ///       every time a chicken is killed.
        ///
        /// e.g. A task to visit a temple once { Flag: "VISIT_TEMPLE", Goal: 1 } would require you to
        ///
        ///         Signal("VISIT_TEMPLE", 1)
        ///
        ///      every time you visit the temple.
        ///
        ///
        /// Every time you signal, check if the flag is the same as the one for the task.
        /// If it is, then the increment would get added to the active questline's sum.
        /// When the sum == Goal, you know the task is complete.
        public string Flag { get; set; } = string.Empty;
        public long Goal { get; set; }

        public char Group { get; set; }
    }
}

using System;

namespace Game.Definitions.Conversations
{
    [Serializable]
    public class ReplyOption
    {
        public string ReplyText { get; set; } = string.Empty;
        public int GoTo { get; set; } = -1;
        public string? Flag { get; set; } = null;
        public long? Increment { get; set; } = null;
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}

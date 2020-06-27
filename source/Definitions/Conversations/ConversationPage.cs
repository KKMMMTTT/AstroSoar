using System;
using System.Collections.Generic;

namespace Game.Definitions.Conversations
{
    [Serializable]
    public class ConversationPage
    {
        public string DisplayText { get; set; } = string.Empty;
        public string DisplayBoxTexture { get; set; } = string.Empty;

        public List<ReplyOption> ReplyOptions { get; set; } = new List<ReplyOption>();
    }
}

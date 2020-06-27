using System;
using System.Collections.Generic;

namespace Game.Definitions.Conversations
{
    [Serializable]
    public class ConversationDefinition
    {
        public string Name { get; set; } = "New Conversation";
        public List<ConversationPage> ConversationPages { get; set; } = new List<ConversationPage>();
    }
}

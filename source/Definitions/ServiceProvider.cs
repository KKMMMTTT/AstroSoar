using Annex;
using Game.Definitions;
using Game.Definitions.Conversations;
using Game.Definitions.Player;
using Game.Definitions.Questlines;

namespace Game
{
    public static partial class AstroSoarServiceProvider
    {
        public static ConversationService ConversationService => ServiceProvider.Locate<ConversationService>() ?? ServiceProvider.Provide<ConversationService>();
        public static QuestlineService QuestlineService => ServiceProvider.Locate<QuestlineService>() ?? ServiceProvider.Provide<QuestlineService>();
        public static DefinitionService DefinitionService => ServiceProvider.Locate<DefinitionService>() ?? ServiceProvider.Provide<DefinitionService>();
        public static PlayerDefinitionService PlayerDefinitionService => ServiceProvider.Locate<PlayerDefinitionService>() ?? ServiceProvider.Provide<PlayerDefinitionService>();
    }
}

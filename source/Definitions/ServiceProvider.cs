using Annex;

namespace Game.Definitions.Questlines
{
    public static partial class AstroSoarServiceProvider
    {
        public static QuestlineService QuestlineService => ServiceProvider.Locate<QuestlineService>() ?? ServiceProvider.Provide<QuestlineService>();
        public static DefinitionService DefinitionService => ServiceProvider.Locate<DefinitionService>() ?? ServiceProvider.Provide<DefinitionService>();
    }
}

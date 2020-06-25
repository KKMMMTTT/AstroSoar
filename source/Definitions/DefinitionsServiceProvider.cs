using Annex;

namespace Game.Definitions.Questlines
{
    public static partial class DefinitionsServiceProvider
    {
        public static QuestlineService QuestlineService => ServiceProvider.Locate<QuestlineService>() ?? ServiceProvider.Provide<QuestlineService>();
    }
}

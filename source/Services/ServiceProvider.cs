using Annex;
using Game.Services;

namespace Game
{
    public static partial class AstroSoarServiceProvider
    {
        public static GuidValidatorService GuidValidatorService => ServiceProvider.Locate<GuidValidatorService>() ?? ServiceProvider.Provide<GuidValidatorService>();
        public static FlagHandlerService FlagHandlerService => ServiceProvider.Locate<FlagHandlerService>() ?? ServiceProvider.Provide<FlagHandlerService>();
    }
}

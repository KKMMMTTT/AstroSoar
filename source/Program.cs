using Annex;
using Annex.Assets;
using Game.Scenes.Demo;
using Game.Scenes.MainMenu;
using System.IO;
using static Annex.Paths;

namespace Game
{
    public static class Program
    {

        private static void Main(string[] args) {

            AnnexGame.Initialize();
            Debug.PackageAssetsToBinaryFrom(AssetType.Texture, Path.Combine(SolutionFolder, "assets/textures/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Audio, Path.Combine(SolutionFolder, "assets/music/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "assets/font/"));

            Debug.AddDebugOverlayCommand("demo", args => {
                switch (args[0]) {
                    case "convo":
                        ServiceProvider.SceneService.LoadNewScene<ConversationDemoScene>();
                        break;
                    case "quest":
                        ServiceProvider.SceneService.LoadNewScene<QuestlineDemoScene>();
                        break;
                }
            });

            AnnexGame.Start<MainMenu>();
        }
    }
}

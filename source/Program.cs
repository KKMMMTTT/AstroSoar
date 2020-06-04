using Annex;
using Annex.Assets;
using Game.Scenes.Demo;
using Game.Scenes.World;
using System.IO;
using static Annex.Paths;

namespace Game
{
    public static class Program
    {

        private static void Main(string[] args) {
            AnnexGame.Initialize();
            Debug.PackageAssetsToBinaryFrom(AssetType.Texture, Path.Combine(SolutionFolder, "Assets/Textures/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Audio, Path.Combine(SolutionFolder, "Assets/Music/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "Assets/Fonts/"));

            Debug.AddDebugOverlayCommand("demo", debugArgs => {
                switch (debugArgs[0]) {
                    case "convo":
                        ServiceProvider.SceneService.LoadNewScene<ConversationDemoScene>();
                        break;
                    case "quest":
                        ServiceProvider.SceneService.LoadNewScene<QuestlineDemoScene>();
                        break;
                }
            });

            AnnexGame.Start<WorldScene>();
        }
    }
}

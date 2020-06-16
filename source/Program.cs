using Annex;
using Game.Scenes;

namespace Game
{
    public static class Program
    {
        private static void Main(string[] args) {
            AnnexGame.Initialize();
            // Load assets
            Debug.PackageAssetsToBinaryFrom(Annex.Assets.AssetType.Texture, Paths.SolutionFolder + "/Assets/Textures");

            AnnexGame.Start<MainMenu>();
        }
    }
}

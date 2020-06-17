using Annex;
using Annex.Assets;
using Game.Scenes;
using System.IO;
using static Annex.Paths;


namespace Game
{
    public static class Program
    {
        private static void Main(string[] args) {
            AnnexGame.Initialize();

            Debug.PackageAssetsToBinaryFrom(AssetType.Audio, Path.Combine(SolutionFolder, "assets/music/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "assets/font/"));


            AnnexGame.Start<Scenes.MainMenu.MainMenu>();
        }
    }
}

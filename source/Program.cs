﻿using Annex;
using Annex.Assets;
using Game.Scenes.World;
using System.IO;
using static Annex.Paths;

namespace Game
{
    public static class Program
    {
        private static void Main(string[] args) {
            
            AnnexGame.Initialize();

            Debug.PackageAssetsToBinaryFrom(AssetType.Texture, Path.Combine(SolutionFolder, "assets/textures/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "assets/fonts/"));

            AnnexGame.Start<WorldScene>();
        }
    }
}

using Annex;
using Game.Scenes;

namespace Game
{
    public static class Program
    {
        private static void Main(string[] args) {
            AnnexGame.Initialize();
            AnnexGame.Start<MainMenu>();
        }
    }
}

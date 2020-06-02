using Annex;
using Annex.Scenes.Components;

namespace Game.Scenes
{
    public class MainMenu : Scene
    {
        public override void HandleCloseButtonPressed() {
            ServiceProvider.SceneService.LoadGameClosingScene();
        }
    }
}

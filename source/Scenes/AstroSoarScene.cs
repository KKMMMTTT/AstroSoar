using Annex;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;

namespace Game.Scenes
{
    /// <summary>
    /// Default scene for all game scenes.
    /// It will implement behaviour common throughout all scenes of the game.
    /// </summary>
    public class AstroSoarScene : Scene
    {
        public override void HandleCloseButtonPressed()
        {
            ServiceProvider.SceneService.LoadGameClosingScene();
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e)
        {
            if (e.Key == KeyboardKey.Escape)
            {
                e.Handled = true;
                ServiceProvider.SceneService.LoadGameClosingScene();
            }

            base.HandleKeyboardKeyReleased(e);
        }
    }
}

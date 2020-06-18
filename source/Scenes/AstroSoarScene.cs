using System;
using Annex;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;
using Game.Scenes.Components.ExitMenuOverlay;

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
            if (GetElementById(ExitMenuOverlay.ID) == null)
            {
                var child = new ExitMenuOverlay(() => { ServiceProvider.SceneService.LoadGameClosingScene(); },
                    () => { RemoveElementById(ExitMenuOverlay.ID); });
                AddChild(child);
            }
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e)
        {
            base.HandleKeyboardKeyReleased(e);

            if (e.Handled) return;
         
            if (e.Key == KeyboardKey.Escape)
            {
                e.Handled = true;
                Debug.ToggleDebugOverlay();
            }
        }
    }
}

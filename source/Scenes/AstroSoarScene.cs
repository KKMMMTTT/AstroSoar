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
        public override void HandleCloseButtonPressed() {
            if (GetElementById(ExitMenuOverlay.ID) == null) {
                var child = new ExitMenuOverlay(() => { ServiceProvider.SceneService.LoadGameClosingScene(); SavePlayer(); },
                    () => { RemoveElementById(ExitMenuOverlay.ID); });
                AddChild(child);
            }
        }

        private void SavePlayer() {
            if (this is ISceneWithPlayer currentScene) {
                AstroSoarServiceProvider.PlayerDefinitionService.Save(currentScene.Player!, "player");
            }
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);

            if (this is ISceneWithPlayer currentScene) {
                if (e.PreviousScene is ISceneWithPlayer previousScene) {
                    currentScene.Player = previousScene.Player;
                }
            }
        }

        public override void HandleKeyboardKeyReleased(KeyboardKeyReleasedEvent e) {
            base.HandleKeyboardKeyReleased(e);

            if (e.Handled) return;

            if (e.Key == KeyboardKey.Escape) {
                e.Handled = true;
                Debug.ToggleDebugOverlay();
            }
        }
    }
}

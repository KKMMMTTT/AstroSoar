using Annex;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using Game.Scenes.Components.Conversations;

namespace Game.Scenes.Demo
{
    public class ConversationDemoScene : Scene
    {
        static ConversationDemoScene() {
            Debug.AddDebugOverlayCommand("open", args => {
                ActiveConversationDialog.Show(ServiceProvider.SceneService.CurrentScene, args[0]);
            });
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (e.Key == Annex.Scenes.KeyboardKey.Tilde) {
                Debug.ToggleDebugOverlay();
                return;
            }
            base.HandleKeyboardKeyPressed(e);
        }
    }
}

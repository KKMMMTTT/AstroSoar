using Annex.Scenes.Components;
using Game.Conversations;

namespace Game.Scenes.Components.Conversations.Helper
{
    public class DisplayBox : Image
    {
        public const float Padding = 10;

        public DisplayBox(ActiveConversationDialog dialog) : base(string.Empty) {
            dialog.OnActiveConversationDialogChanged += this.Dialog_OnActiveConversationDialogChanged;

            float squareSize = dialog.Size.Y - 2 * Padding;

            this.Size.Set(squareSize, squareSize);
            this.Position.Set(Padding, dialog.Position.Y + Padding);
        }

        private void Dialog_OnActiveConversationDialogChanged(ActiveConversation obj) {
            this.ImageTextureName.Set(obj.CurrentPage?.DisplayBoxTexture);
        }
    }
}

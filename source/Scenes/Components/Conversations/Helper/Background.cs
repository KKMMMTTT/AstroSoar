using Annex.Data;
using Annex.Graphics.Contexts;
using Game.Conversations;

namespace Game.Scenes.Components.Conversations.Helper
{
    public class Background : SolidRectangleContext
    {
        public Background(ActiveConversationDialog dialog) : base(new RGBA(25, 25, 25)) {
            this.RenderSize.Set(dialog.Size);
            this.RenderPosition.Set(dialog.Position);

            dialog.OnActiveConversationDialogChanged += this.Dialog_OnActiveConversationDialogChanged;
        }

        private void Dialog_OnActiveConversationDialogChanged(ActiveConversation obj) {

        }
    }
}

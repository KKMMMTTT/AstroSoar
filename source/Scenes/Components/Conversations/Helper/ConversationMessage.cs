using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;

namespace Game.Scenes.Components.Conversations.Helper
{
    public class ConversationMessage : UIElement
    {
        private readonly SolidRectangleContext _background;
        public readonly TextContext _message;

        public const float Height = 50;

        public ConversationMessage(ActiveConversationDialog dialog) : base(string.Empty) {
            dialog.OnActiveConversationDialogChanged += this.Dialog_OnActiveConversationDialogChanged;

            this._background = new SolidRectangleContext(new RGBA(150, 150, 150));
            this._background.RenderPosition.Set(new OffsetVector(dialog.Position, dialog.Size.Y, 0));
            this._background.RenderSize.Set(dialog.Size.X - dialog.Size.Y, Height);

            this._message = new TextContext(string.Empty, Fonts.Default);
            this._message.RenderPosition.Set(this._background.RenderPosition);
            this._message.FontColor.Set(RGBA.White);
            this._message.FontSize.Set(18);
            this._message.BorderColor.Set(RGBA.Black);
            this._message.BorderThickness = 3f;
            this._message.Alignment = new TextAlignment() {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Size = this._background.RenderSize
            };
        }

        public override void Draw(ICanvas canvas) {
            canvas.Draw(this._background);
            canvas.Draw(this._message);
        }

        private void Dialog_OnActiveConversationDialogChanged(Game.Conversations.ActiveConversation dialog) {
            this._message.RenderText.Set(dialog.CurrentPage!.DisplayText);
        }
    }
}

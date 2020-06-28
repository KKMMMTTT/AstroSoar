using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using Game.Conversations;
using Game.Definitions.Conversations;

namespace Game.Scenes.Components.Conversations.Helper
{
    public class OptionsList : Container
    {
        public OptionsList(ActiveConversationDialog dialog) {
            dialog.OnActiveConversationDialogChanged += this.ActiveConversationDialog_OnActiveConversationDialogChanged;

            this.Size.Set(new OffsetVector(dialog.Size, -dialog.Size.Y, 0));
            this.Position.Set(new OffsetVector(dialog.Position, dialog.Size.Y, 0));
        }

        private void ActiveConversationDialog_OnActiveConversationDialogChanged(ActiveConversation activeConversation) {

            this._children.Clear();

            var options = activeConversation.CurrentPage!.ReplyOptions;
            for (int i = 0; i < options.Count; i++) {
                this.AddChild(new Option(this, activeConversation, options[i], i, options.Count));
            }
        }
    }

    public class Option : Label
    {
        private readonly ActiveConversation _activeConversation;
        private readonly int _optionIndex;

        public Option(OptionsList optionsList, ActiveConversation activeConversation, ReplyOption replyOption, int i, int numOptions) {
            this._activeConversation = activeConversation;
            this._optionIndex = i;

            float height = (optionsList.Size.Y - ConversationMessage.Height) / numOptions;
            float width = optionsList.Size.X;
            this.Size.Set(width, height);

            float top = optionsList.Position.Y + ConversationMessage.Height + height * i;
            float left = optionsList.Position.X;
            this.Position.Set(left, top);

            this.Caption.Set(replyOption.ReplyText);
            this.Font.Set(Fonts.Default);
            this.FontSize.Set(14);

            this.RenderText.FontColor = RGBA.White;
            this.RenderText.BorderColor = RGBA.Black;
            this.RenderText.BorderThickness = 1f;

            this.RenderText.Alignment = new TextAlignment() {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Size = this.Size
            };
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            this._activeConversation.SelectionOption(this._optionIndex);
        }
    }
}

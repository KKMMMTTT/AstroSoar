using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Scenes.Components;
using Game.Conversations;
using Game.Scenes.Components.Conversations.Helper;
using System;

namespace Game.Scenes.Components.Conversations
{
    public class ActiveConversationDialog : Container
    {
        private const string DialogGuid = "{0F1D97A3-5FCC-4294-85AF-EB6136BC18C1}";
        private readonly Background _background;
        public event Action<ActiveConversation>? OnActiveConversationDialogChanged;

        public static void Show(Scene scene, string id) {
            Debug.ErrorIf(scene.GetElementById(DialogGuid) != null, "Conversation dialog already exists in the scene");
            scene.AddChild(new ActiveConversationDialog(id));
        }

        private ActiveConversationDialog(string id) : base(DialogGuid) {
            var camera = ServiceProvider.Canvas.GetCamera();
            this.Size.Set(new ScalingVector(camera.Size, 1, 0.4f));
            this.Position.Set(new ScalingVector(camera.Size, 0, 0.6f));

            var convoService = AstroSoarServiceProvider.ConversationService;
            var activeConvo = new ActiveConversation(convoService.Load(id));
            activeConvo.OnActiveConversationUpdated += this.ActiveConvo_OnActiveConversationUpdated;

            this._background = new Background(this);
            this.AddChild(new ConversationMessage(this));
            this.AddChild(new DisplayBox(this));
            this.AddChild(new OptionsList(this));

            this.OnActiveConversationDialogChanged?.Invoke(activeConvo);
        }

        private void ActiveConvo_OnActiveConversationUpdated(ActiveConversation conversation) {
            if (conversation.IsOver) {
                ServiceProvider.SceneService.CurrentScene.RemoveElementById(DialogGuid);
                return;
            }

            this.OnActiveConversationDialogChanged?.Invoke(conversation);
        }

        public override void Draw(ICanvas canvas) {
            canvas.Draw(this._background);
            base.Draw(canvas);
        }
    }
}

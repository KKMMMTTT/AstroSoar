using Annex;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Scenes;
using Annex.Scenes.Components;

namespace Game.Scenes
{
    public abstract class FirstPersonScene : Scene
    {
        private readonly String _titlebarCaption;

        public FirstPersonScene(String backgroundImage, String titlebarCaption) {
            this._titlebarCaption = titlebarCaption;
            this.AddChild(new FirstPersonSceneBackgroundImage(backgroundImage));
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            var titleBar = new Titlebar(this._titlebarCaption);
            this.AddChild(titleBar);
            this.Events.AddEvent(PriorityType.ANIMATION, new Titlebar.FadeAndRemoveEvent(this, titleBar));
        }
    }

    public class Titlebar : Label
    {
        public const string TitlebarCaptionId = "title";

        public Titlebar(String caption) : base(Titlebar.TitlebarCaptionId) {
            this.Caption.Set(caption.Value);
            this.Font.Set(Fonts.Default);
        }

        public class FadeAndRemoveEvent : CustomEvent
        {
            public const string FadeAndRemoveEventID = "fade-and-remove-event";
            private readonly Scene _scene;
            private readonly Titlebar _target;
            private int _counter;

            public FadeAndRemoveEvent(Scene scene, Titlebar target) : base(FadeAndRemoveEventID, 100, 5000) {
                this._scene = scene;
                this._target = target;
                this._counter = 0;
            }

            protected override void Run(EventArgs args) {
                this._counter++;
                if (this._counter == 100) {
                    args.RemoveFromQueue = true;
                    return;
                }
                this._target.RenderText.BorderColor.A = (byte)(0.95f * this._target.RenderText.BorderColor.A);
            }
        }
    }

    public class FirstPersonSceneBackgroundImage : Image
    {
        public const string BackroundImageId = "background";

        public FirstPersonSceneBackgroundImage(String backgroundImageTextureName) : base(FirstPersonSceneBackgroundImage.BackroundImageId) {
            var canvas = ServiceProvider.Canvas;
            ImageTextureName.Set(backgroundImageTextureName.Value);
            Size.Set(canvas.GetResolution());
        }
    }
}

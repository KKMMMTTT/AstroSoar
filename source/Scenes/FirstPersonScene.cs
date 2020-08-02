using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;
using Annex.Scenes.Components;

namespace Game.Scenes
{
    public abstract class FirstPersonScene : AstroSoarScene
    {
        private readonly String _titlebarCaption;
        private System.Type _previousSceneType;

        public FirstPersonScene(String backgroundImage, String titlebarCaption) {
            this._titlebarCaption = titlebarCaption;
            this.AddChild(new FirstPersonSceneBackgroundImage(backgroundImage));
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            this._previousSceneType = e.PreviousScene.GetType();

            var titleBar = new Titlebar(this._titlebarCaption);
            this.AddChild(titleBar);
            this.AddChild(new FirstPersonSceneBackArrow(GoBack));
            this.Events.AddEvent(PriorityType.ANIMATION, new Titlebar.FadeAndRemoveEvent(this, titleBar));
        }

        public void GoBack() {
            ServiceProvider.SceneService.LoadScene(this._previousSceneType);
        }
    }

    public class Titlebar : Label
    {
        public const string TitlebarCaptionId = "title";

        public Titlebar(String caption) : base(TitlebarCaptionId) {
            var camera = ServiceProvider.Canvas.GetCamera();
            this.Size.Set(camera.Size.X, 50);

            this.Position.Set(0, 0);
            this.RenderText.Alignment = new TextAlignment() {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Size = this.Size
            };

            this.Caption.Set(caption.Value);
            this.Font.Set(Fonts.Default);
            this.FontSize.Set(32);
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.BorderColor = RGBA.Black;
            this.RenderText.BorderThickness = 2;
        }

        public class FadeAndRemoveEvent : CustomEvent
        {
            public const string FadeAndRemoveEventID = "fade-and-remove-event";
            private readonly Scene _scene;
            private readonly Titlebar _target;
            private int _counter;

            public FadeAndRemoveEvent(Scene scene, Titlebar target) : base(FadeAndRemoveEventID, 100, 2500) {
                this._scene = scene;
                this._target = target;
                this._counter = 0;
            }

            protected override void Run(EventArgs args) {
                this._counter++;
                if (this._counter == 100) {
                    this._scene.RemoveElementById(Titlebar.TitlebarCaptionId);
                    args.RemoveFromQueue = true;
                    return;
                }
                this._target.RenderText.BorderColor.A = (byte)(0.9f * this._target.RenderText.BorderColor.A);
                this._target.RenderText.FontColor.A = (byte)(0.9f * this._target.RenderText.FontColor.A);
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

    public class FirstPersonSceneBackArrow : Button
    {
        public const string BackArrowId = "backarrow";
        private readonly System.Action _clickHandler;

        public FirstPersonSceneBackArrow(System.Action clickHandler) : base(BackArrowId) {
            this._clickHandler = clickHandler;
            this.ImageTextureName.Set("pov/backarrow.png");
            this.Position.Set(0, 0);
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            base.HandleMouseButtonPressed(e);
            _clickHandler();
        }
    }

    public static class Extensions
    {
        public static void LoadScene(this SceneService sceneSerivce, System.Type type) {
            var method = typeof(SceneService).GetMethod("LoadScene").MakeGenericMethod(new System.Type[] { type });
            method.Invoke(sceneSerivce, new object[0]);
        }
    }
}

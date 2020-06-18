using Annex;
using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.Components.ExitMenuOverlay
{
    public class ExitMenuOverlay : Container
    {
        public const string ID = "MessageBox-Overlay";

        private readonly SolidRectangleContext _background;

        public ExitMenuOverlay(Action affirmationAction, Action refutationAction) : base(ID)
        {
            _background = new SolidRectangleContext(new RGBA(0, 0, 0, 150))
            {
                RenderSize = ServiceProvider.Canvas.GetResolution(),
                UseUIView = true
            };
            
            var affirmationButton = new AffirmationButton("affirmationButton") {
                Visible = true,
                Font = { Value = "default.ttf" },
                ImageTextureName = { Value = "buttons/yes.png" },
                Position = { X = 40, Y = 100 },
                Size = { X = 40, Y = 40 },
                OnClickHandler = affirmationAction
            };
            
            var refutationButton = new RefutationButton("refutationButton")
            {
                Visible = true,
                Font = { Value = "default.ttf" },
                ImageTextureName = { Value = "buttons/no.png" },
                Position = { X = 100, Y = 100 },
                Size = { X = 40, Y = 40 },
                OnClickHandler = refutationAction
            };

            AddChild(affirmationButton);
            AddChild(refutationButton);
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(_background);
            base.Draw(canvas);
        }
    }
}

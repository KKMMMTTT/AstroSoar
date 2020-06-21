using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Data.Shared;

namespace Game.Scenes.DevMode.Elements
{
    //this is the purpple background for the buttons
    public class ButtonsBackground : IDrawableObject
    {

        private readonly SolidRectangleContext _rectangleContext;

        public ButtonsBackground()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Purple)
            {
                RenderPosition = Vector.Create(1, 0),
                RenderBorderColor = RGBA.White,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(1000, 30)
            };
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }
}

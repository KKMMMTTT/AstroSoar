using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Game.Scenes.DevMode.Elements
{
    //this is the righthand purple bar
    public class Placeholder : IDrawableObject
    {
        private readonly SolidRectangleContext _rectangleContext;

        public Placeholder()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Purple)
            {
                RenderPosition = Vector.Create(900, 0),
                RenderBorderColor = RGBA.White,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(100, 650)
            };
        }

        public void UpdatePosition(float x, float y)
        {
            _rectangleContext.RenderPosition = Vector.Create(x, y);
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }
}

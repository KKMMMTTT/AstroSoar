using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Game.Scenes
{
    public class GreenScreen : AstroSoarScene
    {
        private readonly SolidRectangleContext _rectangleContext = new SolidRectangleContext(new RGBA(0, 255, 0));

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            canvas.Draw(_rectangleContext);
        }
    }
}

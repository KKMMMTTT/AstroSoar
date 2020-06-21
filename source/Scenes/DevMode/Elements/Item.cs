using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Game.Scenes.DevMode.Elements
{
    public class Item : IDrawableObject
    {
        private readonly SolidRectangleContext _rectangleContext;
        private string name = "item";

        public Item()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Red)
            {
                RenderPosition = Vector.Create(150, 540),
                RenderBorderColor = RGBA.White,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(200, 100)
            };
        }

        public void SetName(string s)
        {
            name = s;
        }

        public string GetName()
        {
            return name;
        }

        public void SetPosition(float x, float y)
        {
            _rectangleContext.RenderPosition = Vector.Create(x, y);
        }

        public Vector GetPosition()
        {
            return _rectangleContext.RenderPosition;
        }

        public void SetSize(float x, float y)
        {
            _rectangleContext.RenderSize = Vector.Create(x, y);
        }

        public Vector GetSize()
        {
            return _rectangleContext.RenderSize;
        }

        public void isSelected(bool s)
        {
            if(s == true){
                _rectangleContext.RenderBorderColor = RGBA.Blue;
            }
            if(s == false){
                _rectangleContext.RenderBorderColor = RGBA.White;
            }
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }
}

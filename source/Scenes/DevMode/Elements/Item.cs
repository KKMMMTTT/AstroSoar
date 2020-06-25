using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using System;

namespace Game.Scenes.DevMode.Elements
{
    [Serializable]
    public class Item : IDrawableObject
    {
        private readonly SolidRectangleContext _rectangleContext;
        public string Name = "item";

        public float X {
            get => this._rectangleContext.RenderPosition.X;
            set => this._rectangleContext.RenderPosition.X = value;
        }

        public float Y {
            get => this._rectangleContext.RenderPosition.Y;
            set => this._rectangleContext.RenderPosition.Y = value;
        }

        public float RenderX {
            get => this._rectangleContext.RenderSize.X;
            set => this._rectangleContext.RenderSize.X = value;
        }

        public float RenderY {
            get => this._rectangleContext.RenderSize.Y;
            set => this._rectangleContext.RenderSize.Y = value;
        }

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

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
        private readonly TextureContext _texture;
        public string Name = "item";

        public float X {
            set => this._texture.RenderPosition.X = value;
            get => this._texture.RenderPosition.X;
        }

        public float Y {
            set => this._texture.RenderPosition.Y = value;
            get => this._texture.RenderPosition.Y;
        }

        public float RenderX {
            get => this._texture.RenderSize.X;
            set => this._texture.RenderSize.X = value;
        }

        public float RenderY {
            get => this._texture.RenderSize.Y;
            set => this._texture.RenderSize.Y = value;
        }

        public Item(string texture)
        {
            this._texture = new TextureContext(texture)
            {
                RenderPosition = Vector.Create(150, 150),
                RenderSize = Vector.Create(200, 200),
                RenderColor = RGBA.White
            };
        }

        public void SetPosition(float x, float y)
        {
            _texture.RenderPosition = Vector.Create(x, y);
        }

        public Vector GetPosition()
        {
            return _texture.RenderPosition;
        }

        public void SetSize(float x, float y)
        {
            _texture.RenderSize = Vector.Create(x, y);
        }

        public Vector GetSize()
        {
            return _texture.RenderSize;
        }

        public void isSelected(bool s)
        {
            if (s == true){
                _texture.RenderColor.Set(255,255,255,150);
            }
            if (s == false){
                _texture.RenderColor.Set(255,255,255,255);
            }
        }

        public void Draw(ICanvas canvas){
            canvas.Draw(this._texture);
        }
   
    }
}

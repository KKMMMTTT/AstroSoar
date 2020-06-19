using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;

namespace Game.Entities
{
    public class Planet : BaseEntity, IDrawableObject
    {
        private readonly TextureContext _textureContext;

        public Planet(string filePath)
        {
            _textureContext = new TextureContext(filePath);
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(_textureContext);
        }

        public override void Move(Scene worldScene)
        {
        }
    }

}
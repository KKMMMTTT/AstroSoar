using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Game.Scenes.World;

namespace Game.Entities
{
    public class Planet : BaseEntity
    {
        private readonly TextureContext _textureContext;

        public Planet(string spritePath, Vector position)
        {
            Width = 121;
            Height = 106;
            Position = position;
            
            _textureContext = new TextureContext(spritePath)
            {
                RenderPosition = Position
            };
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(_textureContext);
        }

        public override (bool, Vector) CanMove(WorldScene worldScene)
        {
            return (false, null);
        }

        public override void Move(Vector direction)
        {
            
        }
    }

}
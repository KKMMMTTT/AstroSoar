using Annex.Data.Shared;
using Annex.Graphics;
using Game.Entities.Behaviours;
using Game.Scenes.World;

namespace Game.Entities
{
    public abstract class BaseEntity : IMoveable, IDrawableObject
    {
        public Vector Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        protected BaseEntity()
        {
            Position = Vector.Create();
        }

        public abstract void Draw(ICanvas canvas);

        public abstract (bool, Vector) CanMove(WorldScene worldScene);

        public abstract void Move(Vector direction);
    }
}

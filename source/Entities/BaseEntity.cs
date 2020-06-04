using Annex.Data.Shared;
using Game.Entities.Behaviours;
using Game.Scenes.World;

namespace Game.Entities
{
    public abstract class BaseEntity : IMoveable
    {
        public Vector Position { get; set; }

        protected BaseEntity()
        {
            Position = Vector.Create();
        }
        
        public abstract void Move(WorldScene worldScene);
    }
}

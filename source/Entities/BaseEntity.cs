using Annex.Data.Shared;
using Annex.Scenes.Components;
using Game.Entities.Behaviours;

namespace Game.Entities
{
    public abstract class BaseEntity : IMoveable
    {
        public Vector Position { get; set; }

        protected BaseEntity()
        {
            Position = Vector.Create();
        }
        
        public abstract void Move(Scene worldScene);
    }
}

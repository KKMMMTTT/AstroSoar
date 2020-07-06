using Game.Entities.Behaviours;

namespace Game.Entities
{
    public abstract class CollisionEntity : Entity, ICollidable
    {
        public abstract void OnCollision(CollisionEntity entity);
    }
}

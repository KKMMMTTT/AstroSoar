using Annex.Data.Shared;
using Game.Scenes.World;

namespace Game.Entities
{
    public abstract class BoxCollisionEntity : CollisionEntity
    {
        public override (bool, Vector) CanMove(WorldScene worldScene)
        {
            var direction = GetDirection();

            if (direction == null) return (false, null);

            var nextPosition = new OffsetVector(Position, direction);

            foreach (var entity in worldScene.GetEntities())
            {
                if (entity == this || !(entity is CollisionEntity collisionEntity)) continue;

                if (nextPosition.X < entity.Position.X + entity.Width &&
                    nextPosition.X + this.Width > entity.Position.X &&
                    nextPosition.Y < entity.Position.Y + entity.Height &&
                    nextPosition.Y + this.Height > entity.Position.Y)
                {
                    OnCollision(collisionEntity);
                    collisionEntity.OnCollision(this);
                    return (false, null);
                }
            }

            return (true, direction);
        }

        protected abstract Vector GetDirection();
    }
}

using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics.Contexts;
using Game.Scenes.World;

namespace Game.Entities
{
    public abstract class BoxCollisionEntity : CollisionEntity
    {
        private readonly SolidRectangleContext _box;

        public BoxCollisionEntity(float width, float height) {
            this.Width = width;
            this.Height = height;

            this._box = new SolidRectangleContext(RGBA.Red) {
                RenderSize = Vector.Create(Width, Height),
                RenderPosition = this.Position
            };
        }

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

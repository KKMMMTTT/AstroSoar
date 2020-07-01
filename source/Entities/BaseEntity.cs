using Annex.Data.Shared;
using Annex.Graphics;
using Game.Entities.Behaviours;
using Game.Scenes.World;
using System.Text.Json.Serialization;

namespace Game.Entities
{
    public abstract class BaseEntity : IMoveable, IDrawableObject
    {
        [JsonIgnore]
        public Vector Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public float SERIALIZATION_X { get => this.Position.X; set => this.Position.X = value; }
        public float SERIALIZATION_Y { get => this.Position.Y; set => this.Position.Y = value; }


        protected BaseEntity()
        {
            Position = Vector.Create();
        }

        public abstract void Draw(ICanvas canvas);

        public abstract (bool, Vector) CanMove(WorldScene worldScene);

        public abstract void Move(Vector direction);
    }
}

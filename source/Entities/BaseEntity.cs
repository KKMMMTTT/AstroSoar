using Annex.Data.Shared;
using Annex.Scenes.Components;
using Game.Entities.Behaviours;
using System.Text.Json.Serialization;

namespace Game.Entities
{
    public abstract class BaseEntity : IMoveable
    {
        [JsonIgnore]
        public Vector Position { get; set; }

        public float SERIALIZATION_X { get => this.Position.X; set => this.Position.X = value; }
        public float SERIALIZATION_Y { get => this.Position.Y; set => this.Position.Y = value; }


        protected BaseEntity()
        {
            Position = Vector.Create();
        }

        public abstract void Move(Scene worldScene);
    }
}

using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Game.Entities;
using System.Collections.Generic;

namespace Game.Scenes.World
{
    public class WorldScene : AstroSoarScene
    {
        private readonly Player Player;
        private readonly IList<BaseEntity> _baseEntities;
        private readonly TextureContext _mapTextureContext;

        public WorldScene()
        {
            Player = new Player();
            _baseEntities = new List<BaseEntity>() { Player };
            _mapTextureContext = new TextureContext("world.jpg");

            ServiceProvider.Canvas.GetCamera().Follow(Player.Position);

            Events.AddEvent(PriorityType.LOGIC, UpdateEntityPositions());
        }

        public void AddEntity(BaseEntity entity)
        {
            _baseEntities.Add(entity);
        }

        private GameEvent UpdateEntityPositions()
        {
            return new GameEvent("update entity position in world", e =>
            {
                foreach (var baseEntity in _baseEntities)
                {
                    baseEntity.Move(this);
                }
            }, 5, 0);
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(_mapTextureContext);
        }
    }
}

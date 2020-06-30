using Annex;
using Annex.Events;
using Annex.Graphics;
using Game.Entities;
using Game.Events;
using System.Collections.Generic;
using Annex.Data.Shared;

namespace Game.Scenes.World
{
    public class WorldScene : AstroSoarScene
    {
        private readonly Player _player;

        private readonly IList<BaseEntity> _entities;

        public WorldScene()
        {
            _player = new Player("character.png");
            _entities = new List<BaseEntity>()
            {
                //new Planet("buttons/no.png", Vector.Create(70, 70)),
                new Planet("planets/planet_1.png", Vector.Create(100, 100)),
                _player
            };

            ServiceProvider.Canvas.GetCamera().Follow(_player.Position);
            var entityPositionsEvent = new UpdateWorldEntityPositionsEvent(this, _entities, "update entity positions", 10, 0);
            Events.AddEvent(PriorityType.LOGIC, entityPositionsEvent);
        }

        public void AddEntity(BaseEntity entity)
        {
            _entities.Add(entity);
        }

        public IList<BaseEntity> GetEntities()
        {
            return this._entities;
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            foreach (var entity in _entities)
            {
                entity.Draw(canvas);
            }

        }
    }
}

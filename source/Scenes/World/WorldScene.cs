using Annex;
using Annex.Events;
using Annex.Graphics;
using Game.Entities;
using Game.Entities.Behaviours;
using Game.Events;
using System.Collections.Generic;

namespace Game.Scenes.World
{
    public class WorldScene : AstroSoarScene
    {
        private readonly Player _player;

        // Map ids => objects

        private readonly IList<IMoveable> _moveableEntities;
        private readonly IList<IDrawableObject> _drawableEntities;

        public WorldScene()
        {
            _player = new Player();
            _moveableEntities = new List<IMoveable>() { _player };
            _drawableEntities = new List<IDrawableObject>() { new Planet("planets/planet_1.png") };

            ServiceProvider.Canvas.GetCamera().Follow(_player.Position);
            var entityPositionsEvent = new UpdateWorldEntityPositionsEvent(this, _moveableEntities, "update entity positions", 10, 0);
            Events.AddEvent(PriorityType.LOGIC, entityPositionsEvent);
        }

        // Add to map, => add to proper lists

        // Remove from map, => remove from both lists

        public void AddMoveableEntity(IMoveable entity)
        {
            _moveableEntities.Add(entity);
        }

        public void AddDrawableEntity(IDrawableObject entity)
        {
            _drawableEntities.Add(entity);
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
            foreach (var entity in _drawableEntities)
            {
                entity.Draw(canvas);
            }
        }
    }
}

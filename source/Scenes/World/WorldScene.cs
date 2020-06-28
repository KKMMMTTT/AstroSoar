using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Scenes;
using Game.Entities;
using Game.Entities.Behaviours;
using Game.Events;
using System.Collections.Generic;

namespace Game.Scenes.World
{
    public class WorldScene : AstroSoarScene, ISceneWithPlayer
    {
        private readonly IList<IMoveable> _moveableEntities;
        private readonly IList<IDrawableObject> _drawableEntities;
        public Player? Player { get; set; }

        public WorldScene() {
            _moveableEntities = new List<IMoveable>();
            _drawableEntities = new List<IDrawableObject>() { new Planet("planets/planet_1.png") };

            var entityPositionsEvent = new UpdateWorldEntityPositionsEvent(this, _moveableEntities, "update entity positions", 10, 0);
            Events.AddEvent(PriorityType.LOGIC, entityPositionsEvent);
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);

            this.AddMoveableEntity(this.Player!);
            ServiceProvider.Canvas.GetCamera().Follow(this.Player!.Position);
        }

        // Add to map, => add to proper lists

        // Remove from map, => remove from both lists

        public void AddMoveableEntity(IMoveable entity) {
            _moveableEntities.Add(entity);
        }

        public void AddDrawableEntity(IDrawableObject entity) {
            _drawableEntities.Add(entity);
        }

        public override void Draw(ICanvas canvas) {
            foreach (var entity in _drawableEntities) {
                entity.Draw(canvas);
            }
            base.Draw(canvas);
        }
    }
}

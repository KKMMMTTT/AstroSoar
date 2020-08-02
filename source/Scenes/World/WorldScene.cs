using Annex;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics;
using Annex.Scenes;
using Game.Entities;
using Game.Entities.Planets;
using Game.Events;
using System.Collections.Generic;

namespace Game.Scenes.World
{
    public class WorldScene : AstroSoarScene, ISceneWithPlayer
    {
        private readonly IList<Entity> _entities;
        public Player Player { get; set; }

        public WorldScene()
        {
            Player = new Player();
            _entities = new List<Entity>()
            {
                new NewspaperStand(Vector.Create(200, 200)),
                new GreenPlanet("Planets/planet_1.png", Vector.Create(100, 100))
            };

            var entityPositionsEvent = new UpdateWorldEntityPositionsEvent(this, _entities, "update entity positions", 10, 0);
            Events.AddEvent(PriorityType.LOGIC, entityPositionsEvent);
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);

            this.AddEntity(this.Player!);
            ServiceProvider.Canvas.GetCamera().Follow(this.Player!.Position);
        }

        public override void HandleSceneOnLeave(SceneOnLeaveEvent e) {
            base.HandleSceneOnLeave(e);
            this._entities.Remove(this.Player!);
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public IList<Entity> GetEntities()
        {
            return this._entities;
        }

        public override void Draw(ICanvas canvas) {
            foreach (var entity in _entities) {
                entity.Draw(canvas);
            }
            base.Draw(canvas);
        }
    }
}

using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Scenes.Components;
using Game.Entities;
using Game.Entities.Behaviours;
using Game.Events;
using System.Collections.Generic;

namespace Game.Scenes.World
{
    public class WorldScene : AstroSoarScene
    {
        private readonly Player _player;
        private readonly IList<IMoveable> _baseEntities;

        public WorldScene()
        {
            _player = new Player();
            _baseEntities = new List<IMoveable>() { _player };
            
            var button = new Button()
            {
                Caption = { Value = "Click me"},
                Visible = true,
                Font = { Value = "default.ttf"},
                Position = { X = 10, Y = 10 },
                FontSize = { Value = 30 }
            };
            AddChild(button);

            ServiceProvider.Canvas.GetCamera().Follow(_player.Position);

            var entityPositionsEvent = new UpdateWorldEntityPositionsEvent(this, _baseEntities, "update entity positions", 10, 0);
            Events.AddEvent(PriorityType.LOGIC, entityPositionsEvent);
        }

        public void AddEntity(BaseEntity entity)
        {
            _baseEntities.Add(entity);
        }

        public override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);
        }
    }
}

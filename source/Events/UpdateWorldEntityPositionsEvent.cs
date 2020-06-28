using Annex.Events;
using Annex.Scenes.Components;
using Game.Entities.Behaviours;
using System.Collections.Generic;
using EventArgs = Annex.Events.EventArgs;

namespace Game.Events
{
    public class UpdateWorldEntityPositionsEvent : CustomEvent
    {
        private readonly Scene _scene;
        private readonly IList<IMoveable> _moveables;

        public UpdateWorldEntityPositionsEvent(Scene scene, IList<IMoveable> moveables, string eventID, int interval_ms, int delay_ms) : base(eventID, interval_ms, delay_ms)
        {
            _scene = scene;
            _moveables = moveables;
        }

        protected override void Run(EventArgs args)
        {
            foreach (var moveable in _moveables)
            {
                moveable.Move(_scene);
            }
        }
    }
}

﻿using Annex.Events;
using Game.Entities;
using Game.Scenes.World;
using System.Collections.Generic;
using EventArgs = Annex.Events.EventArgs;

namespace Game.Events
{
    public class UpdateWorldEntityPositionsEvent : CustomEvent
    {
        private readonly WorldScene _scene;
        private readonly IList<BaseEntity> _entities;

        public UpdateWorldEntityPositionsEvent(WorldScene scene, IList<BaseEntity> entities, string eventID, int interval_ms, int delay_ms) : base(eventID, interval_ms, delay_ms)
        {
            _scene = scene;
            _entities = entities;
        }

        protected override void Run(EventArgs args)
        {
            foreach (var moveable in _entities)
            {
                var (canMove, direction) = moveable.CanMove(_scene);

                if (canMove)
                {
                    moveable.Move(direction);
                }
            }
        }
    }
}

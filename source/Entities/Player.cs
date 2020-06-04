﻿using Annex;
using Annex.Scenes;
using Game.Scenes.World;

namespace Game.Entities
{
    public class Player : BaseEntity
    {
        public override void Move(WorldScene worldScene)
        {
            var canvas = ServiceProvider.Canvas;

            if (canvas.IsKeyDown(KeyboardKey.A))
            {
                Position.Add(-1, 0);
            }
            
            if (canvas.IsKeyDown(KeyboardKey.D))
            {
                Position.Add(1, 0);
            }
            
            if (canvas.IsKeyDown(KeyboardKey.W))
            {
                Position.Add(0, -1);
            }
            
            if (canvas.IsKeyDown(KeyboardKey.S))
            {
                Position.Add(0, 1);
            }
        }
    }
}

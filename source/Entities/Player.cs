using Annex;
using Annex.Scenes;
using Annex.Scenes.Components;
using Game.Questlines;
using System;

namespace Game.Entities
{
    [Serializable]
    public class Player : BaseEntity
    {
        public QuestlineJournal QuestlineJournal { get; set; }

        public Player() {
            this.QuestlineJournal = new QuestlineJournal();
        }

        public override void Move(Scene worldScene)
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

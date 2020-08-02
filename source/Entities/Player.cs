using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes;
using Game.Questlines;
using System;
using System.Collections.Generic;

namespace Game.Entities
{
    [Serializable]
    public class Player : BoxCollisionEntity
    {
        public QuestlineJournal QuestlineJournal { get; set; }

        private readonly TextureContext _textureContext;

        public Player() : this("character.png") {
            IList<int> x = new List<int>();
        }

        public Player(string spritePath) : base(50, 59)
        {
            this.QuestlineJournal = new QuestlineJournal();

            var renderSize = Vector.Create(Width, Height);

            this._textureContext = new TextureContext(spritePath)
            {
                RenderSize = renderSize,
            };
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(_textureContext);
        }

        public override void Move(Vector direction)
        {
            this.Position.Add(direction);
            this._textureContext.RenderPosition = Position;
        }

        protected override Vector GetDirection()
        {
            var direction = Vector.Create();

            var canvas = ServiceProvider.Canvas;

            if (canvas.IsKeyDown(KeyboardKey.A))
            {
                direction.Add(-1, 0);
            }

            if (canvas.IsKeyDown(KeyboardKey.D))
            {
                direction.Add(1, 0);
            }

            if (canvas.IsKeyDown(KeyboardKey.W))
            {
                direction.Add(0, -1);
            }

            if (canvas.IsKeyDown(KeyboardKey.S))
            {
                direction.Add(0, 1);
            }

            return direction;
        }

        public override void OnCollision(CollisionEntity entity)
        {
            Console.WriteLine("boop: " + entity.GetType().FullName);
        }
    }
}

using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes;
using Game.Questlines;
using Game.Scenes.World;
using System;

namespace Game.Entities
{
    [Serializable]
    public class Player : BaseEntity
    {
        public QuestlineJournal QuestlineJournal { get; set; }

        private readonly TextureContext _textureContext;

        public Player() : this("character.png") {

        }

        public Player(string spritePath)
        {
            this.QuestlineJournal = new QuestlineJournal();
            Width = 50;
            Height = 59;

            var renderSize = Vector.Create(Width, Height);
            var resolution = ServiceProvider.Canvas.GetResolution();

            this._textureContext = new TextureContext(spritePath)
            {
                RenderSize = renderSize,
            };
        }

        public override void Draw(ICanvas canvas)
        {
            canvas.Draw(_textureContext);
        }

        public override (bool, Vector) CanMove(WorldScene worldScene)
        {
            var direction = GetDirection();

            var nextPosition = new OffsetVector(Position, direction);

            foreach (var entity in worldScene.GetEntities())
            {
                if (entity == this) continue;

                if (nextPosition.X < entity.Position.X + entity.Width &&
                    nextPosition.X + this.Width > entity.Position.X &&
                    nextPosition.Y < entity.Position.Y + entity.Height &&
                    nextPosition.Y + this.Height > entity.Position.Y)
                {
                    return (false, null);
                }
            }

            return (true, direction);
        }

        public override void Move(Vector direction)
        {
            this.Position.Add(direction);
            this._textureContext.RenderPosition = Position;
        }

        private Vector GetDirection()
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
    }
}

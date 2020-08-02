using Annex;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Game.Scenes.NewsStand;

namespace Game.Entities
{
    public class NewspaperStand : BoxCollisionEntity
    {
        private readonly TextureContext _sprite;

        public NewspaperStand(Vector position)  : base(100, 100) {
            this.Position = position;
            this._sprite = new TextureContext("newspaperstand.png") {
                RenderSize = Vector.Create(100, 100),
                RenderPosition = position
            };
        }

        public override void Draw(ICanvas canvas) {
            canvas.Draw(this._sprite);
        }

        public override void Move(Vector direction) {
        }

        public override void OnCollision(CollisionEntity entity) {
            if (entity is Player) {
                ServiceProvider.SceneService.LoadNewScene<NewspaperStandScene>();
            }
        }

        protected override Vector GetDirection() {
            return null;
        }
    }
}

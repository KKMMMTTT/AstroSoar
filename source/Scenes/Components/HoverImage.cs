using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;

namespace Game.Scenes.Components
{
    public class HoverImage : Image
    {
        protected readonly String HoverTextureName;

        public HoverImage(String hoverTextureName, string elementID = "") : base(elementID) {
            this.HoverTextureName = hoverTextureName;
        }

        public override void Draw(ICanvas canvas) {
            this.ImageContext.SourceTextureName = this.IsMouseWithinElementBounds() ? this.HoverTextureName : this.ImageTextureName;
            base.Draw(canvas);
        }
    }
}

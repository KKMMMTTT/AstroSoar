using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;


namespace Game.Scenes.DevMode.Elements
{

    //object is a representation of an item
    public class ItemBox : IDrawableObject
    {
        TextContext tctx;
        SolidRectangleContext srctx;
        bool isFocus = false;
        Vector vector;

        public ItemBox(string s, Vector v)
        {
            this.tctx = new TextContext(s, "uni0553.ttf")
            {
                RenderPosition = v,
                FontColor = RGBA.White,
                FontSize = 16
            };
            this.srctx = new SolidRectangleContext(RGBA.Blue)
            {
                RenderPosition = v,
                RenderBorderColor = RGBA.Black,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(200, 75)
            };
            this.vector = v;
        }

        public void ChangePosition(Vector v)
        {
            tctx.RenderPosition = v;
            srctx.RenderPosition = v;
            vector = v;
        }

        public void ChangeColour()
        {
            if (isFocus)
            {
                srctx = new SolidRectangleContext(RGBA.Blue)
                {
                    RenderPosition = vector,
                    RenderBorderColor = RGBA.Black,
                    RenderBorderSize = 1.5f,
                    RenderSize = Vector.Create(200, 75)
                };
                isFocus = false;
            }
            else
            {
                srctx = new SolidRectangleContext(RGBA.Red)
                {
                    RenderPosition = vector,
                    RenderBorderColor = RGBA.Black,
                    RenderBorderSize = 1.5f,
                    RenderSize = Vector.Create(200, 75)
                };
                isFocus = true;
            }
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(srctx);
            canvas.Draw(tctx);
        }
    }
}

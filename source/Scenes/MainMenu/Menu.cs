using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Scenes.MainMenu
{
    class Menu : IDrawableObject
    {

        //this will be changed to a texture context eventually
        private readonly SolidRectangleContext _rectangleContext;


        public Menu()
        {
            this._rectangleContext = new SolidRectangleContext(RGBA.Purple)
            {
                RenderPosition = Vector.Create(350, 50),
                RenderBorderColor = RGBA.Black,
                RenderBorderSize = 1.5f,
                RenderSize = Vector.Create(300, 500)
            };


            

        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(this._rectangleContext);
        }
    }
}

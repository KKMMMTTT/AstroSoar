using Annex.Data;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.MainMenu
{

    //
    class MenuButton : Annex.Scenes.Components.Button
    {


        public MenuButton(String name)
        {
            this.Size.Set(100, 25);
            this.Caption.Set(name);
            // this.ImageTextureName.Set("gui/buttons/simplebutton.png");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            
            OnClickHandler(e);
        }


        //Override this function in MainMenu.cs to link the button up to a function
        public Action< MouseButtonPressedEvent> OnClickHandler { get; set; }
       
    }
}

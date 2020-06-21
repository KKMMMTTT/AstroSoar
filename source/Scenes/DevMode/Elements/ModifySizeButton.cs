using Annex.Data;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.DevMode.Elements
{
    public class ModifySizeButton : Button
    {
        public ModifySizeButton()
        {
            this.Size.Set(100, 25);
            this.Caption.Set("modify");
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 16;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e)
        {
            this.OnClickHandler(e);
        }

        public Action<MouseButtonPressedEvent> OnClickHandler { get; set; }
    }
}

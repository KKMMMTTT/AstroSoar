using Annex.Data;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.DevMode.Elemenets
{
    public class ActionButton : Button
    {
        protected readonly Action<MouseButtonPressedEvent> Action;

        public ActionButton(string id, string caption, Action<MouseButtonPressedEvent> action) : base(id) {
            this.Action = action;

            this.Size.Set(100, 25);
            this.Caption.Set(caption);
            this.Font.Set("Default.ttf");
            this.RenderText.FontSize = 16;
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.Alignment = new TextAlignment()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Size = this.Size
            };
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            this.Action(e);
        }
    }
}
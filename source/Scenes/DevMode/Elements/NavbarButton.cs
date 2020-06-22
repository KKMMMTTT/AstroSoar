using Annex.Data;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.DevMode.Elements
{
    public class NavbarButton : Button
    {
        private static float _leftOffset = 0;
        protected readonly Action<MouseButtonPressedEvent> Action;

        public NavbarButton(string id, string caption, Action<MouseButtonPressedEvent> action) : base(id) {
            this.Action = action;

            this.Size.Set(100, 25);
            this.Caption.Set(caption);
            this.Font.Set("uni0553.ttf");
            this.RenderText.FontSize = 16;
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.Alignment = new TextAlignment()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle,
                Size = this.Size
            };
            this.Position.Set(_leftOffset, 0);
            _leftOffset += this.Size.X;
        }

        public override void HandleMouseButtonPressed(MouseButtonPressedEvent e) {
            this.Action(e);
            e.Handled = true;
        }
    }
}
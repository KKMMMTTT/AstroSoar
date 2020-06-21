using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.DevMode.Elemenets
{
    public class ExportAllButton : ActionButton
    {
        public const string ID = "ExportAllButton";
        public const string Caption = "Export";

        public ExportAllButton(Action<MouseButtonPressedEvent> action) : base(ExportAllButton.ID, ExportAllButton.Caption, action) {
            this.Position.Set(600, 0);
        }
    }
}
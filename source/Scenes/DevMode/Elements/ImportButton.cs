using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.DevMode.Elemenets
{
    public class ImportButton : ActionButton
    {
        public const string ID = "ImportButton";
        public const string Caption = "Import";

        public ImportButton(Action<MouseButtonPressedEvent> action) : base(ImportButton.ID, ImportButton.Caption, action) {
            this.Position.Set(700, 0);
        }
    }
}
using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.DevMode.Elemenets
{
    public class SetTextureButton : ActionButton
    {
        public const string ID = "SetTextureButton";
        public const string Caption = "Set Texture";

        public SetTextureButton(Action<MouseButtonPressedEvent> action) : base(SetTextureButton.ID, SetTextureButton.Caption,  action) {
            this.Position.Set(500, 0);
        }
    }
}
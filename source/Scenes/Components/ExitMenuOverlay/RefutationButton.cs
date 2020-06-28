using Annex.Graphics.Events;
using Annex.Scenes.Components;
using System;

namespace Game.Scenes.Components.ExitMenuOverlay
{
    public class RefutationButton : Button
    {
        public Action OnClickHandler { get; set; }

        public RefutationButton(string Id) : base(Id)
        {
        }

        public override void HandleMouseButtonReleased(MouseButtonReleasedEvent e)
        {
            base.HandleMouseButtonReleased(e);
            
            if (e.Handled) return;

            e.Handled = true;
            OnClickHandler?.Invoke();
        }
    }
}

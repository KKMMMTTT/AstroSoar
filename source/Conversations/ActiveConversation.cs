using Annex;
using Game.Definitions.Conversations;
using System;

namespace Game.Conversations
{
    public class ActiveConversation
    {
        private readonly ConversationDefinition _definition;
        private int _currentPageInex = 0;
        public event Action<ActiveConversation>? OnActiveConversationUpdated;

        public ConversationPage? CurrentPage => this._currentPageInex != -1 ? this._definition.ConversationPages[this._currentPageInex] : null;
        public bool IsOver => this.CurrentPage == null;

        public ActiveConversation(ConversationDefinition definition) {
            this._definition = definition;
        }

        public void SelectionOption(int option) {
            Debug.ErrorIf(option < -1 || option >= this.CurrentPage!.ReplyOptions.Count, $"Option index {option} on page {this._currentPageInex} is not within the bounds [0,{this.CurrentPage!.ReplyOptions.Count}]");

            var selectedOption = this.CurrentPage!.ReplyOptions[option];

            string? flag = selectedOption.Flag;
            long? increment = selectedOption.Increment;
            if (flag != null && increment != null) {
                AstroSoarServiceProvider.FlagHandlerService.Signal(flag, (long)increment);
            }

            Debug.ErrorIf(selectedOption.GoTo < -1 || selectedOption.GoTo >= this._definition.ConversationPages.Count, $"GoTo {selectedOption.GoTo} is not within the bounds [0,{this._definition.ConversationPages.Count}]");
            this._currentPageInex = selectedOption.GoTo;

            this.OnActiveConversationUpdated?.Invoke(this);
        }
    }
}

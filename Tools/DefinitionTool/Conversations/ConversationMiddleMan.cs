using Game;
using Game.Definitions;
using Game.Definitions.Conversations;
using System.Collections.Generic;
using System.IO;

namespace DefinitionTool.Conversations
{
    public class ConversationMiddleMan : IDefinitionEditorMiddleMan
    {
        private List<ConversationDefinition> _definitions = new List<ConversationDefinition>();
        public IEnumerable<object> Definitions => this._definitions;

        public void AddNew() {
            this._definitions.Add(new ConversationDefinition());
        }

        public object GetLabelFor(object definition) {
            return (definition as ConversationDefinition)?.Name;
        }

        public void LoadAll() {
            foreach (var definition in AstroSoarServiceProvider.ConversationService.LoadAll()) {
                _definitions.Add(definition);
            }
        }

        public void BeginEdit(object definition) {
            new EditorPage(definition as ConversationDefinition).ShowDialog();
        }

        public void SaveAll() {
            var definitionService = AstroSoarServiceProvider.DefinitionService;

            Directory.Delete(Globals.DefinitionPath + "conversation/", true);

            foreach (var definition in _definitions) {
                definitionService.Save(definition.Name, definition, DefinitionType.Conversation);
            }
        }
    }
}

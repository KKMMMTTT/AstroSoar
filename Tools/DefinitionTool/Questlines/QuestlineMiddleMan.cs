using Game;
using Game.Definitions;
using Game.Definitions.Questlines;
using System.Collections.Generic;
using System.IO;

namespace DefinitionTool.Questlines
{
    public class QuestlineMiddleMan : IDefinitionEditorMiddleMan
    {
        private List<QuestlineDefinition> _definitions = new List<QuestlineDefinition>();
        public IEnumerable<object> Definitions => this._definitions;

        public void AddNew() {
            this._definitions.Add(new QuestlineDefinition());
        }

        public void BeginEdit(object definition) {
            new EditorPage(definition as QuestlineDefinition).ShowDialog();
        }

        public object GetLabelFor(object definition) {
            return (definition as QuestlineDefinition)!.Name;
        }

        public void LoadAll() {
            foreach (var definition in AstroSoarServiceProvider.QuestlineService.LoadAll()) {
                this._definitions.Add(definition);
            }
        }

        public void SaveAll() {
            var definitionService = AstroSoarServiceProvider.DefinitionService;

            Directory.Delete(Globals.DefinitionPath + "questline/", true);

            foreach (var definition in this._definitions) {
                definitionService.Save(definition.Name, definition, DefinitionType.Questline);
            }
        }
    }
}

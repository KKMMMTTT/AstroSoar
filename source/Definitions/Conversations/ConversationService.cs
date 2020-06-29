using Annex;
using Annex.Assets;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Definitions.Conversations
{
    public class ConversationService : IService
    {
        public void Destroy() {
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }

        public IEnumerable<ConversationDefinition> LoadAll() {
            var definitionService = AstroSoarServiceProvider.DefinitionService;
            string path = Path.Combine(Paths.SolutionFolder, "assets/definitions/conversation/");
            Directory.CreateDirectory(path);
            foreach (var file in Directory.GetFiles(path, "*.json")) {
                yield return definitionService.LoadFromBin<ConversationDefinition>(DefinitionType.Conversation, new FileInfo(file).Name[0..^5]);
            }
        }

        public ConversationDefinition Load(string id) {
            var definitionService = AstroSoarServiceProvider.DefinitionService;
            return definitionService.LoadFromBin<ConversationDefinition>(DefinitionType.Conversation, id);
        }
    }
}

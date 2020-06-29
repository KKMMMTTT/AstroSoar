using Annex;
using Annex.Assets;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Definitions.Questlines
{
    public class QuestlineService : IService
    {
        public void Destroy() {
        }

        public IEnumerable<QuestlineDefinition> LoadAll() {
            var definitionService = AstroSoarServiceProvider.DefinitionService;
            string path = Path.Combine(Paths.SolutionFolder, "assets/definitions/questline/");
            Directory.CreateDirectory(path);
            foreach (var file in Directory.GetFiles(path, "*.json")) {
                yield return definitionService.LoadFromBin<QuestlineDefinition>(DefinitionType.Questline, new FileInfo(file).Name[0..^5]);
            }
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}

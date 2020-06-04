using Annex;
using Annex.Assets;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Definitions.Player
{
    public class PlayerDefinitionService : IService
    {
        public void Destroy() {
        }

        public IEnumerable<string> GetSaveFiles() {
            var path = Path.Combine(Paths.ApplicationPath, "Definitions/Saves/");
            Directory.CreateDirectory(path);
            foreach (var file in Directory.GetFiles(path, "*.json")) {
                yield return new FileInfo(file).Name[0..^5];
            }
        }

        public Entities.Player LoadSave(string saveName) {
            Debug.Assert(GetSaveFiles().Contains(saveName), $"Save {saveName} not found");
            var definitionService = AstroSoarServiceProvider.DefinitionService;
            return definitionService.LoadFromBin<Entities.Player>(DefinitionType.PlayerSave, saveName);
        }

        public void Save(Entities.Player instance, string name) {
            var definitionService = AstroSoarServiceProvider.DefinitionService;
            definitionService.SaveToBin(name, instance, DefinitionType.PlayerSave);

#if DEBUG
            definitionService.SaveToAssets(name, instance, DefinitionType.PlayerSave);
#endif
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}

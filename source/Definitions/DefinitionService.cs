using Annex;
using Annex.Assets;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Game.Definitions
{
    public class DefinitionService : IService
    {
        private string _sourcePath = Path.Combine(Paths.SolutionFolder, "assets/definitions/");
        private string _destPath = Path.Combine(Paths.ApplicationPath, "definitions/");

        public DefinitionService() {
#if DEBUG
            Directory.CreateDirectory(_sourcePath);
            if (Directory.Exists(_destPath)) {
                Directory.Delete(_destPath, true);
            }
            Directory.CreateDirectory(_destPath);

            foreach (var file in Directory.GetFiles(_sourcePath, "*.json", SearchOption.AllDirectories)) {
                var fi = new FileInfo(file);
                string folder = Path.Combine(_destPath, fi.Directory.Name);
                Directory.CreateDirectory(folder);
                File.Copy(file, Path.Combine(_destPath, fi.Directory.Name, fi.Name));
            }
#endif
        }

        public T Load<T>(DefinitionType type, string id) {
            string path = GetAssetPath(_destPath, type, id);
            Debug.Assert(File.Exists(path), $"{type} definition {path} does not exist");
            return JsonSerializer.Deserialize<T>(File.ReadAllText(path));
        }

        public void Save<T>(string id, T obj, DefinitionType type) {
            string path = GetAssetPath(_sourcePath, type, id);
            string json = JsonSerializer.Serialize<T>(obj);
            var fi = new FileInfo(path);
            Directory.CreateDirectory(fi.Directory.FullName);
            File.WriteAllText(path, json);
        }

        public void Destroy() {

        }

        private string GetAssetPath(string source, DefinitionType type, string id) {
            return Path.Combine(source, GetPathForType(type), $"{id}.json");
        }

        private string GetPathForType(DefinitionType type) {
            switch (type) {
                case DefinitionType.Questline:
                    return "questline";
                case DefinitionType.UI:
                    return "ui";
                case DefinitionType.Conversation:
                    return "conversation";
                default:
                    Debug.Error($"Unhandled definition type {type}");
                    return string.Empty;
            }
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}

﻿using Annex;
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

                string relativeFilepath = file.Substring(this._sourcePath.Length);
                string fullFilepath = Path.Combine(this._destPath, relativeFilepath); 
                var fi = new FileInfo(fullFilepath);

                Directory.CreateDirectory(fi.Directory.FullName);
                File.Copy(file, fullFilepath);
            }
#endif
        }

        public T LoadFromBin<T>(DefinitionType type, string id) {
            string path = GetAssetPath(_destPath, type, id);
            Debug.Assert(File.Exists(path), $"{type} definition {path} does not exist");
            return JsonSerializer.Deserialize<T>(File.ReadAllText(path));
        }

        public void SaveToAssets<T>(string id, T obj, DefinitionType type) {
            string path = GetAssetPath(_sourcePath, type, id);
            string json = JsonSerializer.Serialize(obj);
            var fi = new FileInfo(path);
            Directory.CreateDirectory(fi.Directory.FullName);
            File.WriteAllText(path, json);
        }

        public void SaveToBin<T>(string id, T obj, DefinitionType type) {
            string path = GetAssetPath(_destPath, type, id);
            string json = JsonSerializer.Serialize(obj);
            var fi = new FileInfo(path);
            Directory.CreateDirectory(fi.Directory.FullName);
            File.WriteAllText(Path.Combine(fi.Directory.FullName, fi.Name), json);
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
                case DefinitionType.PlayerSave:
                    return "saves";
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

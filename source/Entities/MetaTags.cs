using Annex;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Entities
{
    [Serializable]
    public class MetaTags
    {
        private Dictionary<string, dynamic> _metaData = new Dictionary<string, dynamic>();

        public (string key, dynamic value)[] Entries {
            get {
                return this._metaData.Select(entry => (entry.Key, entry.Value)).ToArray();
            }
            set {
                foreach (var entry in value) {
                    this._metaData[entry.key] = entry.value;
                }
            }
        }

        public dynamic Get(string key) {
            Debug.ErrorIf(!this._metaData.ContainsKey(key), $"Meta key {key} does not exist");
            return this._metaData[key];
        }

        public dynamic GetOrCreate(string key, dynamic value) {
            if (!this._metaData.ContainsKey(key)) {
                this._metaData[key] = value;
            }
            return this._metaData[key];
        }
    }
}

using Annex;
using Annex.Assets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Services
{
    public class GuidValidatorService : IService
    {
        private readonly Dictionary<string, Func<bool>> _registeredGuids;

        public GuidValidatorService() {
            this._registeredGuids = new Dictionary<string, Func<bool>>();
        }

        public void Register(string guid, Func<bool> validator) {
            Debug.ErrorIf(this._registeredGuids.ContainsKey(guid), $"The guid validator for {guid} already exists!");
            this._registeredGuids[guid] = validator;
        }

        public bool IsValid(Guid guid) {
            return IsValid(guid.ToString());
        }

        public bool IsValid(string guid) {
            if (!this._registeredGuids.ContainsKey(guid)) {
                return true;
            }
            return this._registeredGuids[guid]();
        }

        public void Destroy() {
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}

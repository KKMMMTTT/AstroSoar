using Annex;
using Annex.Assets;
using Game.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Services
{
    public class FlagHandlerService : IService
    {
        private readonly Dictionary<string, List<Action<long>>> _flagHandlers;

        public FlagHandlerService() {
            this._flagHandlers = new Dictionary<string, List<Action<long>>>();
        }

        public void Register(string flag, Action<long> handler) {
            if (this._flagHandlers.ContainsKey(flag)) {
                this._flagHandlers[flag] = new List<Action<long>>();
            }

            Debug.ErrorIf(this._flagHandlers[flag].Contains(handler), $"Attempt to register handler for {flag} that already is registered");
            this._flagHandlers[flag].Add(handler);
        }

        public void Remove(string flag, Action<long> handler) {
            Debug.Assert(this._flagHandlers[flag].Contains(handler), $"Attempt to remove handler for {flag} that is not registered");
            this._flagHandlers[flag].Remove(handler);
        }

        public void SignalProgress(string flag, long increment) {
            if (this._flagHandlers.ContainsKey(flag)) {
                foreach (var entry in this._flagHandlers[flag]) {
                    entry.Invoke(increment);
                }
            }

            // TODO: Hook this up to the player's questline journal SIGNAL
            if (ServiceProvider.SceneService.CurrentScene is ISceneWithPlayer scene) {
                scene.Player!.QuestlineJournal.SignalProgress(flag, increment);
            }
        }

        public void Destroy() {
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}

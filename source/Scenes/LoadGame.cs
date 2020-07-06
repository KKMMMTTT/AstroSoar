using Annex;
using Annex.Scenes;
using Annex.Scenes.Components;
using Game.Entities;
using Game.Scenes.World;

namespace Game.Scenes
{
    public class LoadGame : Scene, ISceneWithPlayer
    {
        public Player Player { get; set; }

        public LoadGame() {
            this.Player = AstroSoarServiceProvider.PlayerDefinitionService.LoadSave("player");
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);
            ServiceProvider.SceneService.LoadNewScene<WorldScene>();
        }
    }
}

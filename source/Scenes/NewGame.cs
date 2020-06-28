using Annex;
using Annex.Scenes;
using Annex.Scenes.Components;
using Game.Entities;
using Game.Scenes.World;

namespace Game.Scenes
{
    public class NewGame : Scene, ISceneWithPlayer
    {
        public Player? Player { get; set; }

        public NewGame() {
            this.Player = new Player();
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);
            ServiceProvider.SceneService.LoadNewScene<WorldScene>();
        }
    }
}

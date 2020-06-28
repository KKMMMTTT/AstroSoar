using Annex;
using Annex.Scenes;
using Game.Entities;
using Game.Scenes.World;

namespace Game.Scenes
{
    public class NewGame : SceneWithPlayer
    {
        public NewGame() : base(new Player()) {

        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);
            ServiceProvider.SceneService.LoadNewScene<WorldScene>();
        }
    }
}

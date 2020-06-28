using Annex.Scenes;
using Game.Entities;

namespace Game.Scenes
{
    public class SceneWithPlayer : AstroSoarScene
    {
        public Player? Player { get; private set; }

        public SceneWithPlayer() {

        }

        public SceneWithPlayer(Player instance) {
            this.Player = instance;
        }

        public override void HandleSceneOnEnter(SceneOnEnterEvent e) {
            base.HandleSceneOnEnter(e);

            if (e.PreviousScene is SceneWithPlayer previousScene) {
                this.Player = previousScene.Player;
            }
        }
    }
}

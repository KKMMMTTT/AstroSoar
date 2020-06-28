using Game.Entities;

namespace Game.Scenes
{
    public interface ISceneWithPlayer
    {
        public Player? Player { get; set; }
    }
}

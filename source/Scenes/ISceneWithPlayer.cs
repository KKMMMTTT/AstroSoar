using Game.Entities;

namespace Game.Scenes
{
    public interface ISceneWithPlayer
    {
        Player Player { get; set; }
    }
}

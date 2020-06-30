using Annex.Data.Shared;
using Game.Scenes.World;

namespace Game.Entities.Behaviours
{
    public interface IMoveable
    {
        (bool, Vector) CanMove(WorldScene worldScene);
        
        void Move(Vector direction);
    }
}

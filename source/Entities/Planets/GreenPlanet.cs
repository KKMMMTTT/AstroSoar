using Annex;
using Annex.Data.Shared;
using Game.Scenes;

namespace Game.Entities.Planets
{
    public class GreenPlanet : Planet
    {
        public GreenPlanet(string spritePath, Vector position) : base(spritePath, position)
        {
        }

        public override void OnCollision(CollisionEntity entity)
        {
            ServiceProvider.SceneService.LoadScene<GreenScreen>();
        }
    }
}

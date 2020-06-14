using Annex;
using Annex.Assets;
using System.Collections.Generic;
using System.Linq;

namespace Game.Definitions.Questlines
{
    public class QuestlineService : IService
    {
        public QuestlineService(){
        }

        public void Destroy(){
        }

        public IEnumerable<IAssetManager> GetAssetManagers(){
            return Enumerable.Empty<IAssetManager>();
        }
    }
}

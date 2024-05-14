using Sourse.Background;
using UnityEngine;

namespace Sourse.Spawners
{
    public class BackgroundViewSpawners
    {
        public BackgroundView Get(BackgroundView backgroundViewTemplate)
        {
            return Object.Instantiate(backgroundViewTemplate);
        }
    }
}

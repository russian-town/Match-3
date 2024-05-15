using Sourse.Background;
using UnityEngine;

namespace Sourse.Factories
{
    public class BackgroundViewFactory
    {
        public BackgroundView Get(BackgroundView backgroundViewTemplate)
        {
            return Object.Instantiate(backgroundViewTemplate);
        }
    }
}

using Source.Background;
using UnityEngine;

namespace Source.Factories
{
    public class BackgroundViewFactory
    {
        public BackgroundView Get(BackgroundView backgroundViewTemplate)
        {
            return Object.Instantiate(backgroundViewTemplate);
        }
    }
}

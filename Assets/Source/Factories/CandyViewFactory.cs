using Source.Candies;
using UnityEngine;

namespace Source.Factories
{
    public class CandyViewFactory
    {
        public CandyView Get(CandyView candyViewTemplate)
        {
            return Object.Instantiate(candyViewTemplate);
        }

        public void Destroy(CandyView candyView)
        {
            Object.Destroy(candyView);
        }
    }
}

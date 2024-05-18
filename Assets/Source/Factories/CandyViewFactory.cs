using Sourse.Candies;
using UnityEngine;

namespace Sourse.Factories
{
    public class CandyViewFactory
    {
        public CandyView Get(CandyView candyViewTemplate)
        {
            return Object.Instantiate(candyViewTemplate);
        }
    }
}
using Sourse.Candies;
using UnityEngine;

namespace Sourse.Spawners
{
    public class CandyViewSpawner
    {
        public CandyView Get(CandyView candyViewTemplate)
        {
            return Object.Instantiate(candyViewTemplate);
        }
    }
}

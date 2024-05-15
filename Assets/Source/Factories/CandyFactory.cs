using Sourse.Candies;
using UnityEngine;

namespace Sourse.Factories
{
    public class CandyFactory
    {
        public Candy Get(Vector2 position)
        {
            return new Candy(position);
        }
    }
}

using Sourse.Candies;
using UnityEngine;

namespace Sourse.Factories
{
    public class CandyFactory
    {
        public Candy Get(Vector2 position, int index, CandyType candyType)
        {
            return new Candy(position, index, candyType);
        }
    }
}

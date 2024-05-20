using Source.Candies;
using UnityEngine;

namespace Source.Factories
{
    public class CandyFactory
    {
        public Candy Get(Vector2 position, int index, CandyType candyType)
        {
            return new Candy(position, index, candyType);
        }
    }
}

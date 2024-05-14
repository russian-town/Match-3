using Sourse.Candies;
using UnityEngine;

namespace Sourse.Spawners
{
    public class CandySpawner
    {
        public Candy Get(Vector2 position)
        {
            return new Candy(position);
        }
    }
}

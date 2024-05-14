using UnityEngine;

namespace Sourse.Candies
{
    public class Candy
    {
        public Candy(Vector2 position)
            => Position = position;

        public Vector2 Position;

        public void ChangePosition(Vector2 position)
            => Position = position;
    }
}

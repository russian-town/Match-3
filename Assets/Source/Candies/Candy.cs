using UnityEngine;

namespace Sourse.Candies
{
    public class Candy
    {
        public Candy(Vector2 position, int index)
        {
            Position = position;
            Index = index;
        }

        public Vector2 Position { get; private set; }

        public int Index { get; private set; }

        public void ChangePosition(Vector2 position)
        {
            Position = position;
        }
    }
}

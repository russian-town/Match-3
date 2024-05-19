using System;
using UnityEngine;

namespace Sourse.Candies
{
    public class Candy
    {
        public Candy(Vector2 position, int index, CandyType type)
        {
            Position = position;
            Index = index;
            Type = type;
        }

        public event Action Destroyed;

        public Vector2 Position { get; private set; }

        public int Index { get; private set; }

        public CandyType Type { get; private set; }

        public bool IsRemove { get; private set; }

        public void ChangePosition(Vector2 position)
        {
            Position = position;
        }

        public void Remove()
        {
            IsRemove = true;
            Destroyed?.Invoke();
        }
    }
}

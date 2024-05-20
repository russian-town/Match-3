using System;
using UnityEngine;

namespace Source.Candies
{
    public class Candy
    {
        public Candy(Vector2Int position, int index, CandyType type)
        {
            Position = position;
            Index = index;
            Type = type;
        }

        public event Action<Candy> Destroyed;

        public Vector2Int Position { get; private set; }

        public int Index { get; private set; }

        public CandyType Type { get; private set; }

        public void ChangePosition(Vector2Int position)
        {
            Position = position;
        }

        public void Remove()
        {
            Destroyed?.Invoke(this);
        }
    }
}

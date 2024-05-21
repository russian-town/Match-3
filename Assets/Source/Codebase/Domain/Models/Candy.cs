using System;
using UnityEngine;

namespace Source.Codebase.Domain.Models
{
    public class Candy
    {
        public Candy(CandyType candyType, Vector2Int boardPosition)
        {
            Type = candyType;
            BoardPosition = boardPosition;
        }

        public event Action PositionChanged;
        public event Action<Candy> Destroyed;

        public CandyType Type { get; }

        public Vector2Int BoardPosition { get; private set; }

        public void SetBoardPosition(Vector2Int boardPosition)
        {
            BoardPosition = boardPosition;
            PositionChanged?.Invoke();
        }

        public void Destroy() =>
            Destroyed?.Invoke(this);
    }
}
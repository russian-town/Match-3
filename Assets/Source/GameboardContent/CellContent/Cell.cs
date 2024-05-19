using Sourse.Candies;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class Cell
    {
        private Candy _candy;

        public Cell(Vector2 worldPosition, int index)
        {
            WorldPosition = worldPosition;
            Index = index;
        }

        public Vector2 WorldPosition { get; private set; }

        public int Index { get; private set; }

        public Candy Candy => _candy;

        public CandyType CandyType => Candy.Type;

        public bool IsEmpty => _candy == null;

        public void SetCandy(Candy candy)
        {
            _candy = candy;
        }
    }
}

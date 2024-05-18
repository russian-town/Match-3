using Sourse.Candies;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class Cell
    {
        public Cell(Vector2 worldPosition, int index)
        {
            WorldPosition = worldPosition;
            Index = index;
        }

        public Candy Candy { get; private set; }

        public Vector2 WorldPosition { get; private set; }

        public int Index { get; private set; }

        public CandyType CandyType => Candy.Type;

        public void SetCandy(Candy candy)
        {
            Candy = candy;
        }
    }
}

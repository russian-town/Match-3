using Sourse.Candies;
using System;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class Cell
    {
        public Cell(Vector2 worldPosition)
        {
            WorldPosition = worldPosition;
        }

        public Candy Candy { get; private set; }

        public Vector2 WorldPosition {  get; private set; }

        public void SetCandy(Candy candy)
            => Candy = candy;
    }
}

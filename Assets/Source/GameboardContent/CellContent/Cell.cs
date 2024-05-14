using Sourse.Candies;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class Cell
    {
        public Cell(Vector2 postion)
        {
            Postion = postion;
            //Candy = candy;
        }

        public Vector2 Postion {  get; private set; }

        public Candy Candy { get; private set; }
    }
}

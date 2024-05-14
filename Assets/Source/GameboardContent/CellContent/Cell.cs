using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class Cell
    {
        public Cell(Vector2 postion)
            => Postion = postion;

        public Vector2 Postion {  get; private set; }
    }
}

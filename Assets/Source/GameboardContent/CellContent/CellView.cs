using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class CellView : MonoBehaviour
    {
        private Cell _cell;

        public void Constuct(Cell cell, Transform parent)
        {
            _cell = cell;
            transform.position = cell.Postion;
            transform.parent = parent;
        }
    }
}

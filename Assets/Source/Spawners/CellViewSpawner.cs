using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class CellViewSpawner
    {
        public CellView Get(CellView cellViewTemplate)
        {
            return Object.Instantiate(cellViewTemplate);
        }
    }
}

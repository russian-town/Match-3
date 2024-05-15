using Sourse.GameboardContent.CellContent;
using UnityEngine;

namespace Sourse.Factories
{
    public class CellViewFactory
    {
        public CellView Get(CellView cellViewTemplate)
        {
            return Object.Instantiate(cellViewTemplate);
        }
    }
}

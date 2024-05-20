using Source.GameboardContent.CellContent;
using UnityEngine;

namespace Source.Factories
{
    public class CellViewFactory
    {
        public CellView Get(CellView cellViewTemplate)
        {
            return Object.Instantiate(cellViewTemplate);
        }
    }
}

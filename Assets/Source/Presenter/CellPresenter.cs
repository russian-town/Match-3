using Sourse.Candies;
using Sourse.GameboardContent.CellContent;

namespace Sourse.Presenter
{
    public class CellPresenter : IPresenter
    {
        private Cell _cell;
        private CellView _cellView;

        public CellPresenter(Cell cell, CellView cellView)
        {
            _cell = cell;
            _cellView = cellView;
            Index = _cell.Index;
        }

        public int Index { get; private set; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void ChangeCandy(Candy candy)
        {
            _cell.SetCandy(candy);
        }
    }
}

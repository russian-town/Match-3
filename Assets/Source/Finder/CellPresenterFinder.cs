using System.Collections.Generic;
using Source.Presenter;

namespace Source.Finder
{
    public class CellPresenterFinder
    {
        private readonly List<CellPresenter> _cellPresenters = new ();

        public CellPresenterFinder(List<CellPresenter> cellPresenters)
        {
            _cellPresenters = cellPresenters;
        }

        public CellPresenter Find(int index)
        {
            foreach (var presenter in _cellPresenters)
            {
                if(presenter.Index == index)
                    return presenter;
            }

            return null;
        }

        public CellPresenter FindByCurrentCandy(int index)
        {
            foreach (var presenter in _cellPresenters)
            {
                if (presenter.CheckCandyIndex(index))
                    return presenter;
            }

            return null;
        }
    }
}

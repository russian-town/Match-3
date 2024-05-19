using System.Collections.Generic;
using Sourse.Presenter;
using UnityEngine;

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
}

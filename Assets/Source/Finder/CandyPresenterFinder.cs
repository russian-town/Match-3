using System.Collections.Generic;
using Sourse.Presenter;
using UnityEngine;

namespace Sourse.Finder
{
    public class CandyPresenterFinder
    {
        private readonly List<CandyPresenter> _candyPresenters;

        public CandyPresenterFinder(List<CandyPresenter> candyPresenters) 
        {
            _candyPresenters = candyPresenters;
        }

        public CandyPresenter Find(int index) 
        {
            foreach (var presenter in _candyPresenters)
            {
                if(presenter.Index == index)
                    return presenter;
            }

            return null;
        }
    }
}

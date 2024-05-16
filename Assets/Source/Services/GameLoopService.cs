using System.Collections.Generic;
using Sourse.Candies;
using Sourse.Finder;
using Sourse.GameboardContent.CellContent;
using Sourse.Presenter;
using UnityEngine;

namespace Sourse.Services
{
    public class GameLoopService
    {
        private readonly GameboardPresenter _gameboardPresenter;
        private readonly List<CandyPresenter> _candyPresenters;
        private readonly List<CellPresenter> _cellPresenters;
        private readonly MatchFinder _matchFinder;

        public GameLoopService(
            GameboardPresenter gameboardPresenter,
            List<CandyPresenter> candyPresenters,
            List<CellPresenter> cellPresenters,
            MatchFinder matchFinder)
        {
            _gameboardPresenter = gameboardPresenter;
            _candyPresenters = candyPresenters;
            _cellPresenters = cellPresenters;
            _matchFinder = matchFinder;
        }

        public void Subscribe()
        {
            _gameboardPresenter.CandiesSwaped += OnCandyMoved;
        }

        public void Unsubscribe()
        {
            _gameboardPresenter.CandiesSwaped -= OnCandyMoved;
        }

        private void OnCandyMoved(Candy touchCandy, int touchCellIndex, Candy targetCandy, int targetCellIndex)
        {
            if (touchCandy == null || targetCandy == null)
                return;

            Vector2 touchPosition = touchCandy.Position;
            Vector2 targetPosition = targetCandy.Position;

            foreach (var candyPresenter in _candyPresenters)
            {
                if (candyPresenter.Index == touchCandy.Index)
                {
                    candyPresenter.Swape(targetPosition);
                    break;
                }
            }

            foreach (var candyPresenter in _candyPresenters)
            {
                if(candyPresenter.Index == targetCandy.Index)
                {
                    candyPresenter.Swape(touchPosition);
                    break;
                }
            }

            foreach (var cellPresenter in _cellPresenters)
            {
                if(cellPresenter.Index == touchCellIndex)
                {
                    cellPresenter.ChangeCandy(targetCandy);
                    break;
                }
            }

            foreach (var cellPresenter in _cellPresenters)
            {
                if(cellPresenter.Index == targetCellIndex)
                {
                    cellPresenter.ChangeCandy(touchCandy);
                    break;
                }
            }

            if(_matchFinder.HasMatch(out List<Candy> matches))
            {
                for (int i = 0; i < _candyPresenters.Count; i++) 
                {
                    for (int j = 0; j < matches.Count; j++)
                    {
                        if (_candyPresenters[i].Index == matches[j].Index)
                        {
                            _candyPresenters[i].RemoveCandy();
                        }
                    }
                }
            }
        }
    }
}

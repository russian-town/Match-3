using System.Collections.Generic;
using Sourse.Candies;
using Sourse.Finder;
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

        private List<Candy> _matches = new ();

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
            var touchCandyPresenter = _candyPresenters.Find(x => x.Index == touchCandy.Index);
            var targetCandyPresenter = _candyPresenters.Find(x => x.Index == targetCandy.Index);
            var touchCellPresenter = _cellPresenters.Find(x => x.Index == touchCellIndex);
            var targetCellPresenter = _cellPresenters.Find(x => x.Index == targetCellIndex);
            touchCandyPresenter.Swap(targetPosition);
            targetCandyPresenter.Swap(touchPosition);
            touchCellPresenter.ChangeCandy(targetCandy);
            targetCellPresenter.ChangeCandy(touchCandy);

            if(_matchFinder.HasMatch(ref _matches))
            {
                for (int i = 0; i < _candyPresenters.Count; i++)
                {
                    for (int j = 0; j < _matches.Count; j++)
                    {
                        if (_candyPresenters[i].Index == _matches[j].Index)
                        {
                            _candyPresenters[i].RemoveCandy();
                        }
                    }
                }
            }
            else
            {
                touchCandyPresenter.Swap(touchPosition);
                targetCandyPresenter.Swap(targetPosition);
                touchCellPresenter.ChangeCandy(touchCandy);
                targetCellPresenter.ChangeCandy(targetCandy);
                Debug.Log("poop");
            }
        }
    }
}

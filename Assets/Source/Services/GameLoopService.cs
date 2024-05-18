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

        private List<Candy> _matches = new ();
        private CandyPresenter _touchCandyPresenter;
        private CandyPresenter _targetCandyPresenter;
        private CellPresenter _touchCellPresenter;
        private CellPresenter _targetCellPresenter;

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

            if (touchCandy.IsRemove || targetCandy.IsRemove)
                return;

            Vector2 touchPosition = touchCandy.Position;
            Vector2 targetPosition = targetCandy.Position;
            _touchCandyPresenter = _candyPresenters.Find(x => x.Index == touchCandy.Index);
            _targetCandyPresenter = _candyPresenters.Find(x => x.Index == targetCandy.Index);
            _touchCellPresenter = _cellPresenters.Find(x => x.Index == touchCellIndex);
            _targetCellPresenter = _cellPresenters.Find(x => x.Index == targetCellIndex);
            SwapCandies(targetPosition, touchPosition, targetCandy, touchCandy);

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
                SwapCandies(touchPosition, targetPosition, touchCandy, targetCandy);
            }
        }

        private void SwapCandies
            (Vector2 touchPosition,
            Vector2 targetPosition,
            Candy touchCandy,
            Candy targetCandy)
        {
            _touchCandyPresenter.Swap(touchPosition);
            _targetCandyPresenter.Swap(targetPosition);
            _touchCellPresenter.ChangeCandy(touchCandy);
            _targetCellPresenter.ChangeCandy(targetCandy);
        }
    }
}

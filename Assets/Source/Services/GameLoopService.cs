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
        private readonly CandyPresenterFinder _candyPresenterFinder;
        private readonly CellPresenterFinder _cellPresenterFinder;
        private readonly MatchFinder _matchFinder;

        private List<Candy> _matches = new();
        private CandyPresenter _touchCandyPresenter;
        private CandyPresenter _targetCandyPresenter;
        private CellPresenter _touchCellPresenter;
        private CellPresenter _targetCellPresenter;

        public GameLoopService(
            GameboardPresenter gameboardPresenter,
            CandyPresenterFinder candyPresenterFinder,
            CellPresenterFinder cellPresenterFinder,
            MatchFinder matchFinder)
        {
            _gameboardPresenter = gameboardPresenter;
            _candyPresenterFinder = candyPresenterFinder;
            _cellPresenterFinder = cellPresenterFinder;
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

        private void OnCandyMoved(Cell touchCell, Cell targetCell)
        {
            Run(touchCell, targetCell);
        }

        private void Run(Cell touchCell, Cell targetCell)
        {
            if (touchCell == null || targetCell == null)
                return;

            if (touchCell.IsEmpty || targetCell.IsEmpty)
                return;

            Vector2 touchPosition = touchCell.WorldPosition;
            Vector2 targetPosition = targetCell.WorldPosition;
            Candy touchCandy = touchCell.Candy;
            Candy targetCandy = targetCell.Candy;
            _touchCandyPresenter = _candyPresenterFinder.Find(touchCandy.Index);
            _targetCandyPresenter = _candyPresenterFinder.Find(targetCandy.Index);
            _touchCellPresenter = _cellPresenterFinder.Find(touchCell.Index);
            _targetCellPresenter = _cellPresenterFinder.Find(targetCell.Index);
            SwapCandies(targetPosition, touchPosition, targetCandy, touchCandy);

            if (_matchFinder.HasMatch(ref _matches))
            {
                RemoveCandies();
                UpdateBoard();
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

        private void RemoveCandies()
        {
            for (int i = 0; i < _matches.Count; i++)
            {
                _candyPresenterFinder.Find(_matches[i].Index).Remove();
            }
        }

        private void UpdateBoard()
        {
            if (_gameboardPresenter.NeedUpdate(out Stack<Cell> cellsToUpdate,
                out Queue<Candy> candiesToUpdate))
            {
                Debug.Log("need update");

                while (candiesToUpdate.Count > 0)
                {
                    if (candiesToUpdate.TryDequeue(out Candy candy) == false
                        || cellsToUpdate.TryPop(out Cell cell) == false)
                        break;

                    _candyPresenterFinder.Find(candy.Index).Swap(cell.WorldPosition);
                    _cellPresenterFinder.Find(candy.Index).ChangeCandy(null);
                    _cellPresenterFinder.Find(cell.Index).ChangeCandy(candy);
                }
            }
        }
    }
}

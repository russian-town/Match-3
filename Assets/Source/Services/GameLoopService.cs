using System.Collections;
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
        private readonly MonoBehaviour _context;

        private List<Candy> _matches = new();
        private CandyPresenter _touchCandyPresenter;
        private CandyPresenter _targetCandyPresenter;
        private CellPresenter _touchCellPresenter;
        private CellPresenter _targetCellPresenter;
        private Coroutine _run;

        public GameLoopService(
            GameboardPresenter gameboardPresenter,
            List<CandyPresenter> candyPresenters,
            List<CellPresenter> cellPresenters,
            MatchFinder matchFinder,
            MonoBehaviour context)
        {
            _gameboardPresenter = gameboardPresenter;
            _candyPresenters = candyPresenters;
            _cellPresenters = cellPresenters;
            _matchFinder = matchFinder;
            _context = context;
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
            if (_run != null)
                return;

            _run =
                _context.StartCoroutine(Run(touchCell, targetCell));
        }

        private IEnumerator Run(Cell touchCell, Cell targetCell)
        {
            if (touchCell == null || targetCell == null)
                yield break;

            if (touchCell.IsEmpty || targetCell.IsEmpty)
                yield break;

            Vector2 touchPosition = touchCell.WorldPosition;
            Vector2 targetPosition = targetCell.WorldPosition;
            Candy touchCandy = touchCell.Candy;
            Candy targetCandy = targetCell.Candy;
            _touchCandyPresenter = _candyPresenters.Find(x => x.Index == touchCandy.Index);
            _targetCandyPresenter = _candyPresenters.Find(x => x.Index == targetCandy.Index);
            _touchCellPresenter = _cellPresenters.Find(x => x.Index == touchCell.Index);
            _targetCellPresenter = _cellPresenters.Find(x => x.Index == targetCell.Index);
            yield return _context.StartCoroutine(
                SwapCandies(targetPosition, touchPosition, targetCandy, touchCandy));

            if (_matchFinder.HasMatch(ref _matches))
            {
                yield return _context.StartCoroutine(RemoveCandies());
                yield return _context.StartCoroutine(UpdateBoard());
            }
            else
            {
                yield return _context.StartCoroutine(
                    SwapCandies(touchPosition, targetPosition, touchCandy, targetCandy));
            }

            _run = null;
        }

        private IEnumerator SwapCandies
            (Vector2 touchPosition,
            Vector2 targetPosition,
            Candy touchCandy,
            Candy targetCandy)
        {
            yield return _touchCandyPresenter.Swap(touchPosition);
            yield return _targetCandyPresenter.Swap(targetPosition);
            _touchCellPresenter.ChangeCandy(touchCandy);
            _targetCellPresenter.ChangeCandy(targetCandy);
            yield return null;
        }

        private IEnumerator RemoveCandies()
        {
            for (int i = 0; i < _candyPresenters.Count; i++)
            {
                for (int j = 0; j < _matches.Count; j++)
                {
                    if (_candyPresenters[i].Index == _matches[j].Index)
                    {
                        _candyPresenters[i].Remove();
                    }
                }

                yield return null;
            }
        }

        private IEnumerator UpdateBoard()
        {
            while (_gameboardPresenter.NeedUpdate(out Stack<Cell> cellsToUpdate, out Stack<Candy> candiesToUpdate))
            {
                for (int i = 0; i < candiesToUpdate.Count; i++)
                {
                    if (candiesToUpdate.TryPop(out Candy candy) == false
                        || cellsToUpdate.TryPop(out Cell cell) == false)
                        break;

                    var presenter = _candyPresenters.Find(x => x.Index == candy.Index);
                    presenter.Swap(cell.WorldPosition);
                    cell.SetCandy(candy);
                    yield return null;
                }
            }
        }
    }
}

using System.Collections.Generic;
using Sourse.Candies;
using Sourse.Presenter;
using UnityEngine;

namespace Sourse.Services
{
    public class GameLoopService
    {
        private readonly GameboardPresenter _gameboardPresenter;
        private readonly List<CandyPresenter> _candyPresenters;
        private readonly List<CellPresenter> _cellPresenters;
        private readonly List<CandyView> _candyView;

        public GameLoopService(
            GameboardPresenter gameboardPresenter,
            List<CandyPresenter> candyPresenters,
            List<CandyView> candyViews,
            List<CellPresenter> cellPresenters)
        {
            _gameboardPresenter = gameboardPresenter;
            _candyPresenters = candyPresenters;
            _cellPresenters = cellPresenters;
            _candyView = candyViews;
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
        }
    }
}

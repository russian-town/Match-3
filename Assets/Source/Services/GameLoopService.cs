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
        private readonly List<CandyView> _candyView;

        public GameLoopService(
            GameboardPresenter gameboardPresenter,
            List<CandyPresenter> candyPresenters,
            List<CandyView> candyViews)
        {
            _gameboardPresenter = gameboardPresenter;
            _candyPresenters = candyPresenters;
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

        private void OnCandyMoved(int touchCandyIndex, Candy touchCandy, int targetCandyIndex, Candy targetCandy)
        {
            Vector2 touchPosition = touchCandy.Position;
            Vector2 targetPosition = targetCandy.Position;
            _candyPresenters[touchCandyIndex].Swape(targetPosition);
            _candyPresenters[targetCandyIndex].Swape(touchPosition);
        }
    }
}

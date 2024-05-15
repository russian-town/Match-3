using Sourse.Candies;
using Sourse.Presenter;
using System;
using System.Collections.Generic;

namespace Sourse.Services
{
    public class GameLoopService
    {
        private readonly GameboardPresenter _gameboardPresenter;
        private readonly List<CandyPresenter> _candyPresenters;

        public GameLoopService(
            GameboardPresenter gameboardPresenter,
            List<CandyPresenter> candyPresenters)
        {
            _gameboardPresenter = gameboardPresenter;
            _candyPresenters = candyPresenters;
        }

        public void Subscribe()
        {
            _gameboardPresenter.CandiesSwaped += OnCandyMoved;
        }

        public void Unsubscribe() 
        {
            _gameboardPresenter.CandiesSwaped -= OnCandyMoved;
        }

        private void OnCandyMoved(Candy startCandy, Candy targetCandy)
        {
            foreach (var candyPresenter in _candyPresenters)
            {
                if (candyPresenter.CandyPosition == startCandy.Position)
                    candyPresenter.Swape(targetCandy.Position);

                if (candyPresenter.CandyPosition == targetCandy.Position)
                    candyPresenter.Swape(startCandy.Position);
            }
        }
    }
}

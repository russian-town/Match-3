using Sourse.Candies;
using Sourse.GameboardContent;
using Sourse.HUD.Input;
using System;
using UnityEngine;

namespace Sourse.Presenter
{
    public class GameboardPresenter : IPresenter
    {
        private Gameboard _gameboard;
        private GameboardView _view;
        private Touchpad _touchpad;

        private Candy _startCandy;
        private Candy _targetCandy;

        public GameboardPresenter(
            Gameboard gameboard,
            GameboardView view,
            Touchpad touchpad)
        {
            _gameboard = gameboard;
            _view = view;
            _touchpad = touchpad;
        }

        public event Action<Candy, Candy> CandiesSwaped;

        public void Enable()
        {
            _touchpad.TouchStarted += OnTouchStarted;
            _touchpad.TouchEnded += OnTouchEnded;
        }

        public void Disable()
        {
            _touchpad.TouchStarted -= OnTouchStarted;
            _touchpad.TouchEnded -= OnTouchEnded;
        }

        private void OnTouchStarted(Vector2 worldPosition)
            => _startCandy = _gameboard.GetTouchCell(worldPosition).Candy;

        private void OnTouchEnded(Vector2 worldPosition)
        {
            _targetCandy = _gameboard.GetTargetCell(worldPosition).Candy;
            CandiesSwaped?.Invoke(_startCandy, _targetCandy);
        }
    }
}

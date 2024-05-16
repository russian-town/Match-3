using Sourse.Candies;
using Sourse.GameboardContent;
using Sourse.HUD.Input;
using System;
using UnityEngine;

namespace Sourse.Presenter
{
    public class GameboardPresenter : IPresenter
    {
        private readonly Gameboard _gameboard;
        private readonly GameboardView _view;
        private readonly Touchpad _touchpad;

        private int _startCandyIndex;
        private int _targetCandyIndex;
        private Candy _touchCandy;
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

        public event Action<int, Candy, int, Candy> CandiesSwaped;

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
        {
            _startCandyIndex = _gameboard.GetTouchCellIndex(worldPosition);
            _touchCandy = _gameboard.GetCandy(_startCandyIndex);
        }

        private void OnTouchEnded(Vector2 worldPosition)
        {
            _targetCandyIndex = _gameboard.GetTargetCellIndex(worldPosition);
            _targetCandy = _gameboard.GetCandy(_targetCandyIndex);
            CandiesSwaped?.Invoke(_startCandyIndex, _touchCandy, _targetCandyIndex, _targetCandy);
        }
    }
}

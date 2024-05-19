using System;
using System.Collections.Generic;
using Sourse.Candies;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;
using Sourse.HUD.Input;
using UnityEngine;

namespace Sourse.Presenter
{
    public class GameboardPresenter : IPresenter
    {
        private readonly Gameboard _gameboard;
        private readonly GameboardView _view;
        private readonly Touchpad _touchpad;

        private Candy _touchCandy;
        private Candy _targetCandy;
        private int _touchCellIndex;
        private int _targetCellIndex;
        private Vector2 _touchPosition;

        public GameboardPresenter(
            Gameboard gameboard,
            GameboardView view,
            Touchpad touchpad)
        {
            _gameboard = gameboard;
            _view = view;
            _touchpad = touchpad;
        }

        public event Action<Candy, int, Candy, int> CandiesSwaped;

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

        public bool NeedUpdate(out Stack<Cell> cellsToUpdate, out Stack<Candy> candiesToUpdate)
        {
            _gameboard.Update(out Stack<Cell> cells, out Stack<Candy> candies);

            if (candies.Count == 0)
            {
                cellsToUpdate = null;
                candiesToUpdate = null;
                return false;
            }

            cellsToUpdate = cells;
            candiesToUpdate = candies; 
            return true;
        }

        private void OnTouchStarted(Vector2 worldPosition)
        {
            _touchPosition = worldPosition;
            _touchCandy = _gameboard.GetCandy(_touchPosition, out _touchCellIndex);
        }

        private void OnTouchEnded(Vector2 worldPosition)
        {
            _targetCandy = _gameboard.GetTargetCandy(_touchPosition, worldPosition, out _targetCellIndex);
            CandiesSwaped?.Invoke(_touchCandy, _touchCellIndex, _targetCandy, _targetCellIndex);
        }
    }
}

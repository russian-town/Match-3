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

        private Cell _touchCell;
        private Cell _targetCell;

        public GameboardPresenter(
            Gameboard gameboard,
            GameboardView view,
            Touchpad touchpad)
        {
            _gameboard = gameboard;
            _view = view;
            _touchpad = touchpad;
        }

        public event Action<Cell, Cell> CandiesSwaped;

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
            _touchCell = _gameboard.GetCell(worldPosition);
        }

        private void OnTouchEnded(Vector2 worldPosition)
        {
            if (_touchCell == null)
                return;

            _targetCell = _gameboard.GetCell(worldPosition);

            if (_targetCell == null || CellsIsValid() == false)
                return;

            CandiesSwaped?.Invoke(_touchCell, _targetCell);
        }

        private bool CellsIsValid()
        {
            if (_touchCell.WorldPosition.x - _targetCell.WorldPosition.x < -1f
                || _touchCell.WorldPosition.x - _targetCell.WorldPosition.x > 1f
                || _touchCell.WorldPosition.y - _targetCell.WorldPosition.y < -1f
                || _touchCell.WorldPosition.y - _targetCell.WorldPosition.y > 1f)
                return false;

            return true;
        }
    }
}

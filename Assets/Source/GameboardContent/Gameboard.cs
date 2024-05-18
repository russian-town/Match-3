using System.Collections.Generic;
using Sourse.Candies;
using Sourse.Configs;
using Sourse.GameboardContent.CellContent;
using UnityEngine;

namespace Sourse.GameboardContent
{
    public class Gameboard
    {
        private readonly float _divider = 2f;
        private readonly List<Cell> _cells = new ();
        private readonly GameboardConfig _config;
        private readonly List<Cell> _cellsToUpdate = new();

        public Gameboard(List<Cell> cells, GameboardConfig config)
        {
            _cells = cells;
            _config = config;
        }

        public Candy GetCandy(Vector2 position, out int cellIndex)
        {
            cellIndex = GetTouchIndex(position);

            if (cellIndex < 0 || cellIndex > _cells.Count)
                return null;

            return _cells[cellIndex].Candy;
        }

        public Candy GetTargetCandy(Vector2 touchPosition, Vector2 targetPosition, out int targetIndex)
        {
            Vector2 direction = targetPosition - touchPosition;
            Vector2 position = touchPosition + CalculateDirection(direction);
            int index = GetTouchIndex(position);

            if (index <= 0 || index > _cells.Count)
            {
                targetIndex = 0;
                return null;
            }

            targetIndex = index;
            return _cells[index].Candy;
        }

        public List<Cell> GetCellsToUpdate()
        {
            Vector2 down = new (0f, -1f);

            for (int i = _cells.Count - 1; i >= 0; i--)
            {
                Vector2 direction = _cells[i].WorldPosition + down;
                Cell downCell = GetCellByDirection(direction);

                if (downCell == null)
                    continue;

                if (_cells[i].IsEmpty == false && downCell.IsEmpty)
                {
                    _cellsToUpdate.Add(_cells[i]);
                    _cellsToUpdate.Add(downCell);
                }
            }

            return _cellsToUpdate;
        }

        private Cell GetCellByDirection(Vector2 direction)
        {
            int index = GetTouchIndex(direction);
            
            if(index < 0 || index > _cells.Count)
                return null;

            return _cells[index];
        }

        private int GetTouchIndex(Vector2 position)
        {
            int x = (int)(position.y + _config.Width / _divider);
            int y = (int)(position.x + _config.Height / _divider);

            if (x >= 0 && x < _config.Width && y >= 0 && y < _config.Height)
                return x + y * _config.Width;

            return -1;
        }

        private Vector2 CalculateDirection(Vector2 direction)
        {
            if (direction.x < direction.y && direction.x < 0)
                return Vector2.left;
            else if(direction.x > direction.y && direction.x > 0)
                return Vector2.right;
            else if(direction.y < direction.x && direction.y < 0)
                return Vector2.down;
            else if (direction.y > direction.x && direction.y > 0)
                return Vector2.up;

            Debug.Log(direction);
            return Vector2.zero;
        }
    }
}

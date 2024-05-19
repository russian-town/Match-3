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

        private Queue<Cell> _emptyCells;

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

        public void Update(out Stack<Cell> cells, out Stack<Candy> candies)
        {
            cells = new();
            candies = new();

            for (int i = 0; i < _config.Height; i++)
            {
                if (_cells[i].IsEmpty)
                {
                    cells.Push(_cells[i]);

                    for (int j = i; j < _config.Height; j++)
                    {
                        if (_cells[j].IsEmpty == false)
                        {
                            candies.Push(_cells[j].Candy);
                            break;
                        }
                    }
                }
            }
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

            return Vector2.up;
        }
    }
}

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

        public Gameboard(List<Cell> cells, GameboardConfig config)
        {
            _cells = cells;
            _config = config;
        }

        public void Update(out Stack<Cell> cells, out Queue<Candy> candies)
        {
            cells = new();
            candies = new();

            for (int i = _config.Height - 1; i >= 0; i--)
            {
                if (_cells[i].IsEmpty)
                {
                    if (i + 1 == _config.Height)
                        continue;

                    if (_cells[i + 1].IsEmpty == false)
                    {
                        cells.Push(_cells[i]);
                        candies.Enqueue(_cells[i + 1].Candy);
                    }
                }
            }
        }

        public Cell GetCell(Vector2 touchPosition)
        {
            int cellIndex = GetTouchIndex(touchPosition);

            if (cellIndex < 0 || cellIndex > _cells.Count)
                return null;

            return _cells[cellIndex];
        }

        private int GetTouchIndex(Vector2 position)
        {
            int x = (int)(position.y + _config.Width / _divider);
            int y = (int)(position.x + _config.Height / _divider);

            if (x >= 0 && x < _config.Width && y >= 0 && y < _config.Height)
                return x + y * _config.Width;

            return -1;
        }
    }
}

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

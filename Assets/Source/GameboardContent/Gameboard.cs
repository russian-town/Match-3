using System.Collections.Generic;
using Sourse.Configs;
using Sourse.GameboardContent.CellContent;
using UnityEngine;

namespace Sourse.GameboardContent
{
    public class Gameboard
    {
        private readonly float _divider = 2f;

        private List<Cell> _cells = new List<Cell>();
        private GameboardConfig _config;

        public Gameboard(List<Cell> cells, GameboardConfig config)
        {
            _cells = cells;
            _config = config;
        }

        public Cell GetTouchCell(Vector2 position) 
        {
            return GetCell(position);
        }

        public Cell GetTargetCell(Vector2 position) 
        {
            return GetCell(position);
        }

        private Cell GetCell(Vector2 position)
        {
            int x = (int)(position.x + _config.Width / _divider);
            int y = (int)(position.y + _config.Height / _divider);

            if (x >= 0 && x < _config.Width && y >= 0 && y < _config.Height)
                return _cells[x + y * _config.Width];

            return null;
        }
    }
}

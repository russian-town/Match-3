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

        public int GetTouchCellIndex(Vector2 position) 
        {
            return GetCellIndex(position);
        }

        public int GetTargetCellIndex(Vector2 position) 
        {
            return GetCellIndex(position);
        }

        public Candy GetCandy(int index)
        {
            return _cells[index].Candy;
        }

        private int GetCellIndex(Vector2 position)
        {
            int x = (int)(position.y + _config.Width / _divider);
            int y = (int)(position.x + _config.Height / _divider);

            if (x >= 0 && x < _config.Width && y >= 0 && y < _config.Height)
                return x + y * _config.Width;

            return -1;
        }
    }
}

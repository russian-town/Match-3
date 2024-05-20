using System.Collections.Generic;
using Source.Candies;
using Source.Configs;
using Source.GameboardContent.CellContent;
using UnityEngine;

namespace Source.GameboardContent
{
    public class Gameboard
    {
        private readonly float _divider = 2f;
        private readonly List<Cell> _cells;

        public Gameboard(List<Cell> cells, GameboardConfig config)
        {
            _cells = cells;

            Height = config.Height;
            Width = config.Width;
        }

        private int Height { get; }
        private int Width { get; }

        // public void Update(out Queue<Cell> cells, out Queue<Candy> candies)
        // {
        //     cells = new();
        //     candies = new();
        //
        //     for (int i = Height - 1; i >= 0; i--)
        //     {
        //         if (_cells[i].IsEmpty)
        //         {
        //             if (i + 1 == Height)
        //                 continue;
        //
        //             cells.Enqueue(_cells[i]);
        //
        //             for (int j = 0; j < Height - i; j++)
        //             {
        //                 if (_cells[i + j].IsEmpty == false)
        //                 {
        //                     cells.Enqueue(_cells[i + j]);
        //                     candies.Enqueue(_cells[i + j].Candy);
        //                 }
        //             }
        //         }
        //     }
        // }

        public Cell GetCell(Vector2 worldPosition)
        {
            int cellIndex = GetIndex(worldPosition);

            if (cellIndex < 0 || cellIndex > _cells.Count)
                return null;

            return _cells[cellIndex];
        }

        private int GetIndex(Vector2 worldPosition)
        {
            int x = (int) (worldPosition.y + Width / _divider);
            int y = (int) (worldPosition.x + Height / _divider);

            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return x + y * Width;

            return -1;
        }
    }
}
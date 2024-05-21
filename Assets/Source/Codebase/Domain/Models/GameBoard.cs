using System;
using UnityEngine;

namespace Source.Codebase.Domain.Models
{
    public class GameBoard : IPositionConverter
    {
        private readonly Cell[,] _cells;

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new Cell[width, height];

            FillCells(width, height);
        }

        public int Width { get; }
        public int Height { get; }

        public Cell this[int x, int y] =>
            _cells[x, y];

        public Vector3 GetWorldFromBoardPosition(Vector2Int boardPosition)
        {
            float xOffset = (float) -Width / 2 + 0.5f;
            float yOffset = (float) -Height / 2 + 0.5f;

            Vector2 positionOffset = new(xOffset, yOffset);

            return new Vector2(boardPosition.x, boardPosition.y) + positionOffset;
        }

        public Vector2Int GetBoardFromWorldPosition(Vector3 worldPosition)
        {
            float xOffset = (float) -Width / 2 + 0.5f;
            float yOffset = (float) -Height / 2 + 0.5f;

            Vector2 positionOffset = new(xOffset, yOffset);

            Vector2 boardPosition = new Vector2(worldPosition.x, worldPosition.y) - positionOffset;

            return new Vector2Int(Mathf.RoundToInt(boardPosition.x), Mathf.RoundToInt(boardPosition.y));
        }

        public bool IsCellExist(Vector2Int boardPosition) =>
            boardPosition.x >= 0 && boardPosition.x < Width &&
            boardPosition.y >= 0 && boardPosition.y < Height;

        public bool IsCellExist(Vector3 worldPosition)
        {
            Vector2Int boardPosition = GetBoardFromWorldPosition(worldPosition);

            return boardPosition.x >= 0 && boardPosition.x < Width &&
                   boardPosition.y >= 0 && boardPosition.y < Height;
        }

        public void SetCandy(Candy candy, Vector2Int boardPosition)
        {
            if (IsCellExist(boardPosition) == false)
                throw new Exception($"Cell with position {boardPosition} does not exist!");

            _cells[boardPosition.x, boardPosition.y].Fill(candy);
        }

        public void Swap(Cell firstCell, Cell secondCell)
        {
            if (IsCellExist(firstCell.BoardPosition) == false)
                throw new Exception($"Cell with position {firstCell.BoardPosition} does not exist!");

            if (IsCellExist(secondCell.BoardPosition) == false)
                throw new Exception($"Cell with position {secondCell.BoardPosition} does not exist!");

            firstCell.Swap(secondCell);
        }

        public Cell GetCell(Vector2Int boardPosition)
        {
            if (IsCellExist(boardPosition) == false)
                throw new Exception($"Cell with coordinates {boardPosition} does not exist!");

            return _cells[boardPosition.x, boardPosition.y];
        }

        public bool TryGetCell(Vector2Int boardPosition, out Cell cell)
        {
            cell = IsCellExist(boardPosition) ? GetCell(boardPosition) : null;

            return cell != null;
        }

        public Cell GetCellFromWorld(Vector3 worldPosition)
        {
            Vector2Int boardPosition = GetBoardFromWorldPosition(worldPosition);

            return GetCell(boardPosition);
        }

        public bool TryGetCellFromWorld(Vector3 worldPosition, out Cell cell)
        {
            cell = IsCellExist(worldPosition) ? GetCellFromWorld(worldPosition) : null;

            return cell != null;
        }

        private void FillCells(int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _cells[x, y] = new Cell(new Vector2Int(x, y));
                }
            }
        }
    }
}
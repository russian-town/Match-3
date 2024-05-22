using System;
using System.Collections.Generic;
using Source.Codebase.Domain.Models;

namespace Source.Codebase.Services
{
    public class BoardMatcher
    {
        private readonly HashSet<Cell> _matches = new();
        private readonly GameBoard _gameBoard;

        public BoardMatcher(GameBoard gameBoard)
        {
            _gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
        }

        public bool CheckForMatches()
        {
            for (int x = 0; x < _gameBoard.Width; x++)
            {
                for (int y = 0; y < _gameBoard.Height; y++)
                {
                    if (x > 1
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x - 1, y])
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x - 2, y]))
                    {
                        return true;
                    }

                    if (y > 1
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x, y - 1])
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x, y - 2]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public HashSet<Cell> GetMatches()
        {
            _matches.Clear();

            for (int x = 0; x < _gameBoard.Width; x++)
            {
                for (int y = 0; y < _gameBoard.Height; y++)
                {
                    if (x > 1
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x - 1, y])
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x - 2, y]))
                    {
                        _matches.Add(_gameBoard[x, y]);
                        _matches.Add(_gameBoard[x - 1, y]);
                        _matches.Add(_gameBoard[x - 2, y]);
                    }

                    if (y > 1
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x, y - 1])
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x, y - 2]))
                    {
                        _matches.Add(_gameBoard[x, y]);
                        _matches.Add(_gameBoard[x, y - 1]);
                        _matches.Add(_gameBoard[x, y - 2]);
                    }
                }
            }

            return _matches;
        }

        public bool CheckForDrop()
        {
            for (int x = 0; x < _gameBoard.Width; x++)
            {
                for (int y = 0; y < _gameBoard.Height; y++)
                {
                    if (_gameBoard[x, y].IsFree)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Drop()
        {
            for (int x = 0; x < _gameBoard.Width; x++)
            {
                for (int y = 0; y < _gameBoard.Height; y++)
                {
                    if (_gameBoard[x, y].IsFree)
                    {
                        int currentY = y;

                        while (_gameBoard[x, currentY].IsFree && currentY < _gameBoard.Height - 1)
                        {
                            currentY++;
                        }

                        if (currentY != y)
                        {
                            _gameBoard[x, currentY].Swap(_gameBoard[x, y]);
                        }
                    }
                }
            }
        }

        public void DestroyMatches()
        {
            HashSet<Cell> matches = GetMatches();

            foreach (Cell cell in matches)
                cell.Clear();
        }

        private bool IsCellsWithSameCandies(Cell firstCell, Cell secondCell)
        {
            return firstCell.IsFree == false
                   && secondCell.IsFree == false
                   && firstCell.Candy.Type == secondCell.Candy.Type;
        }
    }
}
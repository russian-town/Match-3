using System;
using System.Collections;
using System.Collections.Generic;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class GameLoopService : IGameLoopService
    {
        private readonly GameBoard _gameBoard;
        private readonly ICandyService _candyService;
        private readonly ICoroutineRunner _coroutineRunner;

        private Cell _selectedCell;
        private bool _inProgress;

        private Coroutine _boardUpdateRoutine;

        public GameLoopService(
            GameBoard gameBoard,
            ICandyService candyService,
            ICoroutineRunner coroutineRunner)
        {
            _gameBoard = gameBoard;
            _candyService = candyService;
            _coroutineRunner = coroutineRunner;
        }

        public void HandleCellTouch(Cell startCell, Cell endCell)
        {
            if (CanSwap(startCell, endCell) == false)
                return;

            _gameBoard.Swap(startCell, endCell);

            if (_boardUpdateRoutine != null)
                _coroutineRunner.StopCoroutine(_boardUpdateRoutine);
            
            _boardUpdateRoutine = _coroutineRunner.StartCoroutine(UpdateBoard());
        }

        private IEnumerator UpdateBoard()
        {
            do
            {
                if (CheckForMatches())
                {
                    yield return new WaitForSeconds(0.3f);

                    DestroyMatches();
                }

                if (CheckForDrop())
                {
                    yield return new WaitForSeconds(0.3f);

                    DropCandies();

                    yield return new WaitForSeconds(0.3f);

                    _candyService.FillEmptyCells(_gameBoard);
                }
            }
            while (CheckForMatches());
        }

        private bool CanSwap(Cell firstCell, Cell secondCell)
        {
            if (firstCell.IsFree || secondCell.IsFree)
                return false;

            if (firstCell.Candy.Type == secondCell.Candy.Type)
                return false;

            Vector2Int delta = firstCell.BoardPosition - secondCell.BoardPosition;
            int deltaX = Math.Abs(delta.x);
            int deltaY = Math.Abs(delta.y);

            return deltaX + deltaY == 1;
        }

        private bool CheckForMatches()
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

        private void DestroyMatches()
        {
            HashSet<Cell> matches = new HashSet<Cell>();

            for (int x = 0; x < _gameBoard.Width; x++)
            {
                for (int y = 0; y < _gameBoard.Height; y++)
                {
                    if (x > 1
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x - 1, y])
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x - 2, y]))
                    {
                        matches.Add(_gameBoard[x, y]);
                        matches.Add(_gameBoard[x - 1, y]);
                        matches.Add(_gameBoard[x - 2, y]);
                    }

                    if (y > 1
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x, y - 1])
                        && IsCellsWithSameCandies(_gameBoard[x, y], _gameBoard[x, y - 2]))
                    {
                        matches.Add(_gameBoard[x, y]);
                        matches.Add(_gameBoard[x, y - 1]);
                        matches.Add(_gameBoard[x, y - 2]);
                    }
                }
            }

            foreach (Cell cell in matches)
                cell.Clear();
        }

        private bool IsCellsWithSameCandies(Cell firstCell, Cell secondCell)
        {
            return firstCell.IsFree == false
                   && secondCell.IsFree == false
                   && firstCell.Candy.Type == secondCell.Candy.Type;
        }

        private bool CheckForDrop()
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

        private void DropCandies()
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
    }
}
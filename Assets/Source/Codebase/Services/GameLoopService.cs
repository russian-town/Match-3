using System;
using System.Collections;
using Source.Codebase.Domain.Constants;
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
        private readonly WaitForSeconds _waitBeforeDestroy;
        private readonly WaitForSeconds _waitBeforeDrop;
        private readonly WaitForSeconds _waitBeforeFill;
        private readonly BoardMatcher _boardMatcher;

        private Cell _selectedCell;
        private bool _inProgress;

        private Coroutine _boardUpdateRoutine;

        public GameLoopService(
            GameBoard gameBoard,
            ICandyService candyService,
            ICoroutineRunner coroutineRunner,
            BoardMatcher boardBoardMatcher)
        {
            _gameBoard = gameBoard;
            _candyService = candyService;
            _coroutineRunner = coroutineRunner;
            _boardMatcher = boardBoardMatcher;

            _waitBeforeDestroy = new WaitForSeconds(GameConstants.BeforeDestroyDelay);
            _waitBeforeDrop = new WaitForSeconds(GameConstants.BeforeDropDelay);
            _waitBeforeFill = new WaitForSeconds(GameConstants.BeforeFillDelay);
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
                if (_boardMatcher.CheckForMatches())
                {
                    yield return _waitBeforeDestroy;

                    _boardMatcher.DestroyMatches();
                }

                if (_boardMatcher.CheckForDrop())
                {
                    yield return _waitBeforeDrop;

                    _boardMatcher.Drop();

                    yield return _waitBeforeFill;

                    _candyService.FillEmptyCells(_gameBoard);
                }
            }
            while (_boardMatcher.CheckForMatches());
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
    }
}
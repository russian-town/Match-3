using System.Collections.Generic;
using Source.Codebase.Controllers.Inputs;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Controllers.Presenters
{
    public class GameBoardPresenter : IPresenter
    {
        private static readonly Dictionary<Direction, Vector2Int> GameBoardDirections = new()
        {
            [Direction.Down] = Vector2Int.down,
            [Direction.Left] = Vector2Int.left,
            [Direction.Right] = Vector2Int.right,
            [Direction.Up] = Vector2Int.up,
            [Direction.Unknown] = Vector2Int.zero,
        };

        private readonly GameBoardView _gameBoardView;
        private readonly GameBoard _gameBoard;
        private readonly CellViewFactory _cellViewFactory;
        private readonly ICandyService _candyService;
        private readonly ITouchpad _touchpad;
        private readonly GameLoopService _gameLoopService;

        private Vector2 _touchStartPosition;

        public GameBoardPresenter(
            GameBoardView gameBoardView,
            GameBoard gameBoard,
            CellViewFactory cellViewFactory,
            ICandyService candyService,
            ITouchpad touchpad,
            GameLoopService gameLoopService)
        {
            _gameBoardView = gameBoardView;
            _gameBoard = gameBoard;
            _cellViewFactory = cellViewFactory;
            _candyService = candyService;
            _touchpad = touchpad;
            _gameLoopService = gameLoopService;
        }

        public void Enable()
        {
            _cellViewFactory.CreateForBoard(_gameBoard, _gameBoardView.CellContainer);


            _touchpad.TouchStarted += OnTouchStarted;
            _touchpad.TouchEnded += OnTouchEnded;
        }

        public void Disable()
        {
            _touchpad.TouchStarted -= OnTouchStarted;
            _touchpad.TouchEnded -= OnTouchEnded;
        }

        private void OnTouchStarted(Vector2 worldPosition)
        {
            _touchStartPosition = worldPosition;
        }

        private void OnTouchEnded(Vector2 worldPosition)
        {
            Vector2 gameBoardDirection = GameBoardDirections[_touchpad.LastTouchDirection];

            _gameBoard.TryGetCellFromWorld(_touchStartPosition, out Cell startCell);
            _gameBoard.TryGetCellFromWorld(_touchStartPosition + gameBoardDirection, out Cell endCell);

            if (startCell == null || endCell == null || startCell == endCell)
                return;

            _gameLoopService.HandleCellTouch(startCell, endCell);
        }
    }
}
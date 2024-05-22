using System;
using System.Collections;
using Source.Codebase.Controllers.Inputs;
using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Domain.Models;
using Source.Codebase.Infrastructure.Pools;
using Source.Codebase.Presentation;
using Source.Codebase.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Codebase.Infrastructure
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private GameBoardView _gameBoardView;
        [SerializeField] private Transform _candySpawnPoint;
        [SerializeField] private Camera _camera;
        [SerializeField] private Touchpad _touchpad;
        [SerializeField] private CoroutineRunner _coroutineRunner;

        private GameLoopService _gameLoopService;

        private IEnumerator Start()
        {
            GameboardConfig gameboardConfig = _levelConfig.GameboardConfig;
            GameBoard gameBoard = new(gameboardConfig.Width, gameboardConfig.Height);
            BoardMatcher boardMatcher = new(gameBoard);

            _camera.orthographicSize = gameboardConfig.Width;

            Pool candyViewPool = new();

            StaticDataService staticDataService = new();

            CellViewFactory cellViewFactory = new(staticDataService);
            CandyViewFactory candyViewFactory = new(candyViewPool, staticDataService, gameBoard, _candySpawnPoint);

            CandyService candyService = new(candyViewFactory, staticDataService, boardMatcher);
            _gameLoopService = new GameLoopService(gameBoard, candyService, _coroutineRunner, boardMatcher);

            staticDataService.LoadConfigs(_levelConfig);

            yield return candyService.InitialFillBoard(gameBoard);

            GameBoardPresenter gameBoardPresenter = new(
                _gameBoardView,
                gameBoard,
                cellViewFactory,
                candyService,
                _touchpad,
                _gameLoopService);

            _touchpad.Construct(_camera);
            _gameBoardView.Construct(gameBoardPresenter);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
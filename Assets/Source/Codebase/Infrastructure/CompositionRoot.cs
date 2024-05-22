using Source.Codebase.Controllers.Inputs;
using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Domain.Models;
using Source.Codebase.Infrastructure.Pools;
using Source.Codebase.Presentation;
using Source.Codebase.Services;
using UnityEngine;

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

        private void Start()
        {
            GameboardConfig gameboardConfig = _levelConfig.GameboardConfig;
            GameBoard gameBoard = new(gameboardConfig.Width, gameboardConfig.Height);

            _camera.orthographicSize = gameboardConfig.Width;

            Pool candyViewPool = new();

            StaticDataService staticDataService = new();

            CellViewFactory cellViewFactory = new(staticDataService);
            CandyViewFactory candyViewFactory = new(candyViewPool, staticDataService, gameBoard, _candySpawnPoint);

            CandyService candyService = new(candyViewFactory, staticDataService);
            _gameLoopService = new GameLoopService(gameBoard, candyService, _coroutineRunner);

            staticDataService.LoadConfigs(_levelConfig);

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
    }
}
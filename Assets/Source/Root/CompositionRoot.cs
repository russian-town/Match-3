using System.Collections.Generic;
using Source.Background;
using Source.Candies;
using Source.Configs;
using Source.Factories;
using Source.Finder;
using Source.GameboardContent;
using Source.GameboardContent.CellContent;
using Source.HUD.Input;
using Source.Presenter;
using Source.Services;
using UnityEngine;

namespace Source.Root
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private BackgroundView _backgroundViewTemplate;
        [SerializeField] private GameboardView _gameboardViewTemplate;
        [SerializeField] private CellView _cellViewTemplate;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameboardConfig _gameboardConfig;
        [SerializeField] private CandyView _candyViewTemplate;
        [SerializeField] private RectTransform _hud;
        [SerializeField] private Touchpad _touchpadTemplate;
        [SerializeField] private List<CandyConfig> _candyConfigs = new();
        [SerializeField] private LevelConfig _levelConfig;

        private readonly BackgroundSizeProvider _backgroundSizeProvider = new();
        private readonly CellFactory _cellFactory = new();
        private readonly CellViewFactory _cellViewFactory = new();
        private readonly GameboardFactory _gameboardFactory = new();
        private readonly GameboardViewFactory _gameboardViewFactory = new();
        private readonly CandyFactory _candyFactory = new();
        private readonly CandyViewFactory _candyViewFactory = new();
        private readonly PresenterFactory _presenterFactory = new();
        private readonly TouchpadFactory _touchpadFactory = new();
        private readonly float _divider = 2f;
        private readonly float _cellHalfSize = .5f;
        private readonly List<CellView> _cellViews = new();
        private readonly List<CandyPresenter> _candyPresenters = new();
        private readonly List<CellPresenter> _cellPresenters = new();
        private readonly List<CandyView> _candyViews = new();

        private List<Cell> _cells = new();
        private Touchpad _touchpad;
        private Gameboard _gameboard;
        private GameboardView _gameboardView;
        private CandyPresenterFinder _candyPresenterFinder;
        private CellPresenterFinder _cellPresenterFinder;
        private GameLoopService _gameLoopService;
        private MatchFinder _matchFinder;

        private void OnDisable()
        {
            foreach (var cellView in _cellViews)
                cellView.Disable();

            _gameboardView.Disable();
        }

        private void OnDestroy()
            => _gameLoopService.Unsubscribe();

        private void Start()
            => Initialize();

        private void Initialize()
        {
            BackgroundView backgroundView = Instantiate(_backgroundViewTemplate);

            Vector2 backgroundSize = _backgroundSizeProvider.GetSize(_camera);
            backgroundView.Construct(backgroundSize);

            _gameboardView = Instantiate(_gameboardViewTemplate);
            
            _cells = _cellFactory.CreateCells(_gameboardConfig.Width, _gameboardConfig.Height);

            for (int i = 0; i < _cells.Count; i++)
            {
                CreateCellView(_cells[i]);
                CreateCandies(_cells[i], i);
            }

            _gameboard = _gameboardFactory.Get(_cells, _gameboardConfig);
            float xPosition = -_gameboardConfig.Width / _divider + _cellHalfSize;
            float yPosition = -_gameboardConfig.Height / _divider + _cellHalfSize;
            Vector2 gameboardViewPosition = new(xPosition, yPosition);
            _touchpad = _touchpadFactory.Get(_touchpadTemplate, _hud);
            _touchpad.Construct(_camera);
            GameboardPresenter gameboardPresenter = new(_gameboard, _gameboardView, _touchpad);
            _gameboardView.Construct(gameboardViewPosition, gameboardPresenter);
            _gameboardView.Enable();
            _matchFinder = new(_cells, _gameboardConfig);
            _candyPresenterFinder = new(_candyPresenters);
            _cellPresenterFinder = new(_cellPresenters);
            _gameLoopService = new(gameboardPresenter, _candyPresenterFinder, _cellPresenterFinder, _matchFinder);
            _gameLoopService.Subscribe();
        }

        private void CreateCellView(Cell cell)
        {
            CellView cellView = _cellViewFactory.Get(_cellViewTemplate);
            _cellViews.Add(cellView);
            CellPresenter cellPresenter = new(cell, cellView);
            cellView.Constuct(cellPresenter, cell.WorldPosition, _gameboardView.transform);
            _cellPresenters.Add(cellPresenter);
        }

        private void CreateCandies(Cell cell, int cellIndex)
        {
            int randomConfigIndex = Random.Range(0, _candyConfigs.Count);
            Candy candy = _candyFactory.Get(
                cell.WorldPosition,
                cellIndex,
                _candyConfigs[randomConfigIndex].Type);
            CandyView candyView = _candyViewFactory.Get(_candyViewTemplate);
            CandyPresenter candyPresenter =
                new(candy, candyView, _levelConfig.CandyMoveSpeed, this);
            candyView.Construct(
                cell.WorldPosition,
                _gameboardView.transform,
                _candyConfigs[randomConfigIndex],
                candyPresenter);
            cell.SetCandy(candy);
            _candyPresenters.Add(candyPresenter);
            _candyViews.Add(candyView);
        }
    }
}
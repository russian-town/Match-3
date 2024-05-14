using System.Collections.Generic;
using Sourse.Background;
using Sourse.Candies;
using Sourse.Configs;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;
using Sourse.HUD.Input;
using Sourse.Spawners;
using UnityEngine;

namespace Sourse.Root
{
    public class CompossitionRoot : MonoBehaviour
    {
        private readonly BackgroundSizeChanger _backgroundSizeChanger = new ();
        private readonly BackgroundViewSpawners _backgroundViewSpawners = new ();
        private readonly CellSpawner _cellSpawner = new();
        private readonly CellViewSpawner _cellViewSpawners = new ();
        private readonly GameboardSpawner _gameboardSpawner = new();
        private readonly GameboardViewSpawner _gameboardViewSpawner = new ();
        private readonly CandySpawner _candySpawner = new();
        private readonly CandyViewSpawner _candyViewSpawner = new ();
        private readonly TouchpadSpawner _touchpadSpawner = new();
        private readonly float _divider = 2f;
        private readonly float _cellHalfSize = .5f;

        [SerializeField] private BackgroundView _backgroundViewTemplate;
        [SerializeField] private GameboardView _gameboardViewTemplate;
        [SerializeField] private CellView _cellViewTemplate;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameboardConfig _gameboardConfig;
        [SerializeField] private CandyView _candyViewTemplate;
        [SerializeField] private RectTransform _hud;
        [SerializeField] private Touchpad _touchpadTemplate;

        private List<Cell> _cells = new ();
        private Gameboard _gameboard;
        private GameboardView _gameboardView;

        public void Start()
            => Initialize();

        private void Initialize()
        {
            Vector2 backgroundSize = _backgroundSizeChanger.GetSize(_camera);
            BackgroundView backgroundView = _backgroundViewSpawners.Get(_backgroundViewTemplate);
            backgroundView.Construct(backgroundSize);
            _cells = _cellSpawner.Get(_gameboardConfig.Width, _gameboardConfig.Height);
            _gameboard = _gameboardSpawner.Get(_cells, _gameboardConfig);
            _gameboardView = _gameboardViewSpawner.Get(_gameboardViewTemplate);

            foreach (var cell in _cells)
            {
                CreateCellView(cell);
                CreateCandies(cell);
            }

            Vector2 gameboardViewPosition =
             new(
                 -_gameboardConfig.Width / _divider + _cellHalfSize,
                 -_gameboardConfig.Height / _divider + _cellHalfSize);
            _gameboardView.Construct(gameboardViewPosition);
            Touchpad touchpad = _touchpadSpawner.Get(_touchpadTemplate, _hud);
            touchpad.Construct(_camera, _gameboard);
        }

        private void CreateCellView(Cell cell)
        {
            CellView cellView = _cellViewSpawners.Get(_cellViewTemplate);
            cellView.Constuct(cell, _gameboardView.transform);
        }

        private void CreateCandies(Cell cell)
        {
            Candy candy = _candySpawner.Get(cell.Postion);
            CandyView candyView = _candyViewSpawner.Get(_candyViewTemplate);
            candyView.Construct(candy, _gameboardView.transform);
        }
    }
}

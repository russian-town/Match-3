using Sourse.Background;
using Sourse.Configs;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;
using Sourse.Spawners;
using System.Collections.Generic;
using UnityEngine;

namespace Sourse.Root
{
    public class CompossitionRoot : MonoBehaviour
    {
        private readonly BackgroundSizeChanger _backgroundSizeChanger = new ();
        private readonly BackgroundViewSpawners _backgroundViewSpawners = new ();
        private readonly GameboardViewSpawner _gameboardViewSpawner = new ();
        private readonly CellSpawner _cellSpawner = new();
        private readonly CellViewSpawner _cellViewSpawners = new ();
        private readonly GameboardSpawner _gameboardSpawner = new();
        private readonly float _divider = 2f;

        [SerializeField] private BackgroundView _backgroundViewTemplate;
        [SerializeField] private GameboardView _gameboardViewTemplate;
        [SerializeField] private CellView _cellViewTemplate;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameboardConfig _gameboardConfig;

        private List<Cell> _cells = new ();
        private Gameboard _gameboard;

        public void Start()
            => Initialize();

        private void Initialize()
        {
            Vector2 backgroundSize = _backgroundSizeChanger.GetSize(_camera);
            BackgroundView backgroundView = _backgroundViewSpawners.Get(_backgroundViewTemplate);
            backgroundView.Construct(backgroundSize);
            CreateGameboard();
            CreateGameboardView();
        }

        private void CreateGameboard()
        {
            _cells = _cellSpawner.Get(_gameboardConfig.Width, _gameboardConfig.Height);
            _gameboard = _gameboardSpawner.Get(_cells); 
        }

        private void CreateGameboardView()
        {
            GameboardView gameboardView = _gameboardViewSpawner.Get(_gameboardViewTemplate);

            foreach (var cell in _cells)
            {
                CellView cellView = _cellViewSpawners.Get(_cellViewTemplate);
                cellView.Constuct(cell, gameboardView.transform);
            }

            Vector2 gameboardViewPosition =
                new Vector2(-_gameboardConfig.Width / _divider, -_gameboardConfig.Height / _divider);
            gameboardView.Construct(gameboardViewPosition);
        }
    }
}

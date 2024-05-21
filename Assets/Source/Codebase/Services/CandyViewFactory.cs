using System;
using JetBrains.Annotations;
using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Codebase.Services
{
    public class CandyViewFactory : ICandyViewFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPositionConverter _positionConverter;
        private readonly Transform _candySpawnPoint;

        public CandyViewFactory(
            IStaticDataService staticDataService,
            IPositionConverter positionConverter,
            Transform candySpawnPoint)
        {
            _staticDataService = staticDataService ?? throw new ArgumentNullException(nameof(staticDataService));
            _positionConverter = positionConverter ?? throw new ArgumentNullException(nameof(positionConverter));
            _candySpawnPoint =
                candySpawnPoint ? candySpawnPoint : throw new ArgumentNullException(nameof(candySpawnPoint));
        }

        public void Create(Candy candy)
        {
            CandyView viewTemplate = _staticDataService.GetViewTemplate<CandyView>();
            CandyView candyView = Object.Instantiate(viewTemplate, _candySpawnPoint.position, Quaternion.identity);

            CandyPresenter candyPresenter = new(candyView, candy, _positionConverter);

            CandyConfig candyConfig = _staticDataService.GetCandyConfig(candy.Type);
            candyView.SetSprite(candyConfig.Sprite);

            candyView.Construct(candyPresenter);
        }
    }
}
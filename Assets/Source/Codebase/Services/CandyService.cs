using System.Linq;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using Source.Codebase.Services.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Codebase.Services
{
    public class CandyService : ICandyService
    {
        private readonly ICandyViewFactory _candyViewFactory;
        private readonly IStaticDataService _staticDataService;

        public CandyService(ICandyViewFactory candyViewFactory, IStaticDataService staticDataService)
        {
            _candyViewFactory = candyViewFactory;
            _staticDataService = staticDataService;
        }

        public void InitialFillBoard(GameBoard gameBoard)
        {
            CandyType[] availableTypes = _staticDataService.CandyConfigs.Select(config => config.Type).ToArray();

            for (int y = 0; y < gameBoard.Height; y++)
            {
                for (int x = 0; x < gameBoard.Width; x++)
                {
                    Vector2Int boardPosition = new(x, y);
                    CandyType candyType = GetRandomCandyType(availableTypes);

                    Candy candy = new(candyType, boardPosition);

                    _candyViewFactory.Create(candy);

                    gameBoard.SetCandy(candy, boardPosition);
                }
            }
        }

        public void FillEmptyCells(GameBoard gameBoard)
        {
            CandyType[] availableTypes = _staticDataService.CandyConfigs.Select(config => config.Type).ToArray();

            for (int y = 0; y < gameBoard.Height; y++)
            {
                for (int x = 0; x < gameBoard.Width; x++)
                {
                    if (gameBoard[x, y].IsFree == false)
                        continue;

                    Vector2Int boardPosition = new(x, y);
                    CandyType candyType = GetRandomCandyType(availableTypes);

                    Candy candy = new(candyType, boardPosition);

                    _candyViewFactory.Create(candy);

                    gameBoard.SetCandy(candy, boardPosition);
                }
            }
        }

        private CandyType GetRandomCandyType(CandyType[] availableTypes) =>
            availableTypes[Random.Range(0, availableTypes.Length)];
    }
}
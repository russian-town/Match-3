using System.Collections;
using System.Collections.Generic;
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
        private readonly BoardMatcher _boardMatcher;

        public CandyService(
            ICandyViewFactory candyViewFactory,
            IStaticDataService staticDataService,
            BoardMatcher boardMatcher)
        {
            _candyViewFactory = candyViewFactory;
            _staticDataService = staticDataService;
            _boardMatcher = boardMatcher;
        }

        public IEnumerator InitialFillBoard(GameBoard gameBoard)
        {
            CandyType[] availableTypes = _staticDataService.CandyConfigs.Select(config => config.Type).ToArray();

            List<Candy> candies = new List<Candy>();

            do
            {
                yield return null;

                candies.Clear();

                for (int y = 0; y < gameBoard.Height; y++)
                {
                    for (int x = 0; x < gameBoard.Width; x++)
                    {
                        Vector2Int boardPosition = new(x, y);
                        CandyType candyType = GetRandomCandyType(availableTypes);
                        Candy candy = new(candyType, boardPosition);
                        candies.Add(candy);
                        gameBoard.SetCandy(candy, candy.BoardPosition);
                    }
                }
            }
            while (_boardMatcher.CheckForMatches());

            foreach (Candy candy in candies)
                _candyViewFactory.Create(candy);
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
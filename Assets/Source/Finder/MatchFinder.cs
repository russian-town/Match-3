using System.Collections.Generic;
using Sourse.Candies;
using Sourse.Configs;
using Sourse.Constants;
using Sourse.GameboardContent.CellContent;
using UnityEngine;

namespace Sourse.Finder
{
    public class MatchFinder
    {
        private List<Cell> _cells = new ();
        private GameboardConfig _gameboardConfig;

        public MatchFinder(List<Cell> cells, GameboardConfig gameboardConfig)
        {
            _cells = cells;
            _gameboardConfig = gameboardConfig;
        }

        public bool HasMatch(out List<Candy> matches)
        {
            List<Candy> candies = new ();

            for (int i = 0; i < _gameboardConfig.Width; i++)
            {
                for (int j = 0; j < _gameboardConfig.Height; j++)
                {
                    if (_cells[i].CandyType == _cells[j].CandyType)
                    {
                        candies.Add(_cells[i].Candy);
                        candies.Add(_cells[j].Candy);
                        Debug.Log("poop");
                    }

                    if(candies.Count >= GameParameter.MinCandyCountToMatch)
                    {
                        matches = candies;
                        candies.Clear ();
                        Debug.Log(matches.Count);
                        return true;
                    }
                }
            }

            matches = null;
            return false;
        }
    }
}

using System.Collections.Generic;
using Sourse.Candies;
using Sourse.Configs;
using Sourse.Constants;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;
using UnityEngine;

namespace Sourse.Finder
{
    public class MatchFinder
    {
        private List<Cell> _cells = new ();
        private GameboardConfig _gameboardConfig;
        private Gameboard _gameboard;

        public MatchFinder(
            List<Cell> cells,
            GameboardConfig gameboardConfig,
            Gameboard gameboard)
        {
            _cells = cells;
            _gameboardConfig = gameboardConfig;
            _gameboard = gameboard;
        }

        public bool HasMatch(out List<Candy> matches)
        {
            List<Candy> temp = new ();

            for (int i = 0; i < _gameboardConfig.Width; i++)
            {
                if (i + 1 == _cells.Count - 1)
                    break;

                if (_cells[i].CandyType == _cells[i + 1].CandyType)
                {
                    temp.Add(_cells[i].Candy);
                    temp.Add(_cells[i + 1].Candy);
                }
                else
                {
                    if (temp.Count >= GameParameter.MinCandyCountToMatch)
                    {
                        matches = temp;
                        return true;
                    }

                    temp.Clear();
                }
            }

            matches = null;
            return false;
        }
    }
}

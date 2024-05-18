using System;
using System.Collections;
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
        private readonly List<Cell> _cells = new ();
        private readonly GameboardConfig _gameboardConfig;

        private List<Candy> _match;

        public MatchFinder(List<Cell> cells, GameboardConfig gameboardConfig)
        {
            _cells = cells;
            _gameboardConfig = gameboardConfig;
        }

        public bool HasMatch(ref List<Candy> match)
        {
            List<Candy> temp = new();
            _match = new ();
            ScanRow(ref temp);
            ScanColum(ref temp);

            if (_match.Count > 0)
            {
                match = _match;
                return true;
            }

            match = null;
            return false;
        }

        private void ScanRow(ref List<Candy> temp)
        {
            for (int i = 0; i < _gameboardConfig.Width; i++)
            {
                for (int j = i; j < _cells.Count; j += _gameboardConfig.Width)
                    Scan(j, j + _gameboardConfig.Width, ref temp);

                temp.Clear();
            }
        }

        private void ScanColum(ref List<Candy> temp)
        {
            int index = -1;

            for (int i = 0; i < _gameboardConfig.Width; i++)
            {
                for (int j = 0; j < _gameboardConfig.Height; j++)
                {
                    index++;
                    Scan(index, index + 1, ref temp);
                }

                temp.Clear();
            }
        }

        private void Scan(int index, int nextIndex, ref List<Candy> temp)
        {
            if (nextIndex > _cells.Count || nextIndex == _cells.Count)
            {
                if (temp.Count < 2)
                    return;

                if (temp[temp.Count - 1].Type == _cells[index].CandyType)
                    AddMatch(ref temp, index);

                return;
            }

            if (_cells[nextIndex].CandyType == _cells[index].CandyType)
                temp.Add(_cells[index].Candy);
            else
                AddMatch(ref temp, index);
        }

        private void AddMatch(ref List<Candy> temp, int index)
        {
            temp.Add(_cells[index].Candy);

            if (temp.Count >= GameParameter.MinCandyCountToMatch)
                _match.AddRange(temp);

            temp.Clear();
        }
    }
}
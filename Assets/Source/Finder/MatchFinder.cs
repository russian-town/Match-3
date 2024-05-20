using System.Collections.Generic;
using Source.Candies;
using Source.Configs;
using Source.Constants;
using Source.GameboardContent.CellContent;

namespace Source.Finder
{
    public class MatchFinder
    {
        private readonly List<Cell> _cells = new ();
        private readonly GameboardConfig _gameboardConfig;

        private List<Candy> _match = new ();

        public MatchFinder(List<Cell> cells, GameboardConfig gameboardConfig)
        {
            _cells = cells;
            _gameboardConfig = gameboardConfig;
        }

        public bool HasMatch(ref List<Candy> match)
        {
            List<Candy> temp = new ();
            ScanRow(ref temp);
            ScanColum(ref temp);

            if (_match.Count > 0)
            {
                match = new ();
                match.AddRange(_match);
                _match.Clear();
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
                {
                    if (_cells[j].IsEmpty)
                        continue;

                    Scan(j, j + _gameboardConfig.Width, ref temp);
                }

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

                    if (_cells[index].IsEmpty)
                        continue;

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

            if (!_cells[nextIndex].IsEmpty && _cells[nextIndex].CandyType == _cells[index].CandyType)
                temp.Add(_cells[index].Candy);
            else
                AddMatch(ref temp, index);
        }

        private void AddMatch(ref List<Candy> temp, int index)
        {
            temp.Add(_cells[index].Candy);

            if (IsValid(ref temp) == false)
                return;

            if(IsValid(ref temp) == false)
                return;

            _match.AddRange(temp);
            temp.Clear();
        }

        private bool IsValid(ref List<Candy> temp)
        {
            if (temp == null || temp.Count < GameParameter.MinCandyCountToMatch)
            {
                temp.Clear();
                return false;
            }

            return true;
        }
    }
}

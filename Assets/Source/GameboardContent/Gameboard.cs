using System.Collections.Generic;
using Sourse.GameboardContent.CellContent;

namespace Sourse.GameboardContent
{
    public class Gameboard
    {
        private List<Cell> _cells = new List<Cell>();

        public Gameboard(List<Cell> cells)
            => _cells = cells;
    }
}

using System.Collections.Generic;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;

namespace Sourse.Spawners
{
    public class GameboardSpawner
    {
        public Gameboard Get(List<Cell> cells)
        {
            return new Gameboard(cells);
        }
    }
}

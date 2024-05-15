using System.Collections.Generic;
using Sourse.Configs;
using Sourse.GameboardContent;
using Sourse.GameboardContent.CellContent;

namespace Sourse.Factories
{
    public class GameboardFactory
    {
        public Gameboard Get(List<Cell> cells, GameboardConfig config)
        {
            return new Gameboard(cells, config);
        }
    }
}

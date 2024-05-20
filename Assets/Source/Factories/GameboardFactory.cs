using System.Collections.Generic;
using Source.Configs;
using Source.GameboardContent;
using Source.GameboardContent.CellContent;

namespace Source.Factories
{
    public class GameboardFactory
    {
        public Gameboard Get(List<Cell> cells, GameboardConfig config)
        {
            return new Gameboard(cells, config);
        }
    }
}

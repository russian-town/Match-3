using System.Collections.Generic;
using UnityEngine;

namespace Sourse.GameboardContent.CellContent
{
    public class CellSpawner
    {
        public List<Cell> Get(int width, int height)
        {
            List<Cell> tempCells = new ();

            for (int i = 0; i < width; i++) 
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 currentPosition = new Vector2(i, j);
                    Cell cell = new Cell(currentPosition);
                    tempCells.Add(cell);
                }
            }

            return tempCells;
        }
    }
}

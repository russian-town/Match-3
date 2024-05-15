using System.Collections.Generic;
using Sourse.GameboardContent.CellContent;
using UnityEngine;

namespace Sourse.Factories
{
    public class CellFactory
    {
        public List<Cell> Get(int width, int height)
        {
            List<Cell> tempCells = new ();

            for (int i = 0; i < width; i++) 
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 currentPosition = new (i, j);
                    Cell cell = new (currentPosition);
                    tempCells.Add(cell);
                }
            }

            return tempCells;
        }
    }
}

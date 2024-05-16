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
            int index = 0;

            for (int i = 0; i < width; i++) 
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 currentPosition = new (i, j);
                    Cell cell = new (currentPosition, index);
                    tempCells.Add(cell);
                    index++;
                    Debug.Log(index);
                }
            }

            return tempCells;
        }
    }
}

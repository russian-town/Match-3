using System.Collections.Generic;
using Source.GameboardContent.CellContent;
using UnityEngine;

namespace Source.Factories
{
    public class CellFactory
    {
        public List<Cell> CreateCells(int boardWidth, int boardHeight)
        {
            List<Cell> tempCells = new ();
            int index = 0;

            for (int i = 0; i < boardWidth; i++) 
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    Vector2 currentPosition = new (i, j);
                    Cell cell = new (currentPosition, index);
                    tempCells.Add(cell);
                    index++;
                }
            }

            return tempCells;
        }
    }
}

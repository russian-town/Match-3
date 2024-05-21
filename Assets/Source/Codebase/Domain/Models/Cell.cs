using UnityEngine;

namespace Source.Codebase.Domain.Models
{
    public class Cell
    {
        public Cell(Vector2Int boardPosition)
        {
            BoardPosition = boardPosition;
        }

        public Vector2Int BoardPosition { get; }
        public Candy Candy { get; private set; }
        
        public bool IsFree => Candy == null;

        public void Fill(Candy candy)
        {
            Candy = candy;
            Candy?.SetBoardPosition(BoardPosition);
        }

        public void Swap(Cell targetCell)
        {
            Candy candyFromTarget = targetCell.Candy;
            
            targetCell.Fill(Candy);
            Fill(candyFromTarget);
        }

        public void Clear()
        {
            Candy?.Destroy();
            Candy = null;
        }
    }
}
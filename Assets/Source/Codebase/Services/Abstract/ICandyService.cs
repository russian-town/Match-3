using Source.Codebase.Domain.Models;

namespace Source.Codebase.Services.Abstract
{
    public interface ICandyService
    {
        void InitialFillBoard(GameBoard gameBoard);
        void FillEmptyCells(GameBoard gameBoard);
    }
}
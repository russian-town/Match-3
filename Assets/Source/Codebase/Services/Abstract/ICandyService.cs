using System.Collections;
using Source.Codebase.Domain.Models;

namespace Source.Codebase.Services.Abstract
{
    public interface ICandyService
    {
        IEnumerator InitialFillBoard(GameBoard gameBoard);
        void FillEmptyCells(GameBoard gameBoard);
    }
}
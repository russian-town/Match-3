using Source.Codebase.Domain.Models;

namespace Source.Codebase.Services.Abstract
{
    public interface IGameLoopService
    {
        void HandleCellTouch(Cell startCell, Cell endCell);
    }
}
using Source.Codebase.Domain.Models;
using UnityEngine;

namespace Source.Codebase.Services.Abstract
{
    public interface ICellViewFactory
    {
        void CreateForBoard(GameBoard gameBoard, Transform parent);
    }
}
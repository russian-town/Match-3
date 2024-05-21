using System;
using JetBrains.Annotations;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Codebase.Services
{
    public class CellViewFactory : ICellViewFactory
    {
        private readonly IStaticDataService _staticDataService;

        public CellViewFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService ?? throw new ArgumentNullException(nameof(staticDataService));
        }

        public void CreateForBoard(GameBoard gameBoard, Transform parent)
        {
            CellView viewTemplate = _staticDataService.GetViewTemplate<CellView>();

            for (int y = 0; y < gameBoard.Height; y++)
            {
                for (int x = 0; x < gameBoard.Width; x++)
                {
                    Vector3 position = gameBoard.GetWorldFromBoardPosition(new Vector2Int(x, y));
                    Object.Instantiate(viewTemplate, position, Quaternion.identity, parent);
                }
            }
        }
    }
}
using UnityEngine;

namespace Source.Codebase.Domain
{
    public interface IPositionConverter
    {
        Vector3 GetWorldFromBoardPosition(Vector2Int boardPosition);
        Vector2Int GetBoardFromWorldPosition(Vector3 worldPosition);
    }
}
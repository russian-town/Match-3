using UnityEngine;

namespace Sourse.Configs
{
    [CreateAssetMenu(fileName = "Gameboard Config", menuName = "Gameboard Config/New Gameboard Config", order = 59)]
    public class GameboardConfig : ScriptableObject
    {
        [field: SerializeField] public int Height;
        [field: SerializeField] public int Width;
    }
}

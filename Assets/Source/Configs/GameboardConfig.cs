using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "Gameboard Config", menuName = "Gameboard Config/New Gameboard Config", order = 59)]
    public class GameboardConfig : ScriptableObject
    {
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField] public int Width { get; private set; }
    }
}
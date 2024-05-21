using UnityEngine;

namespace Source.Codebase.Domain.Configs
{
    [CreateAssetMenu(fileName = "Match3/Candy Config", menuName = "Candy Config/New Candy Config", order = 59)]
    public class CandyConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public CandyType Type { get; private set;}
    }
}

using Source.Candies;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "Candy Config", menuName = "Candy Config/New Candy Config", order = 59)]
    public class CandyConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }

        [field: SerializeField] public CandyType Type { get; private set;}
    }
}

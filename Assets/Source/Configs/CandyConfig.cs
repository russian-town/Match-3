using Sourse.Candies;
using UnityEngine;

namespace Sourse.Configs
{
    [CreateAssetMenu(fileName = "Candy Config", menuName = "Candy Config/New Candy Config", order = 59)]
    public class CandyConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Texture { get; private set; }

        [field: SerializeField] public CandyType Type { get; private set;}
    }
}

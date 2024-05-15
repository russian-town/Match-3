using UnityEngine;

[CreateAssetMenu(fileName = "Candy Config", menuName = "Candy Config/New Candy Config", order = 59)]
public class CandyConfig : ScriptableObject
{
    [field: SerializeField] public Sprite Texture {  get; private set; }
}

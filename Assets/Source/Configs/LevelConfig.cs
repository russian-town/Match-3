using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Level Config/New Level Config", order = 59)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public float CandyMoveSpeed { get; private set; }
    }
}
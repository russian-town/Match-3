using UnityEngine;

namespace Sourse.Configs
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Level Config/New Level Config", order = 59)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public float CandyMoveSpeed;
    }
}

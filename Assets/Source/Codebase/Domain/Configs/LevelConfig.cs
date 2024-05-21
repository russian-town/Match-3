using Source.Codebase.Presentation;
using UnityEngine;

namespace Source.Codebase.Domain.Configs
{
    [CreateAssetMenu(fileName = "Match3/Level Config", menuName = "Level Config/New Level Config", order = 59)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public GameboardConfig GameboardConfig { get; private set; }
        [field: SerializeField] public CellView CellViewTemplate { get; private set; }
        [field: SerializeField] public CandyView CandyViewTemplate { get; private set; }
        [field: SerializeField] public CandyConfig[] CandyConfigs { get; private set; }
    }
}
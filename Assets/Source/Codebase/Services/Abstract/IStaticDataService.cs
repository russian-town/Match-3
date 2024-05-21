using Source.Codebase.Domain;
using Source.Codebase.Domain.Configs;
using UnityEngine;

namespace Source.Codebase.Services.Abstract
{
    public interface IStaticDataService
    {
        CandyConfig[] CandyConfigs { get; }
        void LoadConfigs(LevelConfig levelConfig);
        CandyConfig GetCandyConfig(CandyType candyType);
        T GetViewTemplate<T>() where T : MonoBehaviour;
    }
}
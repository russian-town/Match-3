using System;
using System.Collections.Generic;
using System.Linq;
using Source.Codebase.Domain;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, object> _viewTemplateByTypes = new();

        private Dictionary<CandyType, CandyConfig> _candyConfigByType = new();

        public CandyConfig[] CandyConfigs => _candyConfigByType.Values.ToArray();

        public void LoadConfigs(LevelConfig levelConfig)
        {
            LoadCandyConfigs(levelConfig.CandyConfigs);

            _viewTemplateByTypes.Clear();
            _viewTemplateByTypes.Add(typeof(CandyView), levelConfig.CandyViewTemplate);
            _viewTemplateByTypes.Add(typeof(CellView), levelConfig.CellViewTemplate);
        }

        public CandyConfig GetCandyConfig(CandyType candyType)
        {
            if (_candyConfigByType.ContainsKey(candyType) == false)
                throw new Exception($"CandyConfig for CandyType {candyType} does not exist!");

            return _candyConfigByType[candyType];
        }

        public T GetViewTemplate<T>() where T : MonoBehaviour
        {
            if (_viewTemplateByTypes.TryGetValue(typeof(T), out object viewTemplate))
                return viewTemplate as T;

            throw new Exception($"Can't find viewTemplate with given type: {typeof(T)} ");
        }

        private void LoadCandyConfigs(CandyConfig[] candyConfigs)
        {
            if (candyConfigs.Length != candyConfigs.Distinct().Count())
                throw new Exception("All candiConfigs must be distinct");

            _candyConfigByType =
                candyConfigs.ToDictionary(candyConfig => candyConfig.Type, configConfig => configConfig);
        }
    }
}
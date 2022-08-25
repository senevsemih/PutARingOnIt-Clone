using System;
using System.Collections.Generic;
using Scripts.PutARingOnIt.Other;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _Player;
        [SerializeField] private List<LevelManager> _Levels = new();

        private LevelManager _currentLevel;
        private GameConfig _config;

        private void Awake()
        {
            _config = GameConfig.Instance;
        }

        private void Start()
        {
            LevelLoader();
            SetSettings();
        }

        private void LevelLoader()
        {
            _currentLevel = Instantiate(_Levels[_config.LevelIndex], transform);
        }

        private void SetSettings()
        {
            _Player.Init(_currentLevel.SplineComputer);
        }
    }
}
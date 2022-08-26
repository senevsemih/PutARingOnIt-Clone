using System.Collections.Generic;
using PutARingOnIt.GameElements.Player;
using PutARingOnIt.Other;
using UnityEngine;

namespace PutARingOnIt.GameElements
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _PlayerController;
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
            _PlayerController.Init(_currentLevel.SplineComputer);
        }
    }
}
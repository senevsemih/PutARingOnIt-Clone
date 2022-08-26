using System;
using System.Collections.Generic;
using DG.Tweening;
using PutARingOnIt.GameElements.Player;
using PutARingOnIt.Other;
using UnityEngine;

namespace PutARingOnIt.GameElements
{
    public class GameManager : MonoBehaviour
    {
        public static event Action DidLevelLoad;

        [SerializeField] private PlayerController _PlayerController;
        [SerializeField] private List<LevelManager> _Levels = new();

        private LevelManager _currentLevel;
        private GameConfig _config;

        private void Awake()
        {
            _config = GameConfig.Instance;
            UIManager.DidLevelPass += OnDidLevelPass;
        }
        
        private void Start() => LevelLoader();

        private void OnDidLevelPass() => LevelLoader();

        private void LevelLoader()
        {
            var currentLevelIndex = _config.LevelIndex;
            int loadIndex;
            
            if (currentLevelIndex <= _Levels.Count - 1)
            {
                loadIndex = currentLevelIndex;
            }
            else
            {
                _config.LevelIndex = 0;
                loadIndex = _config.LevelIndex;
            }
            
            if (_currentLevel)
            {
                DOTween.Clear();
                Destroy(_currentLevel.gameObject);
            }
            _currentLevel = Instantiate(_Levels[loadIndex], transform);
            SetSettings();
            
            DidLevelLoad?.Invoke();
        }

        private void SetSettings() => _PlayerController.Init(_currentLevel.SplineComputer);
    }
}
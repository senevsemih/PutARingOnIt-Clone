using System;
using System.Collections.Generic;
using DG.Tweening;
using PutARingOnIt.GameElements.Player;
using PutARingOnIt.Other;
using UnityEngine;

namespace PutARingOnIt.GameElements.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static event Action DidLevelLoad;

        [SerializeField] private PlayerController _PlayerController;
        [SerializeField] private List<LevelController> _Levels = new();

        private LevelController _currentLevel;
        private GameConfig _config;

        private void Awake()
        {
            _config = GameConfig.Instance;
            UIController.DidLevelPass += OnDidLevelPass;
        }

        private void Start()
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            LevelLoader();
        }

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
using System.Collections.Generic;
using Scripts.PutARingOnIt.Other;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfig _Config;
        
        [SerializeField] private InputController _Input;
        [SerializeField] private Player _Player;
        [SerializeField] private List<LevelManager> _Levels = new();

        private LevelManager _currentLevel;
        
        private void Start()
        {
            
            // _Input.SetActive(true);
            
            LevelLoader();
            SetSettings();
        }

        private void LevelLoader()
        {
            _currentLevel = Instantiate(_Levels[_Config.LevelIndex], transform);
        }

        private void SetSettings()
        {
            _Player.Init(_currentLevel.SplineComputer);
        }
    }
}
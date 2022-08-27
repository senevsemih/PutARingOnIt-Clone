using System;
using DG.Tweening;
using PutARingOnIt.GameElements.Player;
using PutARingOnIt.Other;
using TMPro;
using UnityEngine;

namespace PutARingOnIt.GameElements.Controllers
{
    public class UIController : MonoBehaviour
    {
        public static event Action DidLevelPass;

        [SerializeField] private GameObject _ScoreUI;
        [SerializeField] private GameObject _SuccessUI;
        [SerializeField] private GameObject _LevelPassUI;
        [SerializeField] private GameObject _StartUI;

        [SerializeField] private TMP_Text _ScoreText;
        [SerializeField] private TMP_Text _LevelText;

        private GameConfig _config;

        private void Awake()
        {
            _config = GameConfig.Instance;

            InputController.DidTap += InputControllerOnDidTap;
            PlayerController.DidReachEnd += PlayerOnDidReachEnd;
            GameController.DidLevelLoad += GameManagerOnDidLevelLoad;
        }

        private void GameManagerOnDidLevelLoad()
        {
            _ScoreUI.SetActive(true);
            _StartUI.SetActive(true);

            _ScoreText.text = $"{GameConfig.GetScore()}";
            _LevelText.text = $"Level {GameConfig.GetLevel() + 1}";
        }

        private void InputControllerOnDidTap()
        {
            _ScoreUI.SetActive(false);
            _StartUI.SetActive(false);
        }

        private void PlayerOnDidReachEnd()
        {
            _ScoreUI.SetActive(true);
            _SuccessUI.SetActive(true);
            _LevelPassUI.SetActive(true);
        }

        private void LevelPass()
        {
            _config.IncreaseLevelIndex();

            _ScoreUI.SetActive(false);
            _SuccessUI.SetActive(false);
            _LevelPassUI.SetActive(false);
            _StartUI.SetActive(false);

            DidLevelPass?.Invoke();
        }

        public void ScoreAnimation()
        {
            var currentScore = _config.Score;
            var targetScore = currentScore + _config.SuccessScoreValue;

            var score = currentScore;

            DOTween.To(() => currentScore, x => score = x, targetScore, _config.ScoreAnimationDuration)
                .SetEase(Ease.OutSine)
                .OnUpdate(() => _ScoreText.text = $"{score}")
                .OnComplete(() =>
                {
                    _config.Score = score;
                    LevelPass();
                });
        }
    }
}
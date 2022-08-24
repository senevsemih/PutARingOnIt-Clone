using System;
using Scripts.PutARingOnIt.GameElements;
using TimerSystem;
using TMPro;
using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameConfig _Config;

        [SerializeField] private GameObject _ScoreUI;
        [SerializeField] private GameObject _SuccessUI;
        [SerializeField] private GameObject _LevelPassUI;
        [SerializeField] private GameObject _StartUI;

        [SerializeField] private TMP_Text _LevelText;

        private void Awake()
        {
            InputController.DidTap += InputControllerOnDidTap;
            Player.DidReachEnd += PlayerOnDidReachEnd;
        }

        private void Start()
        {
            _ScoreUI.SetActive(true);
            _StartUI.SetActive(true);
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

        public void LevelPass()
        {
            ScoreAnimation();

            _Config.IncreaseLevelIndex();

            _ScoreUI.SetActive(false);
            _ScoreUI.SetActive(false);
            _LevelPassUI.SetActive(false);
            _StartUI.SetActive(false);
        }

        private void ScoreAnimation()
        {
            var currentScore = _Config.Score;
            var targetScore = currentScore + _Config.SuccessScoreValue;

            int scoreUpdate;

            new Operation(
                    duration: _Config.ScoreAnimationDuration,
                    updateAction: tVal =>
                    {
                        scoreUpdate = (int)Mathf.Lerp(currentScore, targetScore, tVal);
                        _LevelText.text = scoreUpdate.ToString();
                    })
                .Start();
        }
    }
}
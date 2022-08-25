using System.Globalization;
using DG.Tweening;
using PutARingOnIt.GameElements.Controllers;
using PutARingOnIt.GameElements.Player;
using Scripts.PutARingOnIt.GameElements;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _ScoreUI;
        [SerializeField] private GameObject _SuccessUI;
        [SerializeField] private GameObject _LevelPassUI;
        [SerializeField] private GameObject _StartUI;
        
        [SerializeField] private TMP_Text _LevelText;

        private GameConfig _config;
        
        private void Awake()
        {
            _config = GameConfig.Instance;
            InputController.DidTap += InputControllerOnDidTap;
            PlayerController.DidReachEnd += PlayerOnDidReachEnd;
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

            _config.IncreaseLevelIndex();

            _ScoreUI.SetActive(false);
            _ScoreUI.SetActive(false);
            _LevelPassUI.SetActive(false);
            _StartUI.SetActive(false);
        }

        [Button]
        private void ScoreAnimation()
        {
            var currentScore = _config.Score;
            var targetScore = currentScore + _config.SuccessScoreValue;
            
            var scoreUpdate = 0f;

            DOTween.To(x => scoreUpdate = x, currentScore, targetScore, 1);
            _LevelText.text = scoreUpdate.ToString(CultureInfo.InvariantCulture);
            
            // new Operation(
            //         duration: _Config.ScoreAnimationDuration,
            //         updateAction: tVal =>
            //         {
            //             scoreUpdate = (int)Mathf.Lerp(currentScore, targetScore, tVal);
            //             _LevelText.text = scoreUpdate.ToString();
            //         })
            //     .Start();
        }
    }
}
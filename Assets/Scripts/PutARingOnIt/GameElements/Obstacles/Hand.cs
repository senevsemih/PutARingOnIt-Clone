using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Obstacles
{
    public class Hand : MonoBehaviour, IObstacle
    {
        [SerializeField, Required] private float _OffsetValue;
        [SerializeField, Required] private float _Duration;

        private void Start() => DoAnimation();

        public void DoAnimation() => transform.DOLocalMoveX(_OffsetValue, _Duration).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
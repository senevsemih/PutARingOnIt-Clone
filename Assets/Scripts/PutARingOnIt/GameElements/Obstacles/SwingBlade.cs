using DG.Tweening;
using UnityEngine;

namespace PutARingOnIt.GameElements.Obstacles
{
    public class SwingBlade : MonoBehaviour, IObstacle
    {
        [SerializeField] private Vector3 _OffsetValue;
        [SerializeField] private float _Duration;

        private void Start() => DoAnimation();

        public void DoAnimation() => transform.DOLocalRotate(_OffsetValue, _Duration).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
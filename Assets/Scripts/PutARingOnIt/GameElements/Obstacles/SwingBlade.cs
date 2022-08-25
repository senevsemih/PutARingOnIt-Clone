using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Obstacles
{
    public class SwingBlade : MonoBehaviour, IObstacle
    {
        [SerializeField, Required] private Vector3 _RotateValue;
        [SerializeField, Required] private float _Duration;

        private void Start() => DoAnimation();

        public void DoAnimation() => transform.DOLocalRotate(_RotateValue, _Duration).SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
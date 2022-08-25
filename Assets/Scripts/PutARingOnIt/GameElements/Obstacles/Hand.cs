using TimerSystem;
using TimerSystem.TimerHelper;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements.Obstacles
{
    public class Hand : MonoBehaviour, IObstacle
    {
        [SerializeField] private float _Duration;
        [SerializeField] private EaseCurve _Curve;
        [SerializeField] private Vector3 _Offset;

        private OperationTreeDescription _repeatingOperation;

        private void Start()
        {
            var currentPosition = transform.position;
            var targetPosition = currentPosition + _Offset;

            _repeatingOperation = new Operation(
                    duration: _Duration,
                    ease: _Curve,
                    updateAction: tVal => { transform.position = Vector3.Lerp(currentPosition, targetPosition, tVal); })
                .Start().Repeat();
        }

        private void OnDestroy() => _repeatingOperation.Cancel();
    }
}
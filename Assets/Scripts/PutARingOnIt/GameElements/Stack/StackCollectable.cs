using Scripts.PutARingOnIt.Other;
using Sirenix.OdinInspector;
using TimerSystem;
using TimerSystem.TimerHelper;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements.Stack
{
    public class StackCollectable : MonoBehaviour
    {
        [SerializeField, Required] private SelfRotate _Graphic;
        [Space] 
        [SerializeField, Required] private float _Duration;
        [SerializeField, Required] private EaseCurve _Curve;

        private Transform _graphicTransform;
        private const float Offset = -0.25f;
        private OperationTreeDescription _repeatingOperation;

        private void Awake() => _graphicTransform = _Graphic.transform;

        private void Start()
        {
            var graphicLocalPosition = _graphicTransform.localPosition;
            var targetPosition = graphicLocalPosition + new Vector3(0f, Offset, 0f);

            _repeatingOperation = new Operation(
                    duration: _Duration,
                    ease: _Curve,
                    updateAction: tVal =>
                    {
                        _graphicTransform.localPosition = Vector3.Lerp(graphicLocalPosition, targetPosition, tVal);
                    })
                .Start().Repeat();
        }

        public void OperationCancel()
        {
            _repeatingOperation.Cancel();

            Destroy(_Graphic);
            _graphicTransform.localPosition = Vector3.zero;
            _graphicTransform.eulerAngles = Vector3.zero;
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements.Stack
{
    public class StackItem : MonoBehaviour
    {
        private const float ScaleDuration = 0.1f;
        private const float ScaleSwellingRate = 0.3f;
        private const float UPWARD_MOVEMENT_FOLLOW_SPEED_MULTIPLIER = 4;

        private Transform _target;
        private float _speed;
        private float _offset;
        private StackFormation _stack;

        private Transform _transform;

        private void Awake() => _transform = transform;

        public void SetStackSettings(Transform target, float speed, float offset, StackFormation stack)
        {
            _target = target;
            _speed = speed;
            _offset = offset;
            _stack = stack;

            DoScaleAnimation(true);
        }

        private void Update() => StackMovement();
        
        public void DoScaleAnimation(bool isInitial, float delay = 0)
        {
            if (isInitial) _transform.localScale = Vector3.zero;

            var targetScale = Vector3.one + new Vector3(ScaleSwellingRate, 0f, ScaleSwellingRate);
            var seq = DOTween.Sequence();
            seq.SetDelay(delay * ScaleDuration);
            seq.Append(_transform.DOScale(targetScale, ScaleDuration));
            seq.Append(_transform.DOScale(Vector3.one, ScaleDuration));
        }

        private void StackMovement()
        {
            var position = _transform.position;
            // var rotation = _transform.eulerAngles;

            var targetTransform = _target.transform;
            var targetPosition = targetTransform.position;
            // var targetRotation = targetTransform.eulerAngles;

            var positionTargetZ = targetPosition.z;

            position.x = Mathf.Lerp(position.x, targetPosition.x, _speed * Time.deltaTime);
            position.y = targetPosition.y + _offset;
            position.z = Mathf.Lerp(position.z, positionTargetZ,
                _speed * UPWARD_MOVEMENT_FOLLOW_SPEED_MULTIPLIER * Time.deltaTime);

            // rotation.z = Mathf.Lerp(rotation.z, targetRotation.z, _speed * Time.deltaTime);

            _transform.position = position;
            // _transform.eulerAngles = rotation;
        }
    }
}
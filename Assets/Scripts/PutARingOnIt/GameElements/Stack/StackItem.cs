using DG.Tweening;
using Scripts.PutARingOnIt.Other;
using TimerSystem;
using TimerSystem.TimerHelper;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements.Stack
{
    public class StackItem : MonoBehaviour
    {
        private const float UPWARD_MOVEMENT_FOLLOW_SPEED_MULTIPLIER = 4;
        
        private Transform _target;
        private float _speed;
        private float _offset;
        private StackFormation _stack;

        private Transform _transform;

        public void SetStackSettings(Transform target, float speed, float offset, StackFormation stack)
        {
            _transform = transform;
            
            _target = target;
            _speed = speed;
            _offset = offset;
            _stack = stack;
        }
        
        private void Update() => StackMovement();

        public void ScaleAnim()
        {
            var currentScale = transform.localScale;
            var targetScale = currentScale + new Vector3(0.2f, 0f, 0.2f);

            new Operation(
                     duration: 1f,
                     ease: new EaseCurve(),
                     updateAction: tVal => { transform.localScale = Vector3.Lerp(currentScale, targetScale, tVal); })
                 .Start();
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
            position.z = Mathf.Lerp(position.z, positionTargetZ, _speed * UPWARD_MOVEMENT_FOLLOW_SPEED_MULTIPLIER * Time.deltaTime);
            
            // rotation.z = Mathf.Lerp(rotation.z, targetRotation.z, _speed * Time.deltaTime);
            
            _transform.position = position;
            // _transform.eulerAngles = rotation;
        }
    }
}
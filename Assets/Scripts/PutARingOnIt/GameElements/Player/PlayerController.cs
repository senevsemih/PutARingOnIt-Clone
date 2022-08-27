using System;
using DG.Tweening;
using Dreamteck.Splines;
using PutARingOnIt.GameElements.Controllers;
using PutARingOnIt.GameElements.Obstacles;
using PutARingOnIt.Other;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static event Action DidReachEnd;

        [SerializeField] private SplineFollower _SplineFollower;
        [SerializeField] private Transform _StackPivot;
        [Space] [SerializeField] private PlayerGraphic _Graphic;
        [SerializeField] private PhysicsListener _PhysicsListener;

        private GameConfig _config;
        private bool _isStackRotateActive;
        [ShowInInspector, ReadOnly] private Vector3 _deltaPos;

        private void Awake()
        {
            _config = GameConfig.Instance;

            InputController.DidTap += InputControllerOnDidTap;
            InputController.DidDrag += InputDragHandlerOnDidDrag;
            InputController.DidDragEnd += InputControllerOnDidDragEnd;
            _PhysicsListener.TriggerEnter += PhysicsListenerOnTriggerEnter;
        }

        private void Update()
        {
            if (_isStackRotateActive) Rotate(_deltaPos.x);
        }

        public void Init(SplineComputer spline)
        {
            _Graphic.StateChangeTo(HandAnimState.Idle);

            _isStackRotateActive = true;
            _SplineFollower.spline = spline;
            _SplineFollower.SetPercent(0);
            _SplineFollower.onEndReached += SplineFollowerOnEndReached;
        }

        private void InputControllerOnDidTap() => _SplineFollower.follow = true;

        private void InputDragHandlerOnDidDrag(Vector3 deltaPos)
        {
            _deltaPos = deltaPos;
            var motionOffset = _SplineFollower.motion.offset;
            var newOffsetX = motionOffset.x + deltaPos.x * _config.DragSpeed;
            newOffsetX = Mathf.Clamp(newOffsetX, -_config.RoadLimit, _config.RoadLimit);
            var newOffset = new Vector2(newOffsetX, 0f);

            _SplineFollower.motion.offset = newOffset;
        }

        private void InputControllerOnDidDragEnd() => _deltaPos = Vector3.zero;

        private void PhysicsListenerOnTriggerEnter(Collider other)
        {
            var obstacle = other.gameObject.GetComponent<IObstacle>();
            if (obstacle != null) Stagger();
        }

        private void SplineFollowerOnEndReached(double value)
        {
            _isStackRotateActive = false;
            _SplineFollower.onEndReached -= SplineFollowerOnEndReached;
            _SplineFollower.follow = false;

            var currentPosition = transform.position;
            currentPosition.x = 0;
            var targetPosition = currentPosition + _config.LevelEndOffset;

            var seq = DOTween.Sequence();
            seq.Append(transform.DOMove(targetPosition, _config.LevelEndMoveOffsetDuration))
                .OnComplete(() =>
                {
                    _Graphic.StateChangeTo(HandAnimState.Shake);
                    DidReachEnd?.Invoke();
                });
        }

        private void Rotate(float value)
        {
            var currentAngles = _StackPivot.rotation;

            var targetAngles = value switch
            {
                0 => Quaternion.Euler(Vector3.zero),
                < 0 => Quaternion.Euler(new Vector3(0f, 0f, -_config.StackRotateAngleLimit)),
                > 0 => Quaternion.Euler(new Vector3(0f, 0f, _config.StackRotateAngleLimit)),
                _ => default
            };

            _StackPivot.rotation = Quaternion.Lerp(currentAngles, targetAngles,
                _config.StackPivotRotationSpeed * Time.deltaTime);
        }

        private void Stagger()
        {
            _SplineFollower.follow = false;

            var currentPercent = (float)_SplineFollower.result.percent;
            var targetPercent = currentPercent - _config.StaggerOffset;
            var percent = currentPercent;

            DOTween.To(() => currentPercent, x => percent = x, targetPercent, _config.StaggerDuration)
                .SetEase(Ease.OutSine)
                .OnUpdate(() => _SplineFollower.SetPercent(percent))
                .OnComplete(() => _SplineFollower.follow = true);
        }
    }
}
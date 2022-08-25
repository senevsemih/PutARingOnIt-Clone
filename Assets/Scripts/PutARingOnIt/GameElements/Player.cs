using System;
using Scripts.PutARingOnIt.Other;
using Dreamteck.Splines;
using PutARingOnIt.GameElements.Obstacles;
using Scripts.PutARingOnIt.GameElements.Stack;
using Sirenix.OdinInspector;
using TimerSystem;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class Player : MonoBehaviour
    {
        public static event Action DidReachEnd;

        // [SerializeField] private GameConfig _Config;
        [SerializeField, Required] private StackConfig _StackConfig = new()
        {
            StackOffset = 1,
            StackMaxSpeed = 20,
            StackSpeedDecreaseRate = 1
        };
        
        [Space] [SerializeField] private SplineFollower _SplineFollower;
        [SerializeField] private PlayerGraphic _Graphic;
        [SerializeField] private PhysicsListener _PhysicsListener;
        [SerializeField] private Transform _StackStartPosition;

        private StackFormation _stackFormation;
        private GameConfig _config;
        
        private void Awake()
        {
            _config = GameConfig.Instance;
            InputController.DidTap += InputControllerOnDidTap;
            InputController.DidDrag += InputDragHandlerOnDidDrag;
            _PhysicsListener.TriggerEnter += PhysicsListenerOnTriggerEnter;
        }

        public void Init(SplineComputer spline)
        {
            _SplineFollower.spline = spline;
            _SplineFollower.onEndReached += SplineFollowerOnEndReached;

            _stackFormation = new StackFormation(_StackConfig, _StackStartPosition);
        }

        private void InputControllerOnDidTap() => _SplineFollower.follow = true;

        private void InputDragHandlerOnDidDrag(Vector3 deltaPos)
        {
            var motionOffset = _SplineFollower.motion.offset;
            var newOffsetX = motionOffset.x + deltaPos.x * _config.DragSpeed;
            newOffsetX = Mathf.Clamp(newOffsetX, -_config.RoadLimit, _config.RoadLimit);
            var newOffset = new Vector2(newOffsetX, 0f);
            
            _SplineFollower.motion.offset = newOffset;
        }

        private void PhysicsListenerOnTriggerEnter(Collider other)
        {
            var obstacle = other.gameObject.GetComponent<IObstacle>();
            if (obstacle != null)
            {
                Stagger();
            }

            var collectable = other.gameObject.GetComponent<StackCollectable>();
            if (collectable) _stackFormation.Increase(collectable);
        }

        private void Stagger()
        {
            _SplineFollower.follow = false;
            
            var currentPercent = _SplineFollower.result.percent;
            var targetPercent = currentPercent - _config.StaggerOffset;
            
            new Operation(
                    duration: _config.StaggerDuration,
                    ease: _config.StaggerCurve,
                    updateAction: tVal =>
                    {
                        var percent = Mathf.Lerp((float)currentPercent, (float)targetPercent, tVal);
                        _SplineFollower.SetPercent(percent);
                    })
                .Add(
                    action: () => { _SplineFollower.follow = true; })
                .Start();
        }

        private void SplineFollowerOnEndReached(double obj)
        {
            _SplineFollower.onEndReached -= SplineFollowerOnEndReached;
            _SplineFollower.follow = false;

            // biraz yukarı yükselecek.

            _Graphic.StateChangeTo(HandAnimState.Shake);
            DidReachEnd?.Invoke();
        }
    }
}
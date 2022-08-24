using System;
using Scripts.PutARingOnIt.Other;
using Dreamteck.Splines;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class Player : MonoBehaviour
    {
        public static event Action DidReachEnd;
        
        [SerializeField] private GameConfig _Config;
        [Space]
        [SerializeField] private SplineFollower _SplineFollower;
        [SerializeField] private PlayerGraphic _Graphic;
        
        private void Awake()
        {
            InputController.DidTap += InputControllerOnDidTap;
            InputController.DidDrag += InputDragHandlerOnDidDrag;
        }

        public void Init(SplineComputer spline)
        {
            _SplineFollower.spline = spline;
            _SplineFollower.onEndReached += SplineFollowerOnEndReached;
        }

        private void SplineFollowerOnEndReached(double obj)
        {
            _SplineFollower.onEndReached -= SplineFollowerOnEndReached;
            _SplineFollower.follow = false;
            
            // biraz yukarı yükselecek.
            
            _Graphic.StateChangeTo(HandAnimState.Shake);
            DidReachEnd?.Invoke();
        }

        private void InputControllerOnDidTap()
        {
            _SplineFollower.follow = true;
        }

        private void InputDragHandlerOnDidDrag(Vector3 deltaPos)
        {
            var motionOffset = _SplineFollower.motion.offset;
            var newOffsetX = motionOffset.x + deltaPos.x * _Config.DragSpeed;
            newOffsetX = Mathf.Clamp(newOffsetX, -_Config.RoadLimit, _Config.RoadLimit);
            var newOffset = new Vector2(newOffsetX, 0f);
            
            _SplineFollower.motion.offset = newOffset;
        }
    }
}

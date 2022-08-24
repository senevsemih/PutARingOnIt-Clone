using Scripts.PutARingOnIt.Other;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SplineFollower _SplineFollower;
        [SerializeField] private PlayerGraphic _Graphic;
        [SerializeField] private GameConfig _Config;

        private void Awake() => InputDragHandler.DidDrag += InputDragHandlerOnDidDrag;

        private void InputDragHandlerOnDidDrag(Vector3 deltaPos)
        {
            var motionOffset = _SplineFollower.motion.offset;
            var newOffsetX = motionOffset.x + deltaPos.x * _Config.DragSpeed;
            newOffsetX = Mathf.Clamp(newOffsetX, -_Config.RoadLimit, _Config.RoadLimit);
            var newOffset = new Vector2(newOffsetX, 0f);
            
            _SplineFollower.motion.offset = newOffset;
        }
        
        [Button]
        public void AnimTest(HandAnimState state)
        {
            _Graphic.StateChangeTo(state);
        }
    }
}

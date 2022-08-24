using System;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class InputController : MonoBehaviour
    {
        public static event Action DidTap; 
        public static event Action<Vector3> DidDrag;
        
        private Vector3? _lastPosition;
        private bool _isDragActive;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isDragActive)
            {
                _isDragActive = true;
                DidTap?.Invoke();
            }
            
            if (!_isDragActive) return;
            
            var isInput = Input.GetMouseButton(0);
            var mousePosition = Input.mousePosition;
            mousePosition.y = 0;
            
            if (isInput && _lastPosition.HasValue)
            {
                var v = mousePosition - _lastPosition.Value;
                var direction = v.normalized;
                DidDrag?.Invoke(direction);
            }
            else
            {
                _lastPosition = null;
            }

            if (isInput) _lastPosition = mousePosition;
        }
    }
}
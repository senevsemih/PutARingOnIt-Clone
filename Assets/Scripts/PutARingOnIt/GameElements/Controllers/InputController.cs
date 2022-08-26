using System;
using PutARingOnIt.GameElements.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PutARingOnIt.GameElements.Controllers
{
    public class InputController : MonoBehaviour
    {
        public static event Action DidTap;
        public static event Action<Vector3> DidDrag;

#if UNITY_EDITOR
        private static bool IsInput => Input.GetMouseButton(0);
        private static Vector3 InputPos => Input.mousePosition;
#else
        private static bool IsInput => Input.touchCount > 0;
        private static Vector3 InputPos => Input.touches[0].position;
#endif

        private Vector3? _lastPosition;
        [ShowInInspector, ReadOnly] private bool _isDragActive;
        [ShowInInspector, ReadOnly] private bool _isInputActive;

        private void Awake()
        {
            GameManager.DidLevelLoad += GameManagerOnDidLevelLoad;
            PlayerController.DidReachEnd += PlayerControllerOnDidReachEnd;
        }

        private void GameManagerOnDidLevelLoad() => _isInputActive = true;

        private void PlayerControllerOnDidReachEnd()
        {
            _isInputActive = false;
            _isDragActive = false;
        }

        private void Update()
        {
            if (EventSystem.current && EventSystem.current.currentSelectedGameObject) return;
            
            if (!_isInputActive) return;
            if (Input.GetMouseButtonDown(0) && !_isDragActive)
            {
                _isDragActive = true;
                DidTap?.Invoke();
            }

            if (!_isDragActive) return;

            var isInput = IsInput;
            var inputPos = InputPos;
            inputPos.y = 0;

            if (isInput && _lastPosition.HasValue)
            {
                var v = inputPos - _lastPosition.Value;
                var direction = v.normalized;
                DidDrag?.Invoke(direction);
            }
            else
            {
                _lastPosition = null;
            }

            if (isInput) _lastPosition = inputPos;
        }
    }
}
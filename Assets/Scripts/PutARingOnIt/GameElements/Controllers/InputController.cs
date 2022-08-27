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
        public static event Action DidDragEnd;

        private Vector3? _lastPos;
        [ShowInInspector, ReadOnly] private float _screenWidth;
        [ShowInInspector, ReadOnly] private float _screenHeight;

        [ShowInInspector, ReadOnly] private bool _isDragActive;
        [ShowInInspector, ReadOnly] private bool _isInputActive;


#if UNITY_EDITOR
        private static bool IsInput => Input.GetMouseButton(0);
        private static Vector3 InputPos => Input.mousePosition;
#else
        private static bool IsInput => Input.touchCount > 0;
        private static Vector3 InputPos => Input.touches[0].position;
#endif

        private void Awake()
        {
            GameController.DidLevelLoad += GameManagerOnDidLevelLoad;
            PlayerController.DidReachEnd += PlayerControllerOnDidReachEnd;
        }

        private void Start()
        {
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
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
            var isInput = IsInput;

            switch (isInput)
            {
                case true:
                    var inputPos = InputPos;
                    if (_lastPos.HasValue && _isDragActive)
                    {
                        var delta = inputPos - _lastPos.Value;
                        var normalizedDelta = delta / _screenWidth;

                        DidDrag?.Invoke(normalizedDelta);
                    }
                    else if (!_isDragActive)
                    {
                        _isDragActive = true;
                        DidTap?.Invoke();
                    }

                    _lastPos = inputPos;
                    break;
                case false when _lastPos.HasValue:
                    _lastPos = null;
                    DidDragEnd?.Invoke();
                    break;
            }
        }
    }
}
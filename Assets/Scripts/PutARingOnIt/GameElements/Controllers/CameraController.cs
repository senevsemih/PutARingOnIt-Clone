using DG.Tweening;
using UnityEngine;

namespace PutARingOnIt.GameElements.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _Target;
        [SerializeField] private Vector3 _Offset;
        [SerializeField] private float _FollowSpeed;

        private const float Duration = 0.2f;
        private const float ShakePower = 0.2f;

        private static Camera _camera;
        private Vector3 TargetPosition => _Target.transform.position + _Offset;

        private void Awake() => _camera = Camera.main;

        public static void Shake() => _camera.DOShakePosition(Duration, ShakePower);

        private void LateUpdate() => TargetFollow();

        private void TargetFollow()
        {
            if (!_Target) return;

            var position = transform.position;
            var targetPos = TargetPosition;

            targetPos.x = position.x;
            position = Vector3.Lerp(position, targetPos, _FollowSpeed * Time.deltaTime);

            transform.position = position;
        }
    }
}
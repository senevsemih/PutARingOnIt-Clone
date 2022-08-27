using System;
using DG.Tweening;
using PutARingOnIt.Other;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Stack
{
    public class StackItem : MonoBehaviour
    {
        private const float ScaleDuration = 0.1f;
        private const float ScaleSwellingRate = 0.3f;
        private const float ScaleUpRate = 0.1f;

        [ShowInInspector, ReadOnly] private Transform _target;
        private float _speed;

        private Sequence _scaleAnim;
        private Vector3 _scale;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private StackCollectable _collectable;
        private GameConfig _config;

        private void Awake()
        {
            _config = GameConfig.Instance;
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_target) StackMovement();
        }

        public void SetStackSettings(Transform target, float speed, StackCollectable collectable)
        {
            _rigidbody.isKinematic = true;
            _scale = transform.localScale;

            _target = target;
            _speed = speed;
            _collectable = collectable;

            DoScaleAnimation(true);
        }

        public void UpdateTarget(Transform target) => _target = target;
        public void UpdateSpeed(float speed) => _speed = speed;

        public void DoScaleAnimation(bool isInitial, float delay = 0, bool isIncrease = false)
        {
            if (isInitial) _transform.localScale = Vector3.zero;
            if (isIncrease)
            {
                _scaleAnim.Kill();
                _scale += new Vector3(ScaleUpRate, ScaleUpRate, ScaleUpRate);
            }

            var targetScale = _scale + new Vector3(ScaleSwellingRate, 0f, ScaleSwellingRate);
            _scaleAnim = DOTween.Sequence();
            _scaleAnim.SetDelay(delay * ScaleDuration);
            _scaleAnim.Append(_transform.DOScale(targetScale, ScaleDuration));
            _scaleAnim.Append(_transform.DOScale(_scale, ScaleDuration));
        }

        public void Throw()
        {
            _target = null;
            _collectable.DoSwing();

            var randomX = _config.StackThrowRangeX.GetRandomValueAsRange();
            var randomY = _config.StackThrowRangeY.GetRandomValueAsRange();
            var randomZ = _config.StackThrowRangeZ.GetRandomValueAsRange();

            var direction = new Vector3(randomX, randomY, randomZ);

            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(direction * _config.StackThrowForce);
        }

        public void MoveForMerge(Transform target, Action stackRefresh)
        {
            _target = null;
            _scaleAnim.Kill();
            _transform.DOMove(target.position, 0.025f).OnComplete(() => stackRefresh?.Invoke());
        }

        public StackCollectableType GetCollectableType() => _collectable.GetCollectableType();
        public Transform TopTransform() => _collectable.TopTransform();

        private void StackMovement()
        {
            var rotation = _transform.rotation;

            var targetTransform = _target.transform;
            var targetPosition = targetTransform.position;
            var targetRotation = targetTransform.rotation;

            rotation = Quaternion.Lerp(rotation, targetRotation, _speed * Time.deltaTime);

            _transform.position = targetPosition;
            _transform.rotation = rotation;
        }
    }
}
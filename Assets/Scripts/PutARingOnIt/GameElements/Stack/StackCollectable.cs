using DG.Tweening;
using PutARingOnIt.Other;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Stack
{
    public class StackCollectable : MonoBehaviour
    {
        [SerializeField, Required] private SelfRotate _Rotate;
        [Space] 
        [SerializeField, Required] private float _SwingDuration;
        [SerializeField, Required] private float _SwingOffset;

        private Transform _graphicTransform;
        private Sequence _swingSeq;

        private void Awake() => _graphicTransform = _Rotate.transform;
        private void Start() => DoSwing();

        public void DoSwing()
        {
            _Rotate.SetActive(true);

            _swingSeq = DOTween.Sequence();
            _swingSeq.Append(_graphicTransform.DOLocalMoveY(_SwingOffset, _SwingDuration).SetEase(Ease.InOutSine));
            _swingSeq.SetLoops(-1, LoopType.Yoyo);
        }

        public void ResetTransform()
        {
            _swingSeq.Kill();

            _Rotate.SetActive(false);
            _graphicTransform.localPosition = Vector3.zero;
            _graphicTransform.eulerAngles = Vector3.zero;
        }
    }
}
using DG.Tweening;
using Scripts.PutARingOnIt.Other;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements.Stack
{
    public class StackCollectable : MonoBehaviour
    {
        [SerializeField, Required] private SelfRotate _Graphic;
        [Space] 
        [SerializeField, Required] private float _SwingDuration;
        [SerializeField, Required] private float _SwingOffset;

        private Transform _graphicTransform;
        private Sequence _swingSeq;

        private void Awake() => _graphicTransform = _Graphic.transform;
        private void Start() => DoSwing();

        private void DoSwing()
        {
            _swingSeq = DOTween.Sequence();
            _swingSeq.Append(_graphicTransform.DOLocalMoveY(_SwingOffset, _SwingDuration).SetEase(Ease.InOutSine));
            _swingSeq.SetLoops(-1, LoopType.Yoyo);
        }

        public void ResetTransform()
        {
            _swingSeq.Kill();

            Destroy(_Graphic);
            _graphicTransform.localPosition = Vector3.zero;
            _graphicTransform.eulerAngles = Vector3.zero;
        }
    }
}
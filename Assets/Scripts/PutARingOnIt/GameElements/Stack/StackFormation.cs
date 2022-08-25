using System.Collections.Generic;
using UnityEngine;

namespace PutARingOnIt.GameElements.Stack
{
    public class StackFormation
    {
        private readonly float _stackOffset;
        private readonly float _stackMaxSpeed;
        private readonly float _stackSpeedDecreaseRate;
        private readonly Transform _stackStartPosition;
        private readonly List<StackItem> _items = new();

        private int StackCount => _items.Count;

        public StackFormation(StackConfig config, Transform stackStartPosition)
        {
            _stackOffset = config.StackOffset;
            _stackMaxSpeed = config.StackMaxSpeed;
            _stackSpeedDecreaseRate = config.StackSpeedDecreaseRate;
            _stackStartPosition = stackStartPosition;
        }

        public void Increase(StackCollectable newCollectable)
        {
            newCollectable.ResetTransform();

            var target = GetTargetTransformForItemIndex(StackCount);
            var targetPos = target.position;
            var newItemPos = targetPos + new Vector3(0f, _stackOffset, 0f);

            var newItem = newCollectable.GetComponent<StackItem>();
            if (!newItem) newItem = newCollectable.gameObject.AddComponent<StackItem>();

            newItem.SetStackSettings(target, GetSpeedByItemIndex(StackCount), _stackOffset, this, newCollectable);
            newItem.name = $"Collectible - {StackCount}";
            newItem.transform.position = newItemPos;
            _items.Add(newItem);

            IncreaseEffect();
        }

        public void Scatter()
        {
            if (StackCount <= 0) return;

            var stackCount = StackCount;
            var half = stackCount / 2;
            var limit = stackCount - (half + 1);

            for (var i = stackCount - 1; i >= limit; i--)
            {
                _items[i].Throw();
                _items.Remove(_items[i]);
            }
        }

        public void Merge()
        {
        }

        private void IncreaseEffect()
        {
            for (var i = StackCount - 1; i >= 0; i--)
            {
                _items[i].DoScaleAnimation(false, StackCount - 1 - i);
            }
        }

        private float GetSpeedByItemIndex(int index)
        {
            var speed = _stackMaxSpeed - (index - 1) * _stackSpeedDecreaseRate;
            if (speed <= 0) speed = 1;

            return speed;
        }

        private Transform GetTargetTransformForItemIndex(int i) =>
            i == 0 ? _stackStartPosition : _items[i - 1].transform;
    }
}
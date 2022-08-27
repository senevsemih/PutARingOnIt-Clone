using System.Collections.Generic;
using UnityEngine;

namespace PutARingOnIt.GameElements.Stack
{
    public class StackFormation
    {
        private readonly float _stackMaxSpeed;
        private readonly float _stackSpeedDecreaseRate;
        private readonly Transform _stackStartPosition;
        private readonly List<StackItem> _items = new();

        private int StackCount => _items.Count;
        private int _mergeIndex;

        public StackFormation(StackConfig config, Transform stackStartPosition)
        {
            _stackMaxSpeed = config.StackMaxSpeed;
            _stackSpeedDecreaseRate = config.StackSpeedDecreaseRate;
            _stackStartPosition = stackStartPosition;
        }

        public void Increase(StackCollectable newCollectable)
        {
            newCollectable.ResetTransform();

            var target = GetTargetTransformForItemIndex(StackCount);
            var targetPos = target.position;

            var newItem = newCollectable.GetComponent<StackItem>();
            if (!newItem) newItem = newCollectable.gameObject.AddComponent<StackItem>();

            newItem.SetStackSettings(target, GetSpeedByItemIndex(StackCount), newCollectable);
            newItem.name = $"Collectible - {StackCount}";
            newItem.transform.position = targetPos;
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

        public void Merge(int index, bool isDoor = false)
        {
            while (true)
            {
                if (isDoor) _mergeIndex = index;

                if (StackCount <= 1) return;

                var item = _items[_mergeIndex];
                if (_mergeIndex + 1 > StackCount - 1) return;
                var aboveItem = _items[_mergeIndex + 1];

                if (item.GetCollectableType() == aboveItem.GetCollectableType())
                {
                    item.DoScaleAnimation(false, 0, true);
                    aboveItem.MoveForMerge(item.transform, StackRefresh);
                }
                else
                {
                    _mergeIndex++;
                    index = _mergeIndex;
                    isDoor = false;
                    continue;
                }

                for (var i = 0; i < StackCount; i++)
                {
                    _items[i].UpdateSpeed(GetSpeedByItemIndex(i));
                }

                break;
            }
        }

        private void StackRefresh()
        {
            var mergeItem = _items[_mergeIndex + 1];

            if (_mergeIndex + 2 < StackCount)
            {
                var item = _items[_mergeIndex];
                var aboveMergeItem = _items[_mergeIndex + 2];

                aboveMergeItem.UpdateTarget(item.TopTransform());
                _items.Remove(mergeItem);
            }
            else
            {
                _items.Remove(mergeItem);
            }

            mergeItem.gameObject.SetActive(false);
            Merge(_mergeIndex);
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
            i == 0 ? _stackStartPosition : _items[i - 1].TopTransform();
    }
}
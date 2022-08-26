using NaughtyAttributes;
using UnityEngine;

namespace PutARingOnIt.Other
{
    public class SelfRotate : MonoBehaviour
    {
        [SerializeField, Required] private Vector3 _Rotation = Vector3.up;

        private bool _isActive = true;
        public void SetActive(bool value) => _isActive = value;

        private void Update()
        {
            if (!_isActive) return;
            transform.Rotate(_Rotation * Time.deltaTime);
        }
    }
}
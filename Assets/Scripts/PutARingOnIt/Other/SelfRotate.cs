using NaughtyAttributes;
using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    public class SelfRotate : MonoBehaviour
    {
        [SerializeField, Required] private Vector3 _Rotation = Vector3.up;
        private void Update() => transform.Rotate(_Rotation * Time.deltaTime);
    }
}
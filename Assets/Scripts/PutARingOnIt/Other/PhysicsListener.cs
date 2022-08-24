using System;
using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    public class PhysicsListener : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        private void OnTriggerEnter(Collider other) => TriggerEnter?.Invoke(other);
    }
}
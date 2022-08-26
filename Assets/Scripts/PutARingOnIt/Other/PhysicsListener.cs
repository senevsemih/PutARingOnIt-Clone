using System;
using UnityEngine;

namespace PutARingOnIt.Other
{
    public class PhysicsListener : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        private void OnTriggerEnter(Collider other) => TriggerEnter?.Invoke(other);
    }
}
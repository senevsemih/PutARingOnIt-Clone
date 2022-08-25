using System;
using UnityEngine;

namespace PutARingOnIt.GameElements.Player
{
    public class PlayerGraphic : MonoBehaviour
    {
        [SerializeField] private Animator _Animator;

        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Shake = Animator.StringToHash("Shake");

        public void StateChangeTo(HandAnimState state)
        {
            switch (state)
            {
                case HandAnimState.Idle:
                    _Animator.SetTrigger(Idle);
                    break;
                case HandAnimState.Shake:
                    _Animator.SetTrigger(Shake);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }

    public enum HandAnimState
    {
        Idle,
        Shake,
    }
}
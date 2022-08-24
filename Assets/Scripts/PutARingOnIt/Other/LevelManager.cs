using Dreamteck.Splines;
using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private SplineComputer _SplineComputer;
        public SplineComputer SplineComputer => _SplineComputer;
    }
}
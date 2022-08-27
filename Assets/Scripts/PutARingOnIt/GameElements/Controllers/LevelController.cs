using Dreamteck.Splines;
using UnityEngine;

namespace PutARingOnIt.GameElements.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private SplineComputer _SplineComputer;
        public SplineComputer SplineComputer => _SplineComputer;
    }
}
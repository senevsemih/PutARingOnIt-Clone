using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public float DragSpeed;
        public float RoadLimit;
    }
}
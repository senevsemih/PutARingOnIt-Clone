using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    // ReSharper disable InconsistentNaming
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public int LevelIndex;
        public int Score;
        public int SuccessScoreValue;
        public float ScoreAnimationDuration;
        
        public float DragSpeed;
        public float RoadLimit;
        
        public void IncreaseLevelIndex() => LevelIndex++;
    }
}
using TimerSystem.TimerHelper;
using UnityEngine;

namespace Scripts.PutARingOnIt.Other
{
    // ReSharper disable InconsistentNaming
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public static GameConfig Instance;
        
        public int LevelIndex;
        public int Score;
        public int SuccessScoreValue;
        public float ScoreAnimationDuration;
        
        public float DragSpeed;
        public float RoadLimit;
        
        public float StaggerDuration;
        public EaseCurve StaggerCurve;
        public float StaggerOffset;
        
        public void IncreaseLevelIndex() => LevelIndex++;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnLoad()
        {
            Instance = Resources.Load<GameConfig>("Config");
        }
    }
}
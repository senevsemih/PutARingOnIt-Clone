using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace PutARingOnIt.Other
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public static GameConfig Instance;

        public int LevelIndex;
        public int Score;
        public int SuccessScoreValue;
        public float ScoreAnimationDuration;

        [FoldoutGroup("Game")] public float DragSpeed;
        [FoldoutGroup("Game")] public float RoadLimit;

        [FoldoutGroup("Player")] public float StackPivotRotationSpeed;
        [FoldoutGroup("Player")] public float StackRotateAngleLimit;
        [Space] 
        [FoldoutGroup("Player")] public float StaggerDuration;
        [FoldoutGroup("Player")] public float StaggerOffset;
        [Space] 
        [FoldoutGroup("Player")] public Vector3 LevelEndOffset;
        [FoldoutGroup("Player")] public float LevelEndMoveOffsetDuration;

        [FoldoutGroup("Stack")] public Vector2 StackThrowRangeX;
        [FoldoutGroup("Stack")] public Vector2 StackThrowRangeY;
        [FoldoutGroup("Stack")] public Vector2 StackThrowRangeZ;
        [FoldoutGroup("Stack")] public float StackThrowForce;

        public void IncreaseLevelIndex()
        {
            LevelIndex++;
            
            PlayerPrefs.SetInt("SavedLevelIndex", LevelIndex);
            PlayerPrefs.SetInt("SavedScore", Score);
            PlayerPrefs.Save();
        }

        public static int GetLevel()
        {
            var index = PlayerPrefs.HasKey("SavedLevelIndex") ? PlayerPrefs.GetInt("SavedLevelIndex") : 0;
            return index;
        }
        
        public static int GetScore()
        {
            var score = PlayerPrefs.HasKey("SavedScore") ? PlayerPrefs.GetInt("SavedScore") : 0;
            return score;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnLoad() => Instance = Resources.Load<GameConfig>("Config");
    }
}
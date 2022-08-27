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

        public void IncreaseLevelIndex() => LevelIndex++;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnLoad() => Instance = Resources.Load<GameConfig>("Config");
    }
}
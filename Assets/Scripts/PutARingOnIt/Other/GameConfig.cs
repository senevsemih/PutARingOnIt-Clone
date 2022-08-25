using DG.Tweening;
using TimerSystem.TimerHelper;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace Scripts.PutARingOnIt.Other
{
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

        public Vector2 StackThrowRangeX;
        public Vector2 StackThrowRangeY;
        public Vector2 StackThrowRangeZ;
        public float StackThrowJumpPower;
        public float StackThrowDuration;
        public Ease JumpEase;

        public void IncreaseLevelIndex() => LevelIndex++;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnLoad()
        {
            Instance = Resources.Load<GameConfig>("Config");
        }
    }
}
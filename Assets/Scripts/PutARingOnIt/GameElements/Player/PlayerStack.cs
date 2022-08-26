using PutARingOnIt.GameElements.Obstacles;
using PutARingOnIt.GameElements.Stack;
using Scripts.PutARingOnIt.Other;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Player
{
    public class PlayerStack : MonoBehaviour
    {
        [SerializeField, Required] private StackConfig _StackConfig = new()
        {
            StackMaxSpeed = 20,
            StackSpeedDecreaseRate = 1
        };

        [SerializeField, Required] private Transform _StackStartPosition;
        [SerializeField, Required] private PhysicsListener _PhysicsListener;

        private StackFormation _stackFormation;

        private void Awake() => _PhysicsListener.TriggerEnter += PhysicsListenerOnTriggerEnter;
        private void Start() => _stackFormation = new StackFormation(_StackConfig, _StackStartPosition);

        private void PhysicsListenerOnTriggerEnter(Collider other)
        {
            var obstacle = other.gameObject.GetComponent<IObstacle>();
            if (obstacle != null) _stackFormation.Scatter();

            var collectable = other.gameObject.GetComponent<StackCollectable>();
            if (collectable) _stackFormation.Increase(collectable);

            var door = other.gameObject.GetComponent<Door>();
            if (door) _stackFormation.Merge(0, true);
        }

        [Button]
        private void Test()
        {
            _stackFormation.Merge(0, true);
        }
    }
}
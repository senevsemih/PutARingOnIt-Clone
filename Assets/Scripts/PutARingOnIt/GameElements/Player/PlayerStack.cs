using PutARingOnIt.GameElements.Obstacles;
using Scripts.PutARingOnIt.GameElements.Stack;
using Scripts.PutARingOnIt.Other;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PutARingOnIt.GameElements.Player
{
    public class PlayerStack : MonoBehaviour
    {
        [SerializeField, Required] private StackConfig _StackConfig = new()
        {
            StackOffset = 1,
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
        }
    }
}
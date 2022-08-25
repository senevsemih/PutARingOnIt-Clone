using System;

namespace Scripts.PutARingOnIt.GameElements.Stack
{
    [Serializable]
    public class StackConfig
    {
        public float StackOffset = 1;
        public float StackMaxSpeed = 20;
        public float StackSpeedDecreaseRate = 1;
    }
}
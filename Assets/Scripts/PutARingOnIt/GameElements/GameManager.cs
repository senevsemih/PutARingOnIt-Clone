using UnityEngine;

namespace Scripts.PutARingOnIt.GameElements
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private InputDragHandler _Input;
        private void Start() => _Input.SetActive(true);
    }
}
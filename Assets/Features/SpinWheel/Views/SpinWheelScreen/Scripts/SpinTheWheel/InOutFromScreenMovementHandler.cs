using UnityEngine;

namespace SpinTheWheel
{
    public class InOutFromScreenMovementHandler : MonoBehaviour
    {
        [SerializeField] UIScreenAnimaion SpinnerInOut;
        private void OnEnable()
        {
            SpinTheWheel.SpinWheelStopedAction += AnimateOut;
        }
        private void OnDisable()
        {
            SpinTheWheel.SpinWheelStopedAction -= AnimateOut;
        }
        private void Start()
        {
            SpinnerInOut.UiElement = GetComponent<RectTransform>();
        }
        private void AnimateIn(int index)
        {
            SpinnerInOut.AnimateIn();
        }
        private void AnimateOut(int index)
        {
            SpinnerInOut.AnimateOut();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerMovementsHandler : MonoBehaviour
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

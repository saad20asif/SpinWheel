using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetRewardText : MonoBehaviour
{
    [SerializeField] IntVariable coinsSo;
    [SerializeField] IntVariable multiplierSo;
    private void OnEnable()
    {
        SpinTheWheel.SpinWheelStopedAction += SetRewText;
    }
    private void OnDisable()
    {
        SpinTheWheel.SpinWheelStopedAction -= SetRewText;
    }
    private void SetRewText(int stopIndex)
    {
        if (GetComponent<TextMeshProUGUI>() != null)
        {
            GetComponent<TextMeshProUGUI>().text = $"<sprite=0> {multiplierSo.value*coinsSo.value}";
        }
    }
}

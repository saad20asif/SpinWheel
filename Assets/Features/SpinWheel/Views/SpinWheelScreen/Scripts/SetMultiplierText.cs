using TMPro;
using UnityEngine;

public class SetMultiplierText : MonoBehaviour
{
    [SerializeField] IntVariable multiplierSo;
    private void OnEnable()
    {
        SpinTheWheel.SpinWheelStopedAction += SetMultText;
    }
    private void OnDisable()
    {
        SpinTheWheel.SpinWheelStopedAction -= SetMultText;
    }
    private void SetMultText(int stopIndex)
    {
        if(GetComponent<TextMeshProUGUI>()!=null)
        {
            GetComponent<TextMeshProUGUI>().text = $"x {multiplierSo.value}";
        }
    }
}

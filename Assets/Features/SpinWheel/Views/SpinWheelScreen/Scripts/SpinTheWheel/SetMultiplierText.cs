using TMPro;
using UnityEngine;
using DG.Tweening;

namespace SpinTheWheel
{
    public class SetMultiplierText : MonoBehaviour
    {
        [SerializeField] IntVariable multiplierSo;
        [SerializeField] DOTweenAnimation myTween;
        private void OnEnable()
        {
            SpinTheWheel.SpinWheelStopedAction += SetMultText;
        }
        private void OnDisable()
        {
            SpinTheWheel.SpinWheelStopedAction -= SetMultText;
        }
        private void PlayTween()
        {
            if (myTween != null)
                myTween.DOPlay();
        }
        private void SetMultText(int stopIndex)
        {
            PlayTween();
            if (GetComponent<TextMeshProUGUI>() != null)
            {
                GetComponent<TextMeshProUGUI>().text = $"x {multiplierSo.value}";
            }
        }
    }
}

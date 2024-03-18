using TMPro;
using DG.Tweening;
using UnityEngine;

namespace SpinTheWheel
{
    public class SetRewardText : MonoBehaviour
    {
        [SerializeField] IntVariable coinsSo;
        [SerializeField] IntVariable multiplierSo;
        [SerializeField] DOTweenAnimation myTween;
        private void OnEnable()
        {
            SpinTheWheel.SpinWheelStopedAction += SetRewText;
        }
        private void OnDisable()
        {
            SpinTheWheel.SpinWheelStopedAction -= SetRewText;
        }
        private void PlayTween()
        {
            if (myTween != null)
                myTween.DOPlay();
        }
        private void SetRewText(int stopIndex)
        {
            PlayTween();
            if (GetComponent<TextMeshProUGUI>() != null)
            {
                GetComponent<TextMeshProUGUI>().text = $"<sprite=0> {multiplierSo.value * coinsSo.value}";
            }
        }
    }
}

using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ProjectCore;

namespace SpinTheWheel
{
    public class SpinnerFlowController : MonoBehaviour
    {
        [SerializeField] RectTransform SpinnerBody;
        [SerializeField] RectTransform RewardAndmultiplierContainer;
        [SerializeField] Button SpinBtn;

        [Header("ANIMATE")]
        [SerializeField] UIScreenAnimaion SpinnerInOut;
        [SerializeField] UIScreenAnimaion RewardAndmultiplierContainerInOut;
        [SerializeField] JsonReaderSO JsonReaderSO;
        [SerializeField] SpinWheelConfigurationsSo SpinWheelConfigurationsSo;

        public static Action ResetSpinner;

        bool _spinned = false;
        Coroutine MiniStateMachineCo;

        private void Awake()
        {
            if (SpinWheelConfigurationsSo.TestMode)
            {
                gameObject.SetActive(false);
            }
            SpinnerInOut.UiElement = SpinnerBody;
            RewardAndmultiplierContainerInOut.UiElement = RewardAndmultiplierContainer;
        }
        private void Start()
        {
            RewardAndmultiplierContainerInOut.ScaleDown(0);
            MiniStateMachineCo = StartCoroutine(Tick());
        }

        private void OnEnable()
        {
            SpinBtn.onClick.AddListener(SpinBtnClicked);
            SpinTheWheel.SpinWheelStopedAction += SpinnerSpinned;
        }
        private void OnDisable()
        {
            SpinBtn.onClick.RemoveListener(SpinBtnClicked);
            SpinTheWheel.SpinWheelStopedAction -= SpinnerSpinned;
        }

        // Events
        private void SpinBtnClicked()
        {
            SpinBtn.interactable = false;
        }
        private void SpinnerSpinned(int index)
        {
            _spinned = true;
        }
        [Button("Shuffle")]
        private void ShuffleDataInJsonAndResetSpinner()
        {
            //print("ShuffleDataInJsonAndResetSpinner");
            JsonReaderSO.ShuffleValues();
            if (ResetSpinner != null)
                ResetSpinner();
        }


        private IEnumerator Tick()
        {
            while (true)
            {
                yield return new WaitUntil(() => _spinned);
                _spinned = false;

                SpinnerInOut.AnimateOut();

                yield return new WaitForSeconds(0.5f);

                RewardAndmultiplierContainerInOut.ScaleUp(0.7f);

                yield return new WaitForSeconds(2);

                ShuffleDataInJsonAndResetSpinner();
                RewardAndmultiplierContainerInOut.ScaleDown(0.3f);
                SpinnerInOut.AnimateIn();

                yield return new WaitForSeconds(1);

                SpinBtn.interactable = true;
                yield return null; // Yield to the next frame
            }
        }

    }
}

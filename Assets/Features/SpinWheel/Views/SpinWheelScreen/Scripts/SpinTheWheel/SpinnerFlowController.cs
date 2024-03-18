using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
            print("Path : " + Application.persistentDataPath);
            if (SpinWheelConfigurationsSo.TestMode)
            {
                gameObject.SetActive(false);
            }
            SpinnerInOut.UiElement = SpinnerBody;
            RewardAndmultiplierContainerInOut.UiElement = RewardAndmultiplierContainer;
        }
        private void Start()
        {
            RewardAndmultiplierContainerInOut.AnimateOut(0);
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
                RewardAndmultiplierContainerInOut.AnimateIn();
                _spinned = false;
                yield return new WaitForSeconds(3);
                SpinnerInOut.AnimateOut();
                yield return new WaitForSeconds(3);
                ShuffleDataInJsonAndResetSpinner();
                SpinnerInOut.AnimateIn();
                yield return new WaitForSeconds(1);
                RewardAndmultiplierContainerInOut.AnimateOut();
                yield return new WaitForSeconds(1);
                SpinBtn.interactable = true;
                yield return null; // Yield to the next frame
            }
        }

    }
}

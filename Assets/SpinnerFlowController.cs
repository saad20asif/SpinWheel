using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerFlowController : MonoBehaviour
{
    [SerializeField] RectTransform SpinnerBody;
    [SerializeField] RectTransform RewardAndmultiplierContainer;
    [SerializeField] Button SpinBtn;

    [Header("ANIMATE")]
    [SerializeField] UIScreenAnimaion SpinnerInOut;
    [SerializeField] UIScreenAnimaion RewardAndmultiplierContainerInOut;
    [SerializeField] JsonReaderSO JsonReaderSO;

    public static Action ResetSpinner;

    bool _spinned = false;
    Coroutine MiniStateMachineCo;

    private void Awake()
    {
        SpinnerInOut.UiElement = SpinnerBody;
        RewardAndmultiplierContainerInOut.UiElement = RewardAndmultiplierContainer;
    }
    private void Start()
    {
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
        print("Spinn ");
        _spinned = true;
    }
    private void ShuffleDataInJsonAndResetSpinner()
    {
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
            yield return new WaitForSeconds(2);
            ShuffleDataInJsonAndResetSpinner();
            SpinnerInOut.AnimateIn();
            SpinBtn.interactable = true;
            yield return null; // Yield to the next frame
        }
    }


}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinTheWheel : MonoBehaviour
{
    public Transform ObjectToRotate;

    private int _stopIndexBackEnd;
    private bool _isSpinning = false;
    private float _targetRotation;
    private float _spinStartTime;

    [SerializeField] IntVariable TotalSlicesSo;
    [SerializeField] IntVariable multiplierSo;
    [SerializeField] IntVariable coinsSo;
    [SerializeField] SpinWheelConfigurationsSo SpinWheelConfig;
    [SerializeField] JsonReaderSO JsonReaderSO;
    [SerializeField] Button SpinBtn;

    public static Action<int> SpinWheelStopedAction;

    private void OnEnable()
    {
        SpinBtn.onClick.AddListener(SpinWheel);
    }
    private void OnDisable()
    {
        SpinBtn.onClick.RemoveListener(SpinWheel);
    }


    private void SetTheStopIndex()
    {
        TotalSlicesSo.value = JsonReaderSO.data.rewards.Length;
        SpinWheelConfig.StopIndex = SpinWheelConfig.StopIndex % TotalSlicesSo.value; // Making sure that its between (0-totalSlicesInsideSpinner) range
        _stopIndexBackEnd = SpinWheelConfig.StopIndex + (SpinWheelConfig.LeastCycles * TotalSlicesSo.value);
        multiplierSo.value = JsonReaderSO.data.rewards[SpinWheelConfig.StopIndex].multiplier;
        coinsSo.value = JsonReaderSO.data.coins;
    }

    private void SpinWheel()
    {
        if (!_isSpinning)
        {
            SpinBtn.interactable = false;
            SetTheStopIndex();
            // Calculate the target rotation angle based on the stop index and rotation direction
            float rotationMultiplier = SpinWheelConfig.RotationDirection == RotationDirection.Clockwise ? -1f : 1f;
            _targetRotation = (_stopIndexBackEnd * (360 / TotalSlicesSo.value)) * rotationMultiplier;
            // Start spinning coroutine
            StartCoroutine(SpinCoroutine());
        }
    }

    private IEnumerator SpinCoroutine()
    {
        _isSpinning = true;
        _spinStartTime = Time.time;

        while (_isSpinning)
        {
            float elapsed = Time.time - _spinStartTime;
            float t = elapsed / SpinWheelConfig.SpinDuration;
            float curveValue = SpinWheelConfig.RotationCurve.Evaluate(t);
            float currentRotation = Mathf.SmoothStep(0f, _targetRotation, curveValue);

            // Apply rotation to the spinner transform
            ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // Check if spinning should stop
            if (elapsed >= SpinWheelConfig.SpinDuration)
            {
                _isSpinning = false;
                // Optionally, you can adjust the rotation to align perfectly with the target angle
                ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, _targetRotation);
            }
            yield return null;
        }

        if (SpinWheelStopedAction != null)
            SpinWheelStopedAction(SpinWheelConfig.StopIndex);
        SpinBtn.interactable = true;
    }
}

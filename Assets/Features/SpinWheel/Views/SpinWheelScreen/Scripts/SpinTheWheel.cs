using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpinTheWheel : MonoBehaviour
{
    public Transform ObjectToRotate;

    private int _stopIndexBackEnd;
    int _stopIndexTemp;
    private bool _isSpinning = false;
    private float _targetRotation;
    private float _spinStartTime;

    [SerializeField] IntVariable TotalSlicesSo;
    [SerializeField] IntVariable multiplierSo;
    [SerializeField] IntVariable coinsSo;
    [SerializeField] JsonReaderSO JsonReaderSO;
    [SerializeField] Button SpinBtn;

    [Header("Config File")]
    [SerializeField] SpinWheelConfigurationsSo SpinWheelConfig;

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
        _stopIndexTemp = SpinWheelConfig.StopIndex;
        if(SpinWheelConfig.RotationDirection==RotationDirection.Clockwise) 
        {
            // When clocwise is set(if index 1 is passed it will stop at (totalSlices-1)nth index)
            // Thats why we are adjusting the index accordingly
            _stopIndexTemp = TotalSlicesSo.value - _stopIndexTemp;
            _stopIndexTemp %= TotalSlicesSo.value;
            //print($"_stopIndexTemp {_stopIndexTemp}");
            //SpinWheelConfig.StopIndex = TotalSlicesSo.value - SpinWheelConfig.StopIndex;
        }
        _stopIndexBackEnd = _stopIndexTemp + (SpinWheelConfig.RotateCyclesBeforeStopping * TotalSlicesSo.value);
        multiplierSo.value = JsonReaderSO.data.rewards[_stopIndexTemp].multiplier;
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
            float _elapsed = Time.time - _spinStartTime;
            float _t = _elapsed / SpinWheelConfig.SpinDuration;
            float _curveValue = SpinWheelConfig.RotationCurve.Evaluate(_t);
            float _currentRotation = Mathf.SmoothStep(0f, _targetRotation, _curveValue);

            // Apply rotation to the spinner transform
            ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, _currentRotation);

            // Check if spinning should stop
            if (_elapsed >= SpinWheelConfig.SpinDuration)
            {
                _isSpinning = false;
                // Optionally, you can adjust the rotation to align perfectly with the target angle
                ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, _targetRotation);
            }
            yield return null;
        }

        if (SpinWheelStopedAction != null)
            SpinWheelStopedAction(_stopIndexTemp);
        SpinBtn.interactable = true;
    }
}

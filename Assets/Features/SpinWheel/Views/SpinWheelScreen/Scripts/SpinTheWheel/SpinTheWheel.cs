using DG.Tweening;
using MPUIKIT;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using ProjectCore;
using UnityEngine.UI;

namespace SpinTheWheel
{
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
        [SerializeField] Effects EffectsSo;
        [SerializeField] Transform SlicesParent;
        [SerializeField] ProbabilityBaseRandomChooser ProbabilityBaseRandomChooser;

        [SerializeField] Button SpinBtn;

        [Header("Config File")]
        [SerializeField] SpinWheelConfigurationsSo SpinWheelConfig;

        public static Action<int> SpinWheelStopedAction;

        private void OnEnable()
        {
            SpinBtn.onClick.AddListener(SpinWheel);
            SpinnerFlowController.ResetSpinner += ResetTheWheelRotation;
        }
        private void OnDisable()
        {
            SpinBtn.onClick.RemoveListener(SpinWheel);
            SpinnerFlowController.ResetSpinner -= ResetTheWheelRotation;
        }


        private void SetTheStopIndex()
        {
            TotalSlicesSo.value = JsonReaderSO.data.rewards.Length;
            SpinWheelConfig.StopIndex = SpinWheelConfig.StopIndex % TotalSlicesSo.value; // Making sure that its between (0-totalSlicesInsideSpinner) range

            // updating So variables
            multiplierSo.value = JsonReaderSO.data.rewards[SpinWheelConfig.StopIndex].multiplier;
            coinsSo.value = JsonReaderSO.data.coins;

            _stopIndexTemp = SpinWheelConfig.StopIndex;


            if (SpinWheelConfig.RotationDirection == RotationDirection.Clockwise)
            {
                // When clocwise is set(if index 1 is passed it will stop at (totalSlices-1)nth index)
                // Thats why we are adjusting the index accordingly
                _stopIndexTemp = TotalSlicesSo.value - _stopIndexTemp;
                _stopIndexTemp %= TotalSlicesSo.value;
            }
            int rotateCyclesBeforeStopping = SpinWheelConfig.RotateCyclesBeforeStopping;
            if (rotateCyclesBeforeStopping <= 0)//clamping
            {
                rotateCyclesBeforeStopping = SpinWheelConfig.RotateCyclesBeforeStopping = 1;
            }
            _stopIndexBackEnd = _stopIndexTemp + (rotateCyclesBeforeStopping * TotalSlicesSo.value);
        }

        private void ChooseProbabiltyBaseRandomIndex()
        {
            SpinWheelConfig.StopIndex = ProbabilityBaseRandomChooser.ChooseRandomValue();
        }
        public void ResetTheWheelRotation() // index which is on the top in JsonReaderSo should also be on the top of the wheel
        {
            ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        private void SpinWheel()
        {
            if (!_isSpinning)
            {
                SpinBtn.interactable = false;
                if (!SpinWheelConfig.TestMode)  // if testmode not enabled
                    ChooseProbabiltyBaseRandomIndex();
                int previousStopIndex = SpinWheelConfig.StopIndex;
                //print($"previousStopIndex {previousStopIndex}");
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
            SpinnerRotationCompleted();
            if(SpinWheelConfig.TestMode)
                Invoke(nameof(ResetToIntialRotation), 1f);
        }
        [Button("Shuffle")]
        private void shuff()
        {
            JsonReaderSO.ShuffleValues();
        }
        private void SpinnerRotationCompleted()
        {
            
            MPImage selectedImage = SlicesParent.GetChild(SpinWheelConfig.StopIndex).GetComponent<MPImage>();
            EffectsSo.GlowImage(selectedImage);
            if (SpinWheelStopedAction != null)
                SpinWheelStopedAction(SpinWheelConfig.StopIndex);

            //if (SpinWheelConfig.TestMode)
            //    SpinBtn.interactable = true;
        }
        private void ResetToIntialRotation()
        {
            ObjectToRotate.DOLocalRotate(Vector3.zero, 0.3f).SetEase(Ease.OutBack).OnComplete(() => SpinBtn.interactable = true);

        }
    }
}

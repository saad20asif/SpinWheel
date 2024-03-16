using System.Collections;
using UnityEngine;

public class SpinTheWheel : MonoBehaviour
{
    public Transform ObjectToRotate;
    public float SpinDuration = 3f; // Duration in seconds for the spinner to spin
    public int StopIndex = 0; // Index where the spinner should stop (0 for the first slice)
    public int LeastCycles = 1; // if value is 1, then spinner will spin atleast one time 360 before stopping at choosen index

    private int _stopIndexBackEnd;
    private bool _isSpinning = false;
    private float _targetRotation;
    private float _spinStartTime;

    [SerializeField] IntVariable TotalSlicesSo;

    void Start()
    {
        SetTheStopIndex();
        // Calculate the target rotation angle based on the stop index
        _targetRotation = _stopIndexBackEnd * (360/ TotalSlicesSo.value); // Assuming each slice is 45 degrees
        SpinWheel();
    }
    private void SetTheStopIndex()
    {
        StopIndex = StopIndex % TotalSlicesSo.value; // Making sure that its between (0-totalSlicesInsideSpinner) range
        _stopIndexBackEnd = StopIndex + (LeastCycles * TotalSlicesSo.value);
    }

    public void SpinWheel()
    {
        if (!_isSpinning)
        {
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
            float t = elapsed / SpinDuration;
            float currentRotation = Mathf.SmoothStep(0f, _targetRotation, t);

            // Apply rotation to the spinner transform
            ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // Check if spinning should stop
            if (elapsed >= SpinDuration)
            {
                _isSpinning = false;
                // Optionally, you can adjust the rotation to align perfectly with the target angle
                ObjectToRotate.rotation = Quaternion.Euler(0f, 0f, _targetRotation);
            }

            yield return null;
        }
    }
}

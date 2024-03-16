using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTheWheel : MonoBehaviour
{
    [SerializeField] int targetIndex;
    public Transform wheelTransform;
    public float targetRotationAngle;
    public float rotationDuration = 2f;
    public AnimationCurve rotationCurve;

    private bool isSpinning = false;

    private void Start()
    {
        SpinWheel(targetIndex);
    }
    public void SpinWheel(int targetIndex)
    {
        if (isSpinning)
            return;

        float totalSlices = GetTotalSlices();
        float sliceAngle = 360f / totalSlices;
        targetRotationAngle = targetIndex * sliceAngle;

        StartCoroutine(SpinCoroutine());
    }

    private IEnumerator SpinCoroutine()
    {
        isSpinning = true;
        float startRotation = wheelTransform.eulerAngles.z;
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            float progress = timeElapsed / rotationDuration;
            float easedProgress = rotationCurve.Evaluate(progress);

            float currentRotation = Mathf.Lerp(startRotation, targetRotationAngle, easedProgress);
            wheelTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        wheelTransform.rotation = Quaternion.Euler(0f, 0f, targetRotationAngle); // Ensure exact target rotation
        isSpinning = false;
    }

    private int GetTotalSlices()
    {
        // Calculate the total number of slices in the wheel
        // You can customize this based on your wheel setup
        return 8; // Example: 8 slices in the wheel
    }
}

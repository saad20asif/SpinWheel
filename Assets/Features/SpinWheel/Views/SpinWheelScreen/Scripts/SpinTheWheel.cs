using System.Collections;
using UnityEngine;

public class SpinTheWheel : MonoBehaviour
{
    public Transform spinnerTransform;
    public float spinDuration = 3f; // Duration in seconds for the spinner to spin
    public int stopIndex = 0; // Index where the spinner should stop (0 for the first slice)

    private bool isSpinning = false;
    private float targetRotation;
    private float spinStartTime;

    void Start()
    {
        // Calculate the target rotation angle based on the stop index
        targetRotation = stopIndex * 45f; // Assuming each slice is 45 degrees
        SpinWheel();
    }

    public void SpinWheel()
    {
        if (!isSpinning)
        {
            // Start spinning coroutine
            StartCoroutine(SpinCoroutine());
        }
    }

    private IEnumerator SpinCoroutine()
    {
        isSpinning = true;
        spinStartTime = Time.time;

        while (isSpinning)
        {
            float elapsed = Time.time - spinStartTime;
            float t = elapsed / spinDuration;
            float currentRotation = Mathf.SmoothStep(0f, targetRotation, t);

            // Apply rotation to the spinner transform
            spinnerTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // Check if spinning should stop
            if (elapsed >= spinDuration)
            {
                isSpinning = false;
                // Optionally, you can adjust the rotation to align perfectly with the target angle
                spinnerTransform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
            }

            yield return null;
        }
    }
}

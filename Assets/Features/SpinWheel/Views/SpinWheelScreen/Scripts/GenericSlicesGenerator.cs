using MPUIKIT;
using Sirenix.OdinInspector;
using UnityEngine;

// Note : No.of slices inside spin wheel depends on your json file
public class GenericSlicesGenerator : MonoBehaviour
{
    int minLimit = 2;
    int maxLimit = 12;
    float offset = -200;
    private int totalSlicesInsideWheel;

    [SerializeField] string spinWheelSlicePrefabName;
 
    [SerializeField] Transform SlicesParent;
    [SerializeField] JsonReaderSO JsonReaderSO;
    [SerializeField] IntVariable TotalSlicesSo;

    private void Start()
    {
        // If SpinWheel not generated in editor mode or maybe you have changed some values in json later on
        // So we must redraw spinner with updated values and configurations when the game plays
        GenerateSpinWheelSlices();
    }
    private void OnEnable()
    {
        SpinnerFlowController.ResetSpinner += RedrawSpinner;
    }
    private void OnDisable()
    {
        SpinnerFlowController.ResetSpinner += RedrawSpinner;
    }

    [Button("Generate Spin Wheel Slices")]
    private void GenerateSpinWheelSlices()
    {
        JsonReaderSO.LoadDataFromFile();
        totalSlicesInsideWheel = TotalSlicesSo.value;
        if (totalSlicesInsideWheel >= minLimit && totalSlicesInsideWheel <= maxLimit)
        {
            RedrawSpinner();
        }
        else
            Debug.LogError($"Out of Range!! No of slices must be between {minLimit} to {maxLimit}");
    }
    private void RedrawSpinner()
    {
        Regenerate();
        SpawnSlices();
    }
  
    private void Regenerate()
    {
        SlicesParent.localEulerAngles = Vector3.zero;
        int childCount = SlicesParent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(SlicesParent.GetChild(i).gameObject);
        }
    }



    private void SpawnSlices()
    {
        GameObject slice;
        float sliceFillAmount = 1f / totalSlicesInsideWheel; // Use 1f for float division
        float rotationAngle = 0f; // Start rotation angle at 0
        float rotateBy = 360f / totalSlicesInsideWheel; // Calculate the rotation increment
        SlicesParent.localEulerAngles = Vector3.zero;
        SlicesParent.Rotate(0, 0, -rotateBy / 2); // to keep it in center

        for (int i = 0; i < totalSlicesInsideWheel; i++)
        {
            slice = Instantiate(Resources.Load(spinWheelSlicePrefabName), SlicesParent) as GameObject;
            slice.name = $"Slice No.{i}";

            slice.GetComponent<MPImage>().fillAmount = sliceFillAmount;

            slice.transform.Rotate(0, 0, rotationAngle); // Rotate the slice by the current rotation angle

            // Update the rotation angle for the next slice
            rotationAngle -= rotateBy; // Subtract rotateBy for clockwise rotation

            // Ensure rotation stays within 0-360 range
            if (rotationAngle < 0f)
            {
                rotationAngle += 360f;
            }

            slice.GetComponent<SliceInfo>().Probability = JsonReaderSO.data.rewards[i].probability;
            slice.GetComponent<SliceInfo>().Multiplier = JsonReaderSO.data.rewards[i].multiplier;
            slice.GetComponent<SliceInfo>().SetColor(JsonReaderSO.data.rewards[i].Color);
        }
    }


}

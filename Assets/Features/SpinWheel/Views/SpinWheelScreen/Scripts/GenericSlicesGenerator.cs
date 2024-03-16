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
 
    [SerializeField] Transform slicesParent;
    [SerializeField] JsonReaderSO jsonReaderSO;


    [Button("Generate Spin Wheel Slices")]
    private void GenerateSpinWheelSlices()
    {
        LoadFromJson();
        totalSlicesInsideWheel = jsonReaderSO.slicesData.totalSlices;
        if (totalSlicesInsideWheel >= minLimit && totalSlicesInsideWheel <= maxLimit)
        {
            Regenerate();
            SpawnSlices();
        }
        else
            Debug.LogError($"Out of Range!! No of slices must be between {minLimit} to {maxLimit}");
    }
    //[Button("Load Data From Json")]
    private void LoadFromJson()
    {
        jsonReaderSO.ResetData();
        jsonReaderSO.LoadDataFromFile();
    }
    private void Regenerate()
    {
        slicesParent.localEulerAngles = Vector3.zero;
        int childCount = slicesParent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(slicesParent.GetChild(i).gameObject);
        }
    }



    private void SpawnSlices()
    {
        GameObject slice;
        float sliceFillAmount = 1f / totalSlicesInsideWheel; // Use 1f for float division
        float rotationAngle = 0f; // Start rotation angle at 0
        float rotateBy = 360f / totalSlicesInsideWheel; // Calculate the rotation increment
        slicesParent.localEulerAngles = Vector3.zero;
        slicesParent.Rotate(0, 0, -rotateBy / 2); // to keep it in center

        for (int i = 0; i < totalSlicesInsideWheel; i++)
        {
            slice = Instantiate(Resources.Load(spinWheelSlicePrefabName), slicesParent) as GameObject;
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

            slice.GetComponent<SliceInfo>().Probability = jsonReaderSO.slicesData.rewards[i].probability;
            slice.GetComponent<SliceInfo>().Multiplier = jsonReaderSO.slicesData.rewards[i].multiplier;
            slice.GetComponent<SliceInfo>().SetColor(jsonReaderSO.slicesData.rewards[i].Color);
        }
    }


}

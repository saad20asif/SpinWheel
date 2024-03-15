using MPUIKIT;
using Sirenix.OdinInspector;
using UnityEngine;

public class GenericSlicesGenerator : MonoBehaviour
{
    [SerializeField] string prefabName;
    [SerializeField] int totalSlicesInsideWheel;
    [SerializeField] Color[] colors;
    int minLimit = 2;
    int maxLimit = 12;


    [Button("Generate Spin Wheel Slices")]
    private void GenerateSpinWheelSlices()
    {
        if (totalSlicesInsideWheel >= minLimit && totalSlicesInsideWheel <= maxLimit)
        {
            Regenerate();
            SpawnSlices();
        }
        else
            Debug.LogError($"Out of Range!! No of slices must be between {minLimit} to {maxLimit}");
    }
    private void Regenerate()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }



    private void SpawnSlices()
    {
        GameObject slice;
        float sliceFillAmount = 1 / (float)totalSlicesInsideWheel;
        float rotationAngle = 360 / (float)totalSlicesInsideWheel;
        float rotateBy = rotationAngle;
        for (int i=0;i<totalSlicesInsideWheel;i++)
        {
            slice = Instantiate(Resources.Load(prefabName), transform) as GameObject;
            slice.name = $"Slice No.{i}";
            slice.GetComponent<MPImage>().fillAmount = sliceFillAmount;
            if(i>0)
            {
                slice.transform.Rotate(0, 0, rotationAngle);
                rotationAngle += rotateBy;
            }
            slice.GetComponent<MPImage>().color = colors[i];
        }
    }
}

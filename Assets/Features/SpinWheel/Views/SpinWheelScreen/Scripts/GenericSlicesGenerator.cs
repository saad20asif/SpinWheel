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
        int childCount = slicesParent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(slicesParent.GetChild(i).gameObject);
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
            slice = Instantiate(Resources.Load(spinWheelSlicePrefabName), slicesParent) as GameObject;
            slice.name = $"Slice No.{i}";
            Vector3 textOffset = new Vector2(offset / 2.6117f, offset);

            //slice.GetComponent<CenterTextAndIcon>().SetPositions();
            slice.GetComponent<MPImage>().fillAmount = sliceFillAmount;
            if(i>0)
            {
                slice.transform.Rotate(0, 0, rotationAngle);
                rotationAngle += rotateBy;
            }
            slice.GetComponent<SliceInfo>().Probability = jsonReaderSO.slicesData.rewards[i].probability;
            slice.GetComponent<SliceInfo>().Multiplier = jsonReaderSO.slicesData.rewards[i].multiplier;
            slice.GetComponent<SliceInfo>().SetColor(jsonReaderSO.slicesData.rewards[i].Color);

        }
    }
}

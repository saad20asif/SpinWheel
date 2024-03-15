using MPUIKIT;
using UnityEngine;

public class GenericSlicesGenerator : MonoBehaviour
{
    [SerializeField] string prefabName;
    [SerializeField] int totalSlicesInsideWheel;
    [SerializeField] Color[] colors;
    int minLimit = 2;
    int maxLimit = 12;

    private void Start()
    {
        SpawnSlices();
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

using MPUIKIT;
using Sirenix.OdinInspector;
using UnityEngine;

// Note : No.of slices inside spin wheel depends on your json file
public class GenericSlicesGenerator : MonoBehaviour
{
    int _minLimit = 2;
    int _maxLimit = 12;

    private int totalSlicesInsideWheel;
    
    [SerializeField] private string _sliceTextPrefabName;
    [SerializeField] private float _yTextsOffset = 260f;

    [SerializeField] string spinWheelSlicePrefabName;
 
    [SerializeField] Transform SlicesParent;
    [SerializeField] Transform AllTextsParent;
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
    
    
    [Button("GenerateTexts")]
    private void GenerateTexts()
    {
        float currentAngle = 0;
        float rotationangle = 360 / (float)totalSlicesInsideWheel;
        DeletePrevious();
        for (int i = 0; i < totalSlicesInsideWheel; i++)
        {
            GameObject textPrefb = Instantiate(Resources.Load(_sliceTextPrefabName),AllTextsParent)as GameObject;
            textPrefb.transform.Rotate(0,0,currentAngle);
            Vector2 pos = textPrefb.transform.GetChild(0).transform.localPosition;
            pos.y += _yTextsOffset;
            textPrefb.transform.GetChild(0).transform.localPosition = pos;
            currentAngle += rotationangle;
        }
    }
    private void DeletePrevious()
    {
        print(AllTextsParent.childCount);
        for (int i = AllTextsParent.childCount-1; i >= 0; i--)
        {
            DestroyImmediate(AllTextsParent.GetChild(i).gameObject); 
        }
    }

    [Button("Generate Spin Wheel Slices")]
    private void GenerateSpinWheelSlices()
    {
        JsonReaderSO.LoadDataFromFile();
        totalSlicesInsideWheel = TotalSlicesSo.value;
        if (totalSlicesInsideWheel >= _minLimit && totalSlicesInsideWheel <= _maxLimit)
        {
            RedrawSpinner();
        }
        else
            Debug.LogError($"Out of Range!! No of slices must be between {_minLimit} to {_maxLimit}");
    }
    private void RedrawSpinner()
    {
        Regenerate();
        SpawnSlices();
        GenerateTexts();
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

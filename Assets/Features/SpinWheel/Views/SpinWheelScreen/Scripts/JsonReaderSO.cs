using Sirenix.OdinInspector;
using System.IO;
using UnityEngine;


[System.Serializable]
public class SpinnerWheelSlice
{
    public int multiplier;
    public float probability;
    public string color;

    public Color Color
    {
        get
        {
            Color parsedColor;
            if (ColorUtility.TryParseHtmlString(color, out parsedColor))
            {
                return parsedColor;
            }
            else
            {
                Debug.LogError("Failed to parse color from string: " + color);
                return Color.white; // Return default color if parsing fails
            }
        }
    }
}

[System.Serializable]
public class ConfigData
{
    public int coins;
    public SpinnerWheelSlice[] rewards;
}

[CreateAssetMenu(fileName = "JsonReaderData", menuName = "ScriptableObjects/JsonReaderSO", order = 1)]
public class JsonReaderSO : JsonReaderBase<ConfigData>
{
    //public ConfigData slicesData;
    [SerializeField] private IntVariable _totalCoinsSo;
    [SerializeField] private IntVariable _totalSlicesSo;
    [SerializeField] private string jsonFilePath; // Assign your JSON string in the Inspector


    public void LoadDataFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, jsonFilePath);
        LoadData(filePath);
        ConfigData configData = data;
        _totalSlicesSo.value = data.rewards.Length;
        _totalCoinsSo.value = data.coins;
        //data.totalSlices = data.rewards.Length;
    }
    public void ResetData()
    {
        // Reset slicesData to default values or clear it
        data.coins = 0;
        //data.totalSlices = 0;
        data.rewards = new SpinnerWheelSlice[0]; // Clear the rewards array
    }
}

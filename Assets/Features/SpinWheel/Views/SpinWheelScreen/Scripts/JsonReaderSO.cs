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
    public int totalSlices;
    public SpinnerWheelSlice[] rewards;
}

[CreateAssetMenu(fileName = "JsonReaderData", menuName = "ScriptableObjects/JsonReaderSO", order = 1)]
public class JsonReaderSO : JsonReaderBase<ConfigData>
{
    public ConfigData slicesData;
    [SerializeField] private string jsonFilePath; // Assign your JSON string in the Inspector


    public void LoadDataFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, jsonFilePath);
        LoadData(filePath);
        ConfigData configData = data;
        slicesData.rewards = new SpinnerWheelSlice[configData.rewards.Length];
        slicesData.coins = configData.coins;
        slicesData.totalSlices = configData.rewards.Length;
        for (int i = 0; i < configData.rewards.Length; i++)
        {
            slicesData.rewards[i] = new SpinnerWheelSlice();
            slicesData.rewards[i].multiplier = configData.rewards[i].multiplier;
            slicesData.rewards[i].probability = configData.rewards[i].probability;
            slicesData.rewards[i].color = configData.rewards[i].color;
        }

    }
    public void ResetData()
    {
        // Reset slicesData to default values or clear it
        slicesData.coins = 0;
        slicesData.totalSlices = 0;
        slicesData.rewards = new SpinnerWheelSlice[0]; // Clear the rewards array
    }
}

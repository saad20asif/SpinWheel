using Sirenix.OdinInspector;
using System.IO;
using UnityEngine;


[System.Serializable]
public class SpinnerWheelSlice
{
    public int multiplier;
    public float probability;
    public string color;
}

[System.Serializable]
public class ConfigData
{
    public int coins;
    public int totalSlices;
    public SpinnerWheelSlice[] rewards;
}

[CreateAssetMenu(fileName = "JsonReaderData", menuName = "ScriptableObjects/JsonReaderSO", order = 1)]
public class JsonReaderSO : ScriptableObject
{
    public ConfigData slicesData;
    [SerializeField] private string jsonFilePath; // Assign your JSON string in the Inspector

    public void LoadDataFromFile()
    {
        if (string.IsNullOrEmpty(jsonFilePath))
        {
            Debug.LogError("JSON file path is not set in the Inspector!");
            return;
        }

        string filePath = Path.Combine(Application.persistentDataPath, jsonFilePath);

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);

            ConfigData configData = JsonUtility.FromJson<ConfigData>(jsonString);
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
        else
        {
            Debug.LogError("JSON file not found at path: " + filePath);
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

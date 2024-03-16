using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;


public class JsonReader : MonoBehaviour
{
    public ConfigData slicesData;
    [SerializeField]private string jsonFilePath; // Assign your JSON string in the Inspector

    [Button("Load Data")]
    private void LoadData()
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
            //print("Length : " + configData.rewards.Length);
            slicesData.coins = configData.coins;
            for (int i=0;i<configData.rewards.Length;i++)
            {
                slicesData.rewards[i] = new SpinnerWheelSlice();
                slicesData.rewards[i].multiplier = configData.rewards[i].multiplier;
                slicesData.rewards[i].probability = configData.rewards[i].probability;
                slicesData.rewards[i].color = configData.rewards[i].color;
                //Debug.Log("Multiplier: " + configData.rewards[i].multiplier);
                //Debug.Log("Probability: " + configData.rewards[i].probability);
                //Debug.Log("Color: " + configData.rewards[i].color);
                //Debug.Log("--------------------------------------------------");
            }
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + filePath);
        }
    }
}

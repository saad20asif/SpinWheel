using UnityEngine;
using System.IO;

public abstract class JsonReaderBase<T> : ScriptableObject where T : new()
{
    public T data;

    public void LoadData(string jsonFilePath)
    {
        if (string.IsNullOrEmpty(jsonFilePath))
        {
            Debug.LogError("JSON file path is not set!");
            return;
        }

        string filePath = Path.Combine(Application.persistentDataPath, jsonFilePath);

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<T>(jsonString);
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + filePath);
        }
    }

    //public abstract void ResetData();
}

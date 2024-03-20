using UnityEngine;

namespace ProjectCore
{
    public abstract class JsonReaderBase<T> : ScriptableObject where T : new()
    {
        public T data;

        public void LoadData(string jsonFileName)
        {
            if (string.IsNullOrEmpty(jsonFileName))
            {
                Debug.LogError("JSON file name is not set!");
                return;
            }

            T loadedData = default(T);

            // Load the TextAsset from Resources folder
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
            if (jsonFile != null)
            {
                string jsonString = jsonFile.text;
                loadedData = JsonUtility.FromJson<T>(jsonString);
            }
            else
            {
                Debug.LogError("JSON file not found with name: " + jsonFileName);
            }

            data = loadedData;
        }
    }
}
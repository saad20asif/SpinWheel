using Sirenix.OdinInspector;
using System.IO;
using UnityEngine;

namespace SpinTheWheel
{
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

    [CreateAssetMenu(fileName = "JsonReaderData", menuName = "ScriptableObjects/JsonReaderSO", order = 2)]
    public class JsonReaderSO : JsonReaderBase<ConfigData>
    {
        //public ConfigData slicesData;
        [SerializeField] private IntVariable _totalCoinsSo;
        [SerializeField] private IntVariable _totalSlicesSo;
        [SerializeField] ProbabilityBaseRandomChooser ProbabilityBaseRandomChooser;
        private string jsonFileName = "data"; // Assign your JSON string in the Inspector


        public void LoadDataFromFile()
        {
            ResetData();
            //string filePath = Path.Combine(Application.persistentDataPath, jsonFilePath);
            LoadData(jsonFileName);
            AssignValuesToRandomChooseSo();
            _totalSlicesSo.value = data.rewards.Length;
            _totalCoinsSo.value = data.coins;
        }
        private void AssignValuesToRandomChooseSo()
        {
            ProbabilityBaseRandomChooser.Values = new int[data.rewards.Length];
            ProbabilityBaseRandomChooser.Probabilities = new float[data.rewards.Length];
            for (int i = 0; i < data.rewards.Length; i++)
            {
                ProbabilityBaseRandomChooser.Values[i] = i;
                ProbabilityBaseRandomChooser.Probabilities[i] = data.rewards[i].probability;
            }
        }
        public void ShuffleValues()
        {
            for (int i = data.rewards.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                //Debug.Log($"{i} and {randomIndex} swapped");

                // Swap rewards array elements
                SpinnerWheelSlice tempSlice = data.rewards[i];
                data.rewards[i] = data.rewards[randomIndex];
                data.rewards[randomIndex] = tempSlice;

                // Swap Values array elements in ProbabilityBaseRandomChooser
                int tempValue = ProbabilityBaseRandomChooser.Values[i];
                ProbabilityBaseRandomChooser.Values[i] = ProbabilityBaseRandomChooser.Values[randomIndex];
                ProbabilityBaseRandomChooser.Values[randomIndex] = tempValue;

                // Swap Probabilities array elements in ProbabilityBaseRandomChooser
                float tempProbability = ProbabilityBaseRandomChooser.Probabilities[i];
                ProbabilityBaseRandomChooser.Probabilities[i] = ProbabilityBaseRandomChooser.Probabilities[randomIndex];
                ProbabilityBaseRandomChooser.Probabilities[randomIndex] = tempProbability;
            }
        }
        private void ResetData()
        {
            // Reset slicesData to default values or clear it
            data.coins = 0;
            //data.totalSlices = 0;
            data.rewards = new SpinnerWheelSlice[0]; // Clear the rewards array
        }
    }
}

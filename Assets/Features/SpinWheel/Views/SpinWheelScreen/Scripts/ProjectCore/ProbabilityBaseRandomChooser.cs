using UnityEngine;

namespace ProjectCore
{

    [CreateAssetMenu(fileName = "ProbabilityBaseRandomChooser", menuName = "ScriptableObjects/ProbabilityBaseRandomChooser", order = 3)]
    public class ProbabilityBaseRandomChooser : ScriptableObject
    {
        [SerializeField] private int[] values;
        [SerializeField] private float[] probabilities;

        public int[] Values
        {
            get { return values; }
            set { values = value; }
        }

        public float[] Probabilities
        {
            get { return probabilities; }
            set { probabilities = value; }
        }
        public int ChooseRandomValue()
        {
            float randomValue = Random.value;
            float cumulativeProbability = 0f;

            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulativeProbability += probabilities[i];
                if (randomValue <= cumulativeProbability)
                {
                    return values[i];
                }
            }

            // If no value is chosen based on probabilities, return the last value
            return values[values.Length - 1];
        }
    }
}

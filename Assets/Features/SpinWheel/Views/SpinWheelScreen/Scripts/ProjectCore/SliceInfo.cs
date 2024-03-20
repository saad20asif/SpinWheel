using MPUIKIT;
using UnityEngine;
using TMPro;

namespace ProjectCore
{
    public class SliceInfo : MonoBehaviour
    {
        [SerializeField] private MPImage circleImage;
        [SerializeField] private TextMeshProUGUI multiplierText;
        [SerializeField] private float probability;
        [SerializeField] private int multiplier;


        public TextMeshProUGUI MultiplierText
        {
            get { return multiplierText; }
            set { multiplierText = value; }
        }
        public MPImage CircleImage
        {
            get { return circleImage; }
            set { circleImage = value; }
        }
        public float Probability
        {
            get { return probability; }
            set { probability = value; }
        }

        // Getter and setter for multiplier
        public int Multiplier
        {
            get { return multiplier; }
            set
            {
                multiplier = value;
                if (multiplierText != null)
                {
                    multiplierText.text = $"X {multiplier.ToString()}";
                }
            }
        }
        public void SetColor(Color color)
        {
            circleImage.color = color;
        }
    }
}

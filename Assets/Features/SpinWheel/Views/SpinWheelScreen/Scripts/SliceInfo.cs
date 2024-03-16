using MPUIKIT;
using UnityEngine;
using TMPro;

public class SliceInfo : MonoBehaviour
{
    [SerializeField] private MPImage circleImage;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private RectTransform iconRect;
    [SerializeField] private float probability;
    [SerializeField] private int multiplier;

    // Getter and setter for circleImage
    public MPImage CircleImage
    {
        get { return circleImage; }
        set { circleImage = value; }
    }
    public RectTransform IconRect
    {
        get { return iconRect; }
        set { iconRect = value; }
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
                multiplierText.text = multiplier.ToString();
            }
        }
    }
    public void SetColor(Color color)
    {
        circleImage.color = color;
    }
}

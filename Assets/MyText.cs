using TMPro;
using UnityEngine;

public class MyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI multiplierText;
    public TextMeshProUGUI MultiplierText
    {
        get { return multiplierText; }
        set { multiplierText = value; }
    }
}

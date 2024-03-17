using UnityEngine;
public enum RotationDirection
{
    Clockwise,
    AntiClockwise
}
[CreateAssetMenu(fileName = "SpinWheelConfigurationsSo", menuName = "ScriptableObjects/SpinWheel/SpinWheelConfigurationsSo", order = 0)]
public class SpinWheelConfigurationsSo : ScriptableObject
{
    [SerializeField]
    private float spinDuration = 3f; // Duration in seconds for the spinner to spin
    [SerializeField]
    private int stopIndex = 0; // Index where the spinner should stop (0 for the first slice)
    [SerializeField]
    private int rotateCyclesBeforeStopping = 1; // if value is 1, then spinner will spin atleast one time 360 before stopping at choosen index
    [SerializeField]
    private RotationDirection rotationDirection = RotationDirection.Clockwise; // Direction of rotation (clockwise or counterclockwise)
    [SerializeField]
    private AnimationCurve rotationCurve; // Animation curve for controlling rotation interpolation
    [Header("If You Want To Set [Stop Index] Yourself To test, Then Enable testMode")]
    [SerializeField] private bool testMode = false;

    public float SpinDuration
    {
        get { return spinDuration; }
        set { spinDuration = value; }
    }

    public int StopIndex
    {
        get { return stopIndex; }
        set { stopIndex = value; }
    }

    public int RotateCyclesBeforeStopping
    {
        get { return rotateCyclesBeforeStopping; }
        set { rotateCyclesBeforeStopping = value; }
    }

    public RotationDirection RotationDirection
    {
        get { return rotationDirection; }
        set { rotationDirection = value; }
    }

    public AnimationCurve RotationCurve
    {
        get { return rotationCurve; }
        set { rotationCurve = value; }
    }
    public bool TestMode
    {
        get { return testMode; }
        set { testMode = value; }
    }
}

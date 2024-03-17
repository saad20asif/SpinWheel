using UnityEngine;
public enum RotationDirection
{
    Clockwise,
    AntiClockwise
}
[CreateAssetMenu(fileName = "SpinWheelConfigurationsSo", menuName = "ScriptableObjects/SpinWheel/SpinWheelConfigurationsSo", order = 0)]
public class SpinWheelConfigurationsSo : ScriptableObject
{
    public float SpinDuration = 3f; // Duration in seconds for the spinner to spin
    public int StopIndex = 0; // Index where the spinner should stop (0 for the first slice)
    public int RotateCyclesBeforeStopping = 1; // if value is 1, then spinner will spin atleast one time 360 before stopping at choosen index
    public RotationDirection RotationDirection = RotationDirection.Clockwise; // Direction of rotation (clockwise or counterclockwise)
    public AnimationCurve RotationCurve; // Animation curve for controlling rotation interpolation
}

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[CreateAssetMenu(fileName = "Effects", menuName = "ScriptableObjects/Effects", order = 1)]
public class Effects : ScriptableObject
{
    [SerializeField] private Image _imageToGlow;
    [SerializeField] private Color _glowColor = Color.white; // Change this to the desired glow color
    [SerializeField] private float _glowDuration = 0.5f; // Duration of the glow effect
    [SerializeField] private int _loopCount = 2; // Duration of the glow effect

    private Color originalColor;

    public void GlowImage(Image glowIm)
    {
        originalColor = glowIm.color;
        _imageToGlow = glowIm;
        EnableGlowEffect();
    }
    private void EnableGlowEffect()
    {
        _imageToGlow.DOColor(_glowColor, _glowDuration)
            .SetLoops(_loopCount, LoopType.Yoyo) // Loop indefinitely (glow and fade)
            .OnComplete(() => DisableGlowEffect()); // Callback when tween completes
    }

    private void DisableGlowEffect()
    {
        _imageToGlow.DOKill();
        _imageToGlow.color = originalColor;
    }
}

using DG.Tweening;
using UnityEngine;

public enum MoveDirection
{
    HorizontalLeft,
    HorizontalRight,
    VerticalUp,
    VerticalDown
}

[CreateAssetMenu(fileName = "UIScreenAnimaion", menuName = "ScriptableObjects/UIScreen Animaion", order = 1)]
public class UIScreenAnimaion : ScriptableObject
{
    [SerializeField] private RectTransform _uiElement;
    [SerializeField] DotweenConfigurationsSoScript InOutFromScreenSo;
    public RectTransform UiElement
    {
        get { return _uiElement; }
        set
        {
            _uiElement = value;
            Initialize(); // Reinitialize when the UI element is set
        }
    }

    [SerializeField] private float offset = 100; // Offset to add to the target position
    [SerializeField] private MoveDirection moveDirection = MoveDirection.HorizontalLeft;

    private Vector2 originalPosition;
    private Vector2 offScreenPosition;

    private void Initialize()
    {
        if (_uiElement == null)
        {
            Debug.LogError("UI element is not assigned!");
            return;
        }

        originalPosition = _uiElement.anchoredPosition;
        CalculateOffScreenPosition();
    }

    private void CalculateOffScreenPosition()
    {
        float xOffset = originalPosition.x;
        float yOffset = originalPosition.y;

        switch (moveDirection)
        {
            case MoveDirection.HorizontalLeft:
                xOffset -= Screen.width + _uiElement.rect.width + offset;
                break;
            case MoveDirection.HorizontalRight:
                xOffset += Screen.width + _uiElement.rect.width + offset;
                break;
            case MoveDirection.VerticalUp:
                yOffset += Screen.height + _uiElement.rect.height + offset;
                break;
            case MoveDirection.VerticalDown:
                yOffset -= Screen.height + _uiElement.rect.height + offset;
                break;
        }

        offScreenPosition = new Vector2(xOffset, yOffset);
    }

    public void AnimateOut()
    {
        if (_uiElement != null)
        {
            _uiElement.DOAnchorPos(offScreenPosition, InOutFromScreenSo.Duration).SetEase(InOutFromScreenSo.Ease).SetDelay(InOutFromScreenSo.Delay);
        }
    }
    public void AnimateOut(float duration)
    {
        if (_uiElement != null)
        {
            _uiElement.DOAnchorPos(offScreenPosition, duration).SetEase(InOutFromScreenSo.Ease).SetDelay(InOutFromScreenSo.Delay);
        }
    }

    public void AnimateIn()
    {
        if (_uiElement != null)
        {
            _uiElement.DOAnchorPos(originalPosition, InOutFromScreenSo.Duration).SetEase(InOutFromScreenSo.Ease).SetDelay(InOutFromScreenSo.Delay);
        }
    }
}

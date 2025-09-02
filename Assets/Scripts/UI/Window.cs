using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _actionButton;

    protected CanvasGroup WindowGroup => _windowGroup;
    protected Button ActionButton => _actionButton;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Open()
    {
        WindowGroup.alpha = 1f;
        ActionButton.interactable = true;
    }

    public void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.interactable = false;
    }

    protected abstract void OnButtonClick();
}
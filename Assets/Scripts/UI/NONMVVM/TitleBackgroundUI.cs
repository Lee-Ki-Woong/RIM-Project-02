using UnityEngine;
using UnityEngine.UI;

public class TitleBackgroundUI : BaseSimpleUIView
{
    [SerializeField] private Image Image_Background;
    [SerializeField] private Button Button_OnBackgroundScreen;

    private void OnEnable()
    {
        Button_OnBackgroundScreen.onClick.AddListener(OnEnterLogin);
        GameManager.Instance.OnLoginSuccess += OnLoginSuccess;
    }

    private void OnDisable()
    {
        Button_OnBackgroundScreen.onClick.RemoveListener(OnEnterLogin);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLoginSuccess -= OnLoginSuccess;
        }
    }

    private void OnEnterLogin()
    {
        UIManager.Instance.OpenLoginUI();
    }

    private void OnLoginSuccess()
    {
        UIManager.Instance.TryCloseUI(UIType.TitleBackgroundUI);
    }
}

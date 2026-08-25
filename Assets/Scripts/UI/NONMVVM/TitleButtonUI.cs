using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonUI : BaseSimpleUIView
{

    [SerializeField] private Button Button_OpenTitleGameOptionUI;
    [SerializeField] private Button Button_OpenExitGamePanel;

    [Header("게임 종료 패널")]
    [SerializeField] private GameObject Panel_ExitGame;

    [SerializeField] private TMP_Text Text_ExitGamePanelTitle;

    [SerializeField] private Button Button_ExitGame;
    [SerializeField] private TMP_Text Text_ExitGameButton;
    
    [SerializeField] private Button Button_CloseExitGamePanel;
    [SerializeField] private TMP_Text Text_CloseExitGamePanelButton;

    private void OnEnable()
    {
        Button_OpenExitGamePanel.onClick.AddListener(OnOpenExitGamePanel);
        Button_OpenTitleGameOptionUI.onClick.AddListener(OnOpenTitleGameOptionUI);

        Button_ExitGame.onClick.AddListener(OnExitGame);
        Button_CloseExitGamePanel.onClick.AddListener(OnCloseExitGamePanel);

        GameManager.Instance.OnLoginSuccess += OnLoginSuccess;

        GameSettingManager.Instance.OnLanguageChanged += RefreshTexts;

        RefreshTexts();

        Panel_ExitGame.SetActive(false);
    }

    private void OnDisable()
    {

        Button_OpenExitGamePanel.onClick.RemoveListener(OnOpenExitGamePanel);
        Button_OpenTitleGameOptionUI.onClick.RemoveListener(OnOpenTitleGameOptionUI);

        Button_ExitGame.onClick.RemoveListener(OnExitGame);
        Button_CloseExitGamePanel.onClick.RemoveListener(OnCloseExitGamePanel);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLoginSuccess -= OnLoginSuccess;
        }

        if (GameSettingManager.Instance != null)
        {
            GameSettingManager.Instance.OnLanguageChanged -= RefreshTexts;
        }
    }

    private void RefreshTexts()
    {
        if (UIDataManager.Instance.TitleButtonUIData.TryGetValue("TitleButtonUIData", out TitleButtonUIDataBase data) == false)
        {
            this.LogWarning($"[TitleButtonUIData]에 해당하는 데이터가 TitleUIDataBase에 없습니다!!");

            return;
        }
        Text_ExitGamePanelTitle.text = data.TitleText;
        Text_ExitGameButton.text = data.ExitGameButton;
        Text_CloseExitGamePanelButton.text = data.CloseExitGamePanelButton;
    }

    private void OnOpenTitleGameOptionUI()
    {
        // TODO : 나중에 타이틀 게임 옵션 UI가 생기면 추가
    }

    private void OnOpenExitGamePanel()
    {
        Panel_ExitGame.SetActive(true);
    }

    private void OnExitGame()
    {
        GameManager.Instance.QuitGame();
    }

    private void OnCloseExitGamePanel()
    {
        Panel_ExitGame.SetActive(false);
    }

    private void OnLoginSuccess()
    {
        UIManager.Instance.TryCloseUI(UIType.TitleButtonUI);
    }
}

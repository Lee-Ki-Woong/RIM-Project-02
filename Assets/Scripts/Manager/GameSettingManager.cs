using System;
using System.IO;
using UnityEngine;

public class GameSettingManager : BaseManager<GameSettingManager>
{
    public GameSettingData GameCurrentSettingData { get; private set; }

    public Language GameCurrentLanguage => LanguageUtil.GetLanguage(GameCurrentSettingData.Language);

    public event Action OnLanguageChanged;

    protected override void InitAction()
    {
        Load();
        ApplyGameSettingData();
    }

    private string GetGameSettingDirectory()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameSetting");

        return path;
    }

    private string GetGameSettingPath()
    {
        string path = Path.Combine(GetGameSettingDirectory(), $"GameSetting.json");

        return path;
    }

    public void Save()
    {
        Directory.CreateDirectory(GetGameSettingDirectory());

        string json = JsonUtility.ToJson(GameCurrentSettingData, true);
        File.WriteAllText(GetGameSettingPath(), json);
    }

    private void Load()
    {
        string path = GetGameSettingPath();

        if(File.Exists(path) == false)
        {
            Log("새로운 게임 설정 데이터를 생성합니다!!");
            GameCurrentSettingData = CreateDefaultSetting();
            Save();

            return;
        }

        try
        {
            string json = File.ReadAllText(path);
            GameSettingData settingData = JsonUtility.FromJson<GameSettingData>(json);

            if (settingData == null)
            {
                LogError($"게임 설정 데이터의 파싱에 실패하였습니다!! 게임 설정 데이터를 새로 생성합니다!!");

                GameCurrentSettingData = CreateDefaultSetting();
                Save();

                return;
            }

            GameCurrentSettingData = settingData;
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);

            LogError($"게임 설정을 불러오는 도중 오류가 발생했습니다!! 게임 설정 데이터를 기본 설정 데이터로 되돌립니다!!");

            GameCurrentSettingData= CreateDefaultSetting();
            Save();
        }
    }

    private GameSettingData CreateDefaultSetting()
    {
        GameSettingData setting = new();
        
        setting.Language = LanguageUtil.GetLanguage(Language.Korean);
        
        setting.ScreenWidth = Screen.currentResolution.width;
        setting.ScreenHeight = Screen.currentResolution.height;

        setting.IsFullScreen = true;
        setting.MasterVolume = 1f;

        setting.IsRememberId = false;
        setting.RememberedId = string.Empty;

        return setting;
    }

    public void SetMasterVolume(float volume)
    {
        GameCurrentSettingData.MasterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = GameCurrentSettingData.MasterVolume;

        Save();
    }

    public void SetResolution(int width, int height, bool isFullScreen)
    {
        GameCurrentSettingData.ScreenWidth = width;
        GameCurrentSettingData.ScreenHeight = height;
        GameCurrentSettingData.IsFullScreen = isFullScreen;

        Screen.SetResolution(GameCurrentSettingData.ScreenWidth, GameCurrentSettingData.ScreenHeight, GameCurrentSettingData.IsFullScreen);

        Save();
    }

    public void ChangeLanguage(Language language)
    {
        GameCurrentSettingData.Language = LanguageUtil.GetLanguage(language);

        Save();

        GameDataManager.Instance.ReloadAllData();
        UIDataManager.Instance.ReloadAllData();

        OnLanguageChanged?.Invoke();
    }

    public void SetRememberId(bool isRemember, string id)
    {
        GameCurrentSettingData.IsRememberId = isRemember;
        GameCurrentSettingData.RememberedId = id;

        Save();
    }

    private void ApplyGameSettingData()
    {
        Screen.SetResolution(GameCurrentSettingData.ScreenWidth, GameCurrentSettingData.ScreenHeight, GameCurrentSettingData.IsFullScreen);
        AudioListener.volume = GameCurrentSettingData.MasterVolume;
    }
}

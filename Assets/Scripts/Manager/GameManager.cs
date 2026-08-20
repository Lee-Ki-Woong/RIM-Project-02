using System;
using UnityEngine;

public class GameManager : BaseMonoManager<GameManager>
{
    [SerializeField] private GameObject Prefab_UIManager;

    public AccountData GameCurrentAccountData { get; private set; }

    protected override void AwakeAction()
    {
        ResourceManager.Create();
        NetworkManager.Create();
        GameSettingManager.Create();
        GameDataManager.Create();
        UIDataManager.Create();
        SkinManager.Create();

        Instantiate(Prefab_UIManager, this.transform);
    }

    public void SetAccount(AccountData account)
    {
        GameCurrentAccountData = account;
    }

    public void SaveAccount()
    {
        if(GameCurrentAccountData == null)
        {
            this.LogWarning("로그인한 계정이 없어 저장할 수 없습니다!!");
            return;
        }

        NetworkManager.Instance.RequestSaveData(GameCurrentAccountData);
    }

    public void QuitGame()
    {
        SaveAccount();
        Application.Quit();
    }
}

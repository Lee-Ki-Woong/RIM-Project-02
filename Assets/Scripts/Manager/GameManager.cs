using UnityEngine;

public class GameManager : BaseMonoManager<GameManager>
{
    [SerializeField] private GameObject Prefab_UIManager;

    public AccountData GameCurrentAccountData { get; private set; }
    public PlayerService PlayerService { get; private set; }

    protected override void AwakeAction()
    {
        ResourceManager.Create();
        NetworkManager.Create();
        GameSettingManager.Create();
        GameDataManager.Create();
        UIDataManager.Create();

        Instantiate(Prefab_UIManager, this.transform);
    }

    public void SetAccount(AccountData account)
    {
        GameCurrentAccountData = account;
        PlayerService = new(account.GameSaveData.PlayerData);
    }

    public void SaveAccount()
    {
        if(GameCurrentAccountData == null)
        {
            this.LogWarning("로그인한 계정이 없어 저장할 수 없습니다!!");
            return;
        }

        PlayerService.ApplyToData(GameCurrentAccountData.GameSaveData.PlayerData);

        NetworkManager.Instance.RequestSaveData(GameCurrentAccountData);
    }

    public void QuitGame()
    {
        SaveAccount();
        Application.Quit();
    }
}

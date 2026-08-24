public class NetworkManager : BaseManager<NetworkManager>
{
    private AccountFileService _accountFileService;
    private AccountIndexService _accountIndexService;

    protected override void InitAction()
    {
        _accountFileService = new();
        _accountIndexService = new(_accountFileService);
    }

    public CreateAccountResult TryCreateAccount(string id, string password, string passwordConfirm, out AccountData account)
    {
        account = null;

        string loweredId = id.ToLowerInvariant();

        if (AccountUtil.ValidateId(loweredId) != IdValidationResult.Valid)
        {
            return CreateAccountResult.InvalidId;
        }

        if (AccountUtil.ValidatePassword(password) != PasswordValidationResult.Valid)
        {
            return CreateAccountResult.InvalidPassword;
        }

        if (password != passwordConfirm)
        {
            return CreateAccountResult.PasswordMismatch;
        }

        if (_accountIndexService.FindPlayerUid(loweredId) != null)
        {
            return CreateAccountResult.AlreadyExists;
        }

        account = new AccountData();
        account.Id = loweredId;
        account.PlayerUID = _accountIndexService.IssuePlayerUid();
        account.PasswordHash = AccountUtil.HashPassword(password);
        account.GameSaveData = CreateNewSaveData();

        // 계정 파일을 먼저 쓰고 색인을 갱신한다.
        // 순서가 반대면 색인에는 있는데 파일이 없어 로그인이 실패한다.
        _accountFileService.Save(account);
        _accountIndexService.Register(account.Id, account.PlayerUID);

        Log($"새로운 계정을 생성하였습니다!! 계정 Id : {account.Id}, PlayerUID : {account.PlayerUID}");

        return CreateAccountResult.Success;
    }

    public LoginResult TryLogin(string id, string password, out AccountData account)
    {
        account = null;

        string loweredId = id.ToLowerInvariant();

        if (AccountUtil.ValidateId(loweredId) != IdValidationResult.Valid)
        {
            return LoginResult.Failed;
        }

        if (AccountUtil.ValidatePassword(password) != PasswordValidationResult.Valid)
        {
            return LoginResult.Failed;
        }

        string playerUID = _accountIndexService.FindPlayerUid(loweredId);

        if (string.IsNullOrEmpty(playerUID))
        {
            return LoginResult.Failed;
        }

        AccountData loaded = _accountFileService.Load(playerUID);

        if (loaded == null)
        {
            return LoginResult.Failed;
        }

        if (loaded.PasswordHash != AccountUtil.HashPassword(password))
        {
            return LoginResult.Failed;
        }

        Log($"계정 로드 완료!! 계정 Id : {loaded.Id}, PlayerUID : {loaded.PlayerUID}");

        account = loaded;

        return LoginResult.Success;
    }

    public void RequestSaveData(AccountData accountData)
    {
        if (accountData == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(accountData.PlayerUID))
        {
            LogError("PlayerUID가 없는 계정은 저장할 수 없습니다!!");

            return;
        }

        _accountFileService.Save(accountData);
    }

    private GameSaveData CreateNewSaveData()
    {
        GameSaveData saveData = new();

        saveData.PlayerData.OwnedSkins.Add("Hair_MediumBob_Beige");
        saveData.PlayerData.OwnedSkins.Add("Eyes_Black");
        saveData.PlayerData.OwnedSkins.Add("Eyebrows_Neutral");
        saveData.PlayerData.OwnedSkins.Add("Mouth_Empty");
        saveData.PlayerData.OwnedSkins.Add("Cloth_Nahida");
        saveData.PlayerData.OwnedSkins.Add("Body_Light");

        saveData.PlayerData.EquippedSkins.Add(new EquippedSkin(SkinCategory.Hair.ToString(), "Hair_MediumBob_Beige"));
        saveData.PlayerData.EquippedSkins.Add(new EquippedSkin(SkinCategory.Eyes.ToString(), "Eyes_Black"));
        saveData.PlayerData.EquippedSkins.Add(new EquippedSkin(SkinCategory.EyeBrows.ToString(), "Eyebrows_Neutral"));
        saveData.PlayerData.EquippedSkins.Add(new EquippedSkin(SkinCategory.Mouth.ToString(), "Mouth_Empty"));
        saveData.PlayerData.EquippedSkins.Add(new EquippedSkin(SkinCategory.Cloth.ToString(), "Cloth_Nahida"));
        saveData.PlayerData.EquippedSkins.Add(new EquippedSkin(SkinCategory.Body.ToString(), "Body_Light"));

        return saveData;
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class NetworkManager : BaseManager<NetworkManager>
{
    private string GetAccountDirectory()
    {
        string path = Path.Combine(Application.persistentDataPath, "Accounts");

        return path;
    }

    private string GetAccountPath(string id)
    {
        string path = Path.Combine(GetAccountDirectory(), $"{id}.json");

        return path;
    }

    public AccountResult TryCreateAccount(string id, string password, out AccountData account)
    {
        account = null;

        if (AccountUtil.ValidateId(id) != IdValidationResult.Valid)
        {
            return AccountResult.InvalidId;
        }

        if (AccountUtil.ValidatePassword(password) != PasswordValidationResult.Valid)
        {
            return AccountResult.InvalidPassword;
        }

        if (File.Exists(GetAccountPath(id)))
        {
            return AccountResult.AlreadyExists;
        }

        account = new AccountData();
        account.Id = id;
        account.PasswordHash = HashPassword(password);
        account.GameSaveData = CreateNewSaveData();

        Log($"새로운 계정을 생성하였습니다!! 계정 Id : {account.Id}");

        SaveAccount(account);

        return AccountResult.Success;
    }

    public AccountResult TryLogin(string id, string password, out AccountData account)
    {
        account = null;

        if (AccountUtil.ValidateId(id) != IdValidationResult.Valid)
        {
            return AccountResult.Failed;
        }

        if (AccountUtil.ValidatePassword(password) != PasswordValidationResult.Valid)
        {
            return AccountResult.Failed;
        }

        AccountData loaded = LoadAccount(id);

        if (loaded == null)
        {
            return AccountResult.Failed;
        }

        if (loaded.PasswordHash != HashPassword(password))
        {
            return AccountResult.Failed;
        }

        Log($"계정 로드 완료!! 계정 Id : {loaded.Id}");
        account = loaded;

        return AccountResult.Success;
    }

    public void RequestSaveData(AccountData accountData)
    {
        if(accountData == null)
        {
            return;
        }

        if(AccountUtil.ValidateId(accountData.Id) != IdValidationResult.Valid)
        {
            return;
        }

        SaveAccount(accountData);
    }

    private void SaveAccount(AccountData accountData)
    {
        Directory.CreateDirectory(GetAccountDirectory());

        string json = JsonUtility.ToJson(accountData, true);
        File.WriteAllText(GetAccountPath(accountData.Id), json);

        Log($"계정 저장 완료!! 아이디 명 : {accountData.Id}");
    }

    private AccountData LoadAccount(string id)
    {
        string path = GetAccountPath(id);
        
        if(File.Exists(path) == false)
        {
            return null;
        }

        try
        {
            string json = File.ReadAllText(path);

            AccountData accountData = JsonUtility.FromJson<AccountData>(json);

            if(accountData == null)
            {
                LogError($"계정 세이브 데이터의 파싱에 실패하였습니다!! 아이디 : {id}");
            }

            return accountData;
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);

            return null;
        }
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

    private string HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);

        string hashPassword = Convert.ToBase64String(hash);

        return hashPassword;
    }
}

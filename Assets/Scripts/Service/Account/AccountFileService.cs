using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AccountFileService : BaseService
{
    public string GetAccountDirectory()
    {
        string path = Path.Combine(Application.persistentDataPath, "Accounts");

        return path;
    }

    public string GetAccountPath(string playerUID)
    {
        string path = Path.Combine(GetAccountDirectory(), $"{playerUID}.json");

        return path;
    }

    public bool Exists(string playerUID)
    {
        bool isExists = File.Exists(GetAccountPath(playerUID));

        return isExists;
    }

    public AccountData Load(string playerUID)
    {
        string path = GetAccountPath(playerUID);

        if (File.Exists(path) == false)
        {
            LogError($"계정 파일을 찾을 수 없습니다!! PlayerUID : {playerUID}");

            return null;
        }

        AccountData accountData = ReadAccountFile(path);

        if (accountData == null)
        {
            LogError($"계정 세이브 데이터의 파싱에 실패하였습니다!! PlayerUID : {playerUID}");
        }

        return accountData;
    }

    public void Save(AccountData accountData)
    {
        Directory.CreateDirectory(GetAccountDirectory());

        string json = JsonUtility.ToJson(accountData, true);

        File.WriteAllText(GetAccountPath(accountData.PlayerUID), json);

        Log($"계정 저장 완료!! 계정 Id : {accountData.Id}, PlayerUID : {accountData.PlayerUID}");
    }

    public List<AccountData> LoadAll()
    {
        List<AccountData> accountDatas = new();

        string directory = GetAccountDirectory();

        if (Directory.Exists(directory) == false)
        {
            return accountDatas;
        }

        foreach (string path in Directory.GetFiles(directory, "*.json"))
        {
            AccountData accountData = ReadAccountFile(path);

            if (accountData == null)
            {
                continue;
            }

            if (string.IsNullOrEmpty(accountData.Id) || string.IsNullOrEmpty(accountData.PlayerUID))
            {
                continue;
            }

            accountDatas.Add(accountData);
        }

        return accountDatas;
    }

    private AccountData ReadAccountFile(string path)
    {
        try
        {
            string json = File.ReadAllText(path);

            AccountData accountData = JsonUtility.FromJson<AccountData>(json);

            return accountData;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);

            return null;
        }
    }
}

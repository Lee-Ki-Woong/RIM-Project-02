using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AccountIndexService : BaseService
{
    private const string AccountIndexFileName = "AccountIndex.json";
    private const int PlayerUidStartValue = 100000000;

    private readonly AccountFileService _accountFileService;

    private AccountIndex _accountIndex;

    public AccountIndexService(AccountFileService accountFileService)
    {
        _accountFileService = accountFileService;
    }

    public string FindPlayerUid(string loweredId)
    {
        AccountIndex index = GetAccountIndex();

        foreach (AccountIndexEntry entry in index.Entries)
        {
            if (entry.Id == loweredId)
            {
                return entry.PlayerUID;
            }
        }

        return null;
    }

    public string IssuePlayerUid()
    {
        AccountIndex index = GetAccountIndex();

        int maxUid = PlayerUidStartValue - 1;

        foreach (AccountIndexEntry entry in index.Entries)
        {
            if (int.TryParse(entry.PlayerUID, out int uid) == false)
            {
                continue;
            }

            if (uid > maxUid)
            {
                maxUid = uid;
            }
        }

        int nextUid = maxUid + 1;

        while (_accountFileService.Exists(nextUid.ToString()))
        {
            nextUid++;
        }

        return nextUid.ToString();
    }

    public void Register(string loweredId, string playerUID)
    {
        AccountIndex index = GetAccountIndex();

        index.Entries.Add(new AccountIndexEntry(loweredId, playerUID));

        SaveAccountIndex(index);
    }

    private string GetAccountIndexPath()
    {
        string path = Path.Combine(_accountFileService.GetAccountDirectory(), AccountIndexFileName);

        return path;
    }

    private AccountIndex GetAccountIndex()
    {
        if (_accountIndex == null)
        {
            _accountIndex = LoadAccountIndex();
        }

        return _accountIndex;
    }

    private AccountIndex LoadAccountIndex()
    {
        string path = GetAccountIndexPath();

        if (File.Exists(path) == false)
        {
            return RebuildAccountIndex();
        }

        try
        {
            string json = File.ReadAllText(path);

            AccountIndex index = JsonUtility.FromJson<AccountIndex>(json);

            if (index == null || index.Entries == null)
            {
                LogError("계정 색인 파싱에 실패하였습니다!! 계정 파일을 훑어 복구합니다!!");

                return RebuildAccountIndex();
            }

            return index;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);

            return RebuildAccountIndex();
        }
    }

    private void SaveAccountIndex(AccountIndex index)
    {
        Directory.CreateDirectory(_accountFileService.GetAccountDirectory());

        string json = JsonUtility.ToJson(index, true);

        File.WriteAllText(GetAccountIndexPath(), json);
    }

    private AccountIndex RebuildAccountIndex()
    {
        AccountIndex index = new();

        foreach (AccountData accountData in _accountFileService.LoadAll())
        {
            index.Entries.Add(new AccountIndexEntry(accountData.Id, accountData.PlayerUID));
        }

        SaveAccountIndex(index);

        Log($"계정 색인을 재구성하였습니다!! 계정 수 : {index.Entries.Count}");

        return index;
    }

    [Serializable]
    private class AccountIndex
    {
        public List<AccountIndexEntry> Entries = new();
    }

    [Serializable]
    private class AccountIndexEntry
    {
        public string Id;
        public string PlayerUID;

        public AccountIndexEntry() { }

        public AccountIndexEntry(string id, string playerUID)
        {
            Id = id;
            PlayerUID = playerUID;
        }
    }
}

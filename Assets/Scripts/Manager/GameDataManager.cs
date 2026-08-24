using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : BaseManager<GameDataManager>
{
    public Dictionary<SkinCategory, List<SkinDataBase>> SkinData { get; private set; } = new();

    protected override void InitAction()
    {
        LoadAllData();
    }

    public void ReloadAllData()
    {
        LoadAllData();
    }

    private void LoadAllData()
    {
        LoadSkinData();
    }

    private void LoadSkinData()
    {
        Dictionary<SkinCategory, List<SkinDataBase>> newSkinData = new();

        newSkinData[SkinCategory.Hair] = LoadDataInList<SkinDataBase>("SkinDataBase_Hair");
        newSkinData[SkinCategory.Eyes] = LoadDataInList<SkinDataBase>("SkinDataBase_Eyes");
        newSkinData[SkinCategory.EyeBrows] = LoadDataInList<SkinDataBase>("SkinDataBase_Eyebrows");
        newSkinData[SkinCategory.Mouth] = LoadDataInList<SkinDataBase>("SkinDataBase_Mouth");
        newSkinData[SkinCategory.Cloth] = LoadDataInList<SkinDataBase>("SkinDataBase_Cloth");
        newSkinData[SkinCategory.Body] = LoadDataInList<SkinDataBase>("SkinDataBase_Body");

        SkinData = newSkinData;
    }

    [Serializable]
    private class SerializableWrapper<T>
    {
        public List<T> _data;
    }

    private List<T> LoadDataInList<T>(string path) where T : GameDataBase
    {
        string language = LanguageUtil.GetLanguage(GameSettingManager.Instance.GameCurrentLanguage);
        string resourcePath = $"Json/{path}_{language}";
        TextAsset textAsset = ResourceManager.Instance.LoadAssetSync<TextAsset>(resourcePath);

        if (textAsset == null)
        {
            return new List<T>();
        }

        try
        {
            string wrapperData = "{\"_data\":" + textAsset.text + "}";
            SerializableWrapper<T> wrapper = JsonUtility.FromJson<SerializableWrapper<T>>(wrapperData);

            if (wrapper._data == null)
            {
                LogError($"{resourcePath}의 데이터가 없습니다 다시 확인해주세요!!");

                return new List<T>();
            }

            Debug.Log($"{resourcePath}의 데이터가 {wrapper._data.Count}만큼 로드 되었습니다!!");

            return wrapper._data;
        }
        catch (Exception e)
        {
            Debug.LogException(e);

            return new List<T>();
        }
        finally
        {
            ResourceManager.Instance.UnloadAsset(resourcePath);
        }
    }
}

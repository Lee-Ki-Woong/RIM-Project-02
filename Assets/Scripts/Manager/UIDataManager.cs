using System;
using System.Collections.Generic;
using UnityEngine;

public class UIDataManager : BaseManager<UIDataManager>
{
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

    }

    [Serializable]
    private class SerializableWrapper<T>
    {
        public List<T> m_data;
    }

    private Dictionary<string, T> LoadData<T>(string path) where T : UIDataBase
    {
        string language = LanguageUtil.GetLanguage(GameSettingManager.Instance.GameCurrentLanguage);
        string resourcePath = $"Json/{path}_{language}";
        TextAsset textAsset = ResourceManager.Instance.LoadAssetSync<TextAsset>(resourcePath);

        if (textAsset == null)
        {
            return new Dictionary<string, T>();
        }

        try
        {
            string jsonData = textAsset.text;
            string wrapperData = "{\"m_data\":" + jsonData + "}";
            SerializableWrapper<T> wrapper = JsonUtility.FromJson<SerializableWrapper<T>>(wrapperData);
            if (wrapper.m_data != null)
            {
                Debug.Log($"{resourcePath}의 데이터가 {wrapper.m_data.Count}만큼 로드 되었습니다!!");
                Dictionary<string, T> newDictionary = new(wrapper.m_data.Count);

                foreach (T data in wrapper.m_data)
                {
                    if(newDictionary.ContainsKey(data.Id))
                    {
                        this.LogWarning($"{resourcePath}에 중복된 Id가 있습니다!! 중복 Id: {data.Id}");
                        continue;
                    }

                    newDictionary.Add(data.Id, data);
                }
                return newDictionary;
            }
            else
            {
                this.LogError($"{resourcePath}의 데이터가 없습니다 다시 확인해주세요!!");

                return new Dictionary<string, T>();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return new Dictionary<string, T>();
        }
        finally
        {
            ResourceManager.Instance.UnloadAsset(resourcePath);
        }
    }
}

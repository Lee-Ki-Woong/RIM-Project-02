using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UIEntry
{
    public UIType UIType;
    public CanvasType CanvasType;
    public string PrefabAddress;
}

[CreateAssetMenu(fileName = "UICatalog", menuName = "ScriptableObject/UICatalog")]
public class UICatalog : ScriptableObject
{
    [SerializeField] private List<UIEntry> UICatalogList = new();

    private Dictionary<UIType, UIEntry> _uiCatalogDictionary;

    public UIEntry GetUIData(UIType uiType)
    {
        if (_uiCatalogDictionary == null)
        {
            CreateUIData();
        }

        if (_uiCatalogDictionary.TryGetValue(uiType, out UIEntry uiEntry) == false)
        {
            return null;
        }

        return uiEntry;
    }

    private void CreateUIData()
    {
        _uiCatalogDictionary = new Dictionary<UIType, UIEntry>(UICatalogList.Count);

        foreach (UIEntry data in UICatalogList)
        {
            if (_uiCatalogDictionary.ContainsKey(data.UIType))
            {
                Debug.LogError($"UICatalog에 중복된 UIType이 있습니다!! 중복된 키 : {data.UIType}");
                continue;
            }

            _uiCatalogDictionary.Add(data.UIType, data);
        }
    }
}
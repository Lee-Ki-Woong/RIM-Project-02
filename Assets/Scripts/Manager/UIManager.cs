using System.Collections.Generic;
using UnityEngine;

public partial class UIManager : BaseMonoManager<UIManager>
{
    [SerializeField] private Canvas Canvas_Main;
    [SerializeField] private Canvas Canvas_Popup;
    [SerializeField] private Canvas Canvas_Content;

    [SerializeField] private UICatalog SO_UICatalog;

    private readonly Dictionary<UIType, IUIView> _uiDictionary = new();
    private readonly HashSet<UIType> _activeUISet = new();

    public T GetUI<T>(UIType uiType) where T : MonoBehaviour, IUIView
    {
        if (_uiDictionary.TryGetValue(uiType, out IUIView existingView) == false)
        {
            this.LogWarning($"[{uiType}]에 알맞는 UI를 찾을 수 없었습니다!!");

            return null;
        }

        T ui = existingView as T;

        if (ui == null)
        {
            this.LogError($"[{uiType}]으로의 형변환에 실패하였습니다!!");

            return null;
        }

        return ui;
    }

    public void PreloadUI(UIType uiType)
    {
        CreateUI(uiType);
    }

    public void PreloadUI(IEnumerable<UIType> uiTypes)
    {
        foreach(UIType uiType in uiTypes)
        {
            CreateUI(uiType);
        }
    }

    private T OpenUI<T>(UIType uiType) where T : MonoBehaviour, IUIView
    {
        IUIView view = CreateUI(uiType);

        if (view == null)
        {
            return null;
        }

        T ui = view as T;

        if(ui == null)
        {
            this.LogError($"[{uiType}]으로의 형변환에 실패하였습니다!! 제네릭 타입을 확인하세요!!");
            return null;
        }

        ui.transform.SetAsLastSibling();

        if (_activeUISet.Add(uiType) == false)
        {
            return ui;
        }

        ui.Open();

        return ui;
    }

    private IUIView CreateUI(UIType uiType)
    {
        if (_uiDictionary.TryGetValue(uiType, out IUIView existingView))
        {
            return existingView;
        }

        UIEntry uiEntry = SO_UICatalog.GetUIData(uiType);

        if (uiEntry == null)
        {
            this.LogError($"{nameof(uiType)}에 대한 UIEntry가 없습니다!!");
            return null;
        }

        GameObject prefab = ResourceManager.Instance.LoadAssetSync<GameObject>(uiEntry.PrefabAddress);

        if (prefab == null)
        {
            return null;
        }

        Canvas canvas = GetCanvas(uiEntry.CanvasType);

        if (canvas == null)
        {
            return null;
        }

        GameObject uiGameObject = Instantiate(prefab, canvas.transform);

        IUIView view = uiGameObject.GetComponent<IUIView>();

        if (view == null)
        {
            this.LogError($"{nameof(prefab)}에 {typeof(IUIView)} 컴포넌트가 없습니다!!");
            Destroy(uiGameObject);

            return null;
        }

        uiGameObject.SetActive(false);
        _uiDictionary.Add(uiType, view);
        return view;
    }

    public bool TryCloseUI(UIType uiType)
    {
        if (_activeUISet.Contains(uiType) == false)
        {
            this.Log($"[UIType : {uiType}] 타입의 UI는 열려있지 않습니다!!");

            return false;
        }

        if (_uiDictionary.TryGetValue(uiType, out IUIView existingView) == false)
        {
            this.Log($"[UIType : {uiType}] 타입의 UI를 찾을 수 없습니다!!");

            return false;
        }

        existingView.Close();
        _activeUISet.Remove(uiType);
        
        return true;
    }

    private Canvas GetCanvas(CanvasType canvasType)
    {
        switch (canvasType)
        {
            case CanvasType.Main:
                {
                    return Canvas_Main;
                }
            case CanvasType.Popup:
                {
                    return Canvas_Popup;
                }
            case CanvasType.Content:
                {
                    return Canvas_Content;
                }
            default:
                {
                    this.LogError($"[{canvasType}]에 알맞는 Canvas가 없습니다!!");
                    
                    return null;
                }
        }
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button Button_Self;

    [SerializeField] private TMP_Text Text_SkinName;
    [SerializeField] private TMP_Text Text_SkinDescription;

    [SerializeField] private Image Image_SkinIcon;

    [SerializeField] private GameObject GameObject_SelectedIcon;
    [SerializeField] private GameObject GameObject_UnOwnedIcon;

    public string SkinId => _skin.Id;
    private SkinDataBase _skin;

    private bool _isIconLoaded;
    
    private Action _onClickButton;

    private void OnEnable()
    {
        Button_Self.onClick.AddListener(InvokeClickButton);
    }

    private void OnDisable()
    {
        Button_Self.onClick.RemoveListener(InvokeClickButton);
    }

    private void OnDestroy()
    {
        if(_isIconLoaded)
        {
            ResourceManager.Instance.UnloadAsset(_skin.SkinIconAddress);
        }
    }

    public void InitButton(SkinDataBase skin, bool isOwned, bool isSelected, Action onClickButtonCallback)
    {
        _skin = skin;
        Text_SkinName.text = _skin.SkinName;
        Text_SkinDescription.text = _skin.SkinDescription;

        Sprite iconSprite = ResourceManager.Instance.LoadAssetSync<Sprite>(_skin.SkinIconAddress);

        if(iconSprite != null)
        {
            Image_SkinIcon.sprite = iconSprite;
            _isIconLoaded = true;
        }

        GameObject_UnOwnedIcon.SetActive(isOwned == false);
        Button_Self.interactable = isOwned;

        SetSelectedButton(isSelected);

        _onClickButton = onClickButtonCallback;
    }

    public void SetSelectedButton(bool isSelected)
    {
       GameObject_SelectedIcon.SetActive(isSelected);
    }

    private void InvokeClickButton()
    {
        _onClickButton?.Invoke();
    }
}

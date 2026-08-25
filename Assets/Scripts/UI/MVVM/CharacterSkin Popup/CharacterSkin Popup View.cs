using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class CharacterSkinPopupView : BaseUIView<CharacterSkinPopupViewModel>
{
    [SerializeField] private SkeletonAnimation SkeletonAnimation_Preview;

    [Serializable]
    private class CategoryButton
    {
        public SkinCategory Category;
        public Button Button;
    }

    [SerializeField] private CategoryButton[] CategoryButtons;

    [SerializeField] private Transform Transform_SkinButtons;
    [SerializeField] private SkinButton SkinButtonPrefab;

    [SerializeField] private Button AcceptButton;
    [SerializeField] private Button CancelButton;

    private readonly List<SkinButton> _skinButtons = new();

    private void Awake()
    {
        foreach(CategoryButton categoryButton in CategoryButtons)
        {
            SkinCategory skinCategory = categoryButton.Category;

            UnityAction onClickButton = () => _viewModel.SelectCategory(skinCategory);

            categoryButton.Button.onClick.AddListener(onClickButton);
        }

        AcceptButton.onClick.AddListener(OnAcceptButtonClicked);
        CancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        switch(propertyName)
        {
            case nameof(CharacterSkinPopupModel.CurrentCategory):
                {
                    RefreshSkinButtons(_viewModel.CurrentCategory);
                }
                break;
            case nameof(CharacterSkinPopupModel.EquippedSkins):
                {
                    ApplyPreviewSkin();
                    RefreshButtonHighlight();
                }
                break;
        }
    }

    private void RefreshSkinButtons(SkinCategory category)
    {
        foreach (SkinButton skinButton in _skinButtons)
        {
            Destroy(skinButton.gameObject);
        }

        _skinButtons.Clear();

        IReadOnlyList<SkinDataBase> skins = _viewModel.GetSkins(category);

        _viewModel.EquippedSkins.TryGetValue(category, out string selectedSkinId);

        foreach (SkinDataBase skin in skins)
        {
            SkinButton skinButton = Instantiate(SkinButtonPrefab, Transform_SkinButtons);

            bool isOwned = _viewModel.IsOwned(skin.Id);
            bool isSelected = (skin.Id == selectedSkinId);

            Action onClickButton = () => { _viewModel.SelectSkin(skin.Id); };

            skinButton.InitButton(skin, isOwned, isSelected, onClickButton);

            _skinButtons.Add(skinButton);
        }
    }

    private void ApplyPreviewSkin()
    {
        string[] skinNames = _viewModel.EquippedSkins.Values.ToArray();
        SkinCombiner.ApplyCombinedSkin(SkeletonAnimation_Preview, skinNames);
    }

    private void RefreshButtonHighlight()
    {
        _viewModel.EquippedSkins.TryGetValue(_viewModel.CurrentCategory, out string selectedSkinId);

        foreach (SkinButton button in _skinButtons)
        {
            bool isSelectedButton = (button.SkinId == selectedSkinId);

            button.SetSelectedButton(isSelectedButton);
        }
    }

    private void OnAcceptButtonClicked()
    {
        _viewModel.Accept();
        UIManager.Instance.TryCloseUI(UIType.CharacterSkinPopup);
    }

    private void OnCancelButtonClicked()
    {
        _viewModel.Cancel();
        UIManager.Instance.TryCloseUI(UIType.CharacterSkinPopup);
    }
}

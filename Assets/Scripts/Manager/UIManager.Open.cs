using System;
using System.Collections.Generic;

public partial class UIManager 
{
    public void OpenTitleBackgroundUI()
    {
        OpenUI<TitleBackgroundUI>(UIType.TitleBackgroundUI);
    }

    public void OpenTitleButtonUI()
    {
        OpenUI<TitleButtonUI>(UIType.TitleButtonUI);
    }

    public void OpenPlayerUID()
    {
        OpenUI<PlayerUID>(UIType.PlayerUID);
    }

    public void OpenLoginUI()
    {
        LoginUIView view = OpenUI<LoginUIView>(UIType.LoginUI);
        if (view == null) return;

        LoginUIModel model = new(UIDataManager.Instance.LoginUIData);
        LoginUIViewModel viewModel = new(model);

        view.BindViewModel(viewModel);
    }

    public void OpenCharacterSkinPopup(Action<IReadOnlyDictionary<SkinCategory, string>> applyToPlayer)
    {
        CharacterSkinPopupView view = OpenUI<CharacterSkinPopupView>(UIType.CharacterSkinPopup);

        if (view == null)
        {
            return;
        }

        SkinService skinService = GameManager.Instance.PlayerService.SkinService;

        IReadOnlyDictionary<SkinCategory, List<SkinDataBase>> skinCatalog = skinService.GetSkinCatalog();
        Dictionary<SkinCategory, string> equippedSkins = new(skinService.GetEquippedSkins());
        HashSet<string> ownedSkins = new(skinService.GetOwnedSkins());

        CharacterSkinPopupModel model = new(SkinCategory.Hair, skinCatalog, equippedSkins, ownedSkins);
        CharacterSkinPopupViewModel viewModel = new(model, applyToPlayer);

        view.BindViewModel(viewModel);
    }
}
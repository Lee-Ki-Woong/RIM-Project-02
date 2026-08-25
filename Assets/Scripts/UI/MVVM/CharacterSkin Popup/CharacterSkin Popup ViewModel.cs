using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterSkinPopupViewModel : BaseViewModel<CharacterSkinPopupModel>
{
    private readonly Action<IReadOnlyDictionary<SkinCategory, string>> _applyEquippedSkinToPlayerCharacter;

    public SkinCategory CurrentCategory => _model.CurrentCategory;
    public IReadOnlyDictionary<SkinCategory, string> EquippedSkins => _model.EquippedSkins;

    public CharacterSkinPopupViewModel(CharacterSkinPopupModel model, Action<IReadOnlyDictionary<SkinCategory, string>> applyEquippedSkinToPlayerCharacter) : base(model)
    {
        _applyEquippedSkinToPlayerCharacter = applyEquippedSkinToPlayerCharacter;
    }

    public IReadOnlyList<SkinDataBase> GetSkins(SkinCategory category)
    {
        if (_model.SkinCatalog.TryGetValue(category, out List<SkinDataBase> skins) == false)
        {
            return Array.Empty<SkinDataBase>();
        }

        return skins;
    }

    public void SelectSkin(string skinId)
    {
        if(IsOwned(skinId) == false)
        {
            return;
        }

        SkinCategory category = _model.CurrentCategory;

        Dictionary<SkinCategory, string> next = new(_model.EquippedSkins);
        next[category] = skinId;
        _model.EquippedSkins = next;
    }

    public void SelectCategory(SkinCategory category)
    {
        _model.CurrentCategory = category;
    }

    public bool IsOwned(string skinId)
    {
        bool isOwned = _model.OwnedSkins.Contains(skinId);

        return isOwned;
    }

    public void Accept()
    {
        _applyEquippedSkinToPlayerCharacter?.Invoke(_model.EquippedSkins);
    }

    public void Cancel()
    {
    }
}

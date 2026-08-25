using System.Collections.Generic;

public class CharacterSkinPopupModel : BaseModel
{
    public override void PropertyChangedOnInit()
    {
        OnPropertyChanged(nameof(CurrentCategory));
        OnPropertyChanged(nameof(EquippedSkins));
    }

    public CharacterSkinPopupModel(SkinCategory currentCategory, IReadOnlyDictionary<SkinCategory, List<SkinDataBase>> skinCatalog, IReadOnlyDictionary<SkinCategory, string> equippedSkins, IReadOnlyCollection<string> ownedSkins)
    {
        _currentCategory = currentCategory;
        _skinCatalog = skinCatalog;
        _equippedSkins = equippedSkins;
        _ownedSkins = ownedSkins;
    }

    private SkinCategory _currentCategory;
    public SkinCategory CurrentCategory
    {
        get => _currentCategory;
        set
        {
            if (_currentCategory != value)
            {
                _currentCategory = value;
                OnPropertyChanged();
            }
        }
    }

    private IReadOnlyDictionary<SkinCategory, string> _equippedSkins;
    public IReadOnlyDictionary<SkinCategory, string> EquippedSkins
    {
        get => _equippedSkins;
        set
        {
            if (_equippedSkins != value)
            {
                _equippedSkins = value;
                OnPropertyChanged();
            }
        }
    }

    private IReadOnlyCollection<string> _ownedSkins;
    public IReadOnlyCollection<string> OwnedSkins
    {
        get => _ownedSkins;
    }

    private IReadOnlyDictionary<SkinCategory, List<SkinDataBase>> _skinCatalog;
    public IReadOnlyDictionary<SkinCategory, List<SkinDataBase>> SkinCatalog
    {
        get => _skinCatalog;
    }
}

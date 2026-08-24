using System;
using System.Collections.Generic;
using System.Linq;

public class SkinService : BaseService
{
    private readonly PlayerModel _playerModel;

    public SkinService(PlayerModel playerModel)
    {
        _playerModel = playerModel;
    }

    public IReadOnlyDictionary<SkinCategory, List<SkinDataBase>> GetSkinCatalog()
    {
        return GameDataManager.Instance.SkinData;
    }

    public IReadOnlyDictionary<SkinCategory, string> GetEquippedSkins()
    {
        return _playerModel.PlayerEquippedSkins;
    }

    public IReadOnlyCollection<string> GetOwnedSkins()
    {
        return _playerModel.PlayerOwnedSkins;
    }

    public bool IsOwned(string skinId)
    {
        return _playerModel.PlayerOwnedSkins.Contains(skinId);
    }

    public void SetEquippedSkins(IReadOnlyDictionary<SkinCategory, string> equippedSkins)
    {
        _playerModel.PlayerEquippedSkins = equippedSkins;
        GameManager.Instance.SaveAccount();
    }
}

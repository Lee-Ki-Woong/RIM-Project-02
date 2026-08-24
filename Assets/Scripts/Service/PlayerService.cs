using System.Collections.Generic;

public class PlayerService : BaseService
{
    public PlayerModel Model { get; }
    public SkinService SkinService { get; }

    public PlayerService(PlayerData playerData)
    {
        Model = new(playerData);
        SkinService = new(Model);
    }

    public void ApplyToData(PlayerData playerData)
    {
        playerData.PlayerName = Model.PlayerName;
        playerData.PlayerMoveSpeed = Model.PlayerMoveSpeed;
        playerData.PlayerAttackDamage = Model.PlayerAttackDamage;
        playerData.PlayerMaxHp = Model.PlayerMaxHp;
        playerData.PlayerCurrentHp = Model.PlayerCurrentHp;

        playerData.OwnedSkins = new List<string>(Model.PlayerOwnedSkins);
        playerData.SetEquippedSkins(Model.PlayerEquippedSkins);
    }
}

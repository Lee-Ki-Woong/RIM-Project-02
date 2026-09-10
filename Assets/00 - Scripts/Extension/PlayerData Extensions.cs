using System;
using System.Collections.Generic;

public static class PlayerDataExtensions
{
    public static Dictionary<SkinCategory, string> GetEquippedSkins(this PlayerData playerData)
    {
        Dictionary<SkinCategory, string> dictionary = new();

        foreach(EquippedSkin equippedSkin in playerData.EquippedSkins)
        {
            if(Enum.TryParse(equippedSkin.Category, out SkinCategory category))
            {
                dictionary[category] = equippedSkin.SkinId;
            }
        }

        return dictionary;
    }

    public static void SetEquippedSkins(this PlayerData playerData, IReadOnlyDictionary<SkinCategory, string> dictionary)
    {
        playerData.EquippedSkins = new();

        foreach(KeyValuePair<SkinCategory, string> keyValuePair in dictionary)
        {
            string category = keyValuePair.Key.ToString();
            string skinId = keyValuePair.Value;

            EquippedSkin equippedSkin = new EquippedSkin(category, skinId);

            playerData.EquippedSkins.Add(equippedSkin);
        }
    }
}

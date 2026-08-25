using TMPro;
using UnityEngine;

public class PlayerUIDUI : BaseSimpleUIView
{
    [SerializeField] private TMP_Text Text_PlayerUID;

    public void SetPlayerUID(string playerUID)
    {
        Text_PlayerUID.text = $"UID : {playerUID}";
    }
}

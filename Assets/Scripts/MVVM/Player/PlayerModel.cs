using System.Collections.Generic;

public class PlayerModel : BaseModel
{
    public override void PropertyChangedOnInit()
    {
        OnPropertyChanged(nameof(PlayerName));
        OnPropertyChanged(nameof(PlayerMoveSpeed));
        OnPropertyChanged(nameof(PlayerAttackDamage));
        OnPropertyChanged(nameof(PlayerMaxHp));
        OnPropertyChanged(nameof(PlayerCurrentHp));
        OnPropertyChanged(nameof(PlayerOwnedSkins));
        OnPropertyChanged(nameof(PlayerEquippedSkins));
    }

    public PlayerModel(PlayerData playerData)
    {
        _playerName = playerData.PlayerName;
        _playerMoveSpeed = playerData.PlayerMoveSpeed;
        _playerAttackDamage = playerData.PlayerAttackDamage;
        _playerMaxHp = playerData.PlayerMaxHp;
        _playerCurrentHp = playerData.PlayerCurrentHp;

        _playerOwnedSkins = new List<string>(playerData.OwnedSkins);
        _playerEquippedSkins = playerData.GetEquippedSkins();
    }

    private string _playerName;
    public string PlayerName
    {
        get => _playerName;
        set
        {
            if(_playerName != value)
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }
    }

    private float _playerMoveSpeed;
    public float PlayerMoveSpeed
    {
        get => _playerMoveSpeed;
        set
        {
            if (_playerMoveSpeed != value)
            {
                _playerMoveSpeed = value;
                OnPropertyChanged();
            }
        }
    }

    private int _playerAttackDamage;
    public int PlayerAttackDamage
    {
        get => _playerAttackDamage;
        set
        {
            if (_playerAttackDamage != value)
            {
                _playerAttackDamage = value;
                OnPropertyChanged();
            }
        }
    }

    private int _playerMaxHp;
    public int PlayerMaxHp
    {
        get => _playerMaxHp;
        set
        {
            if (_playerMaxHp != value)
            {
                _playerMaxHp = value;
                OnPropertyChanged();
            }
        }
    }

    private int _playerCurrentHp;
    public int PlayerCurrentHp
    {
        get => _playerCurrentHp;
        set
        {
            if (_playerCurrentHp != value)
            {
                _playerCurrentHp = value;
                OnPropertyChanged();
            }
        }
    }

    private IReadOnlyList<string> _playerOwnedSkins;
    public IReadOnlyList<string> PlayerOwnedSkins
    {
        get => _playerOwnedSkins;
        set
        {
            if(_playerOwnedSkins != value)
            {
                _playerOwnedSkins = value;
                OnPropertyChanged();
            }
        }
    }

    private IReadOnlyDictionary<SkinCategory, string> _playerEquippedSkins;
    public IReadOnlyDictionary<SkinCategory, string> PlayerEquippedSkins
    {
        get => _playerEquippedSkins;
        set
        {
            if(_playerEquippedSkins != value)
            {
                _playerEquippedSkins = value;
                OnPropertyChanged();
            }
        }
    }
}

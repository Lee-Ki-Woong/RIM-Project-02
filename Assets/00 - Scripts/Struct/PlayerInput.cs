using UnityEngine;

public readonly struct PlayerInput
{
    public readonly Vector2 MoveInput;
    public readonly SkillCategory PressedSkillCategory;

    public PlayerInput(Vector2 moveInput, SkillCategory pressedSkillSlot)
    {
        MoveInput = moveInput;
        PressedSkillCategory = pressedSkillSlot;
    }

    public bool HasSkillInput => PressedSkillCategory != SkillCategory.None;
}
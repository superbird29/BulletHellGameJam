using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame;

/// <summary>
/// Holds the information of the card that the dictionary will read
/// </summary>

[CreateAssetMenu(fileName = "New Card", menuName = "Card System/Card")]
public class Card : ScriptableObject
{
    // Name of the Card
    [SerializeField] public string cardName;
    // What the card does i.e. "Gain a shield, Shoot at 45 degree angle"
    [SerializeField] public string description;
    // The list of effects that card can do
    [SerializeField] public List<CardEffect> effects;
}

[System.Serializable]
public class WeaponToggleData
{
    public WeaponType weaponType;
    public bool isActive;
}

[System.Serializable]
public class CardEffect
{
    public CardEffectCategory category;
    public CardEffectType effectType; //i.e. GainShield, ModifyBulletAngle
    public int value;   //i.e. 1 shield, 45 degrees

    // Only used if category == Weapon
    public WeaponType weaponType;
    public bool isActive;
}


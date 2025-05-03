using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame;

// Author: Declin Anderson
// May 2, 2025

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

// Handles the information for if the card effect is based on changing a weapon
[System.Serializable]
public class WeaponToggleData
{
    public WeaponType weaponType; // The weapon type i.e. ice beam, bullet, sword
    public bool isActive; // If the weapon is being set to active
}

// Handles the information for the card effects
[System.Serializable]
public class CardEffect
{
    public CardEffectCategory category; // The category the effect belongs to i.e. Player, Projectile, Weapon
    public CardEffectType effectType; //i.e. GainShield, ModifyBulletAngle
    public int value;   //i.e. 1 shield, 45 degrees

    // Only used if category == Weapon
    public WeaponType weaponType; // The weapon type
    public bool isActive; // If the weapon is active
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the enum for what the dictionary will read for the card effects
/// </summary>
public class CardEnums : MonoBehaviour
{
}

namespace CardGame
{
    public enum CardEffectCategory
    {
        Player,
        Projectile,
        Card,
        Weapon
    }

    // Card Effects
    public enum CardEffectType
    {
        // Player
        GainShield,
        Heal,
        TakeDamage,

        //Projectile
        ModifyBulletAngle,
        ModifyBulletSpeed,
        ModifyBulletSize,

        // Weapon
        ToggleWeapon,
        
        // Card
        Draw,
        Discard
    }

    public enum WeaponType{
        //Projectile Types
        IceBeam,
        ChainLightning,
        HomingRocket,
        Sword,
    }
}

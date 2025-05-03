using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Declin Anderson
// May 2, 2025

/// <summary>
/// Holds the enum for what the dictionary will read for the card effects
/// </summary>
namespace CardGame
{
    // The types of card effects i.e. affects the player or projectiles shot or weapon
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

    // The types of weapon that the player could potentially use
    public enum WeaponType{
        IceBeam,
        ChainLightning,
        FireBall,
        Sword,
        Bullet
    }
}

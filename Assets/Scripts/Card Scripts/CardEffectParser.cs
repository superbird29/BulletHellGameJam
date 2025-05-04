using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame;

// Author: Declin Anderson
// May 2, 2025

/// <summary>
/// Reads the effects of the card and creates results based on that
/// </summary>
public class CardEffectParser : MonoBehaviour
{
    //[SerializeField] private ProjectileModifier projectileModifier;
    //[SerializeField] private PlayerStats playerStats; // Replace with your player/stat handler

    // Creating an instance for singleton
    public static CardEffectParser Instance { get; private set;}

    // Doing the singleton behavior
    private void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// When a card is chosen it executes the events tied to the cards
    /// </summary>
    /// <param name="card"> The card selected</param>
    public void ExecuteCard(Card card)
    {
        // Goes through each of the effects on the card and activates it
        foreach (CardEffect effect in card.effects)
        {
            ExecuteEffect(effect);
        }
    }

    /// <summary>
    /// Checks what effect is triggered and does the relevant actions
    /// </summary>
    /// <param name="effect">The current effect being read</param>
    private void ExecuteEffect(CardEffect effect)
    {
        // The type of effect the current card effect is
        switch (effect.effectType)
        {
            // If the Player is drawing a card
            case CardEffectType.Draw:
                GameManager.Instance._DeckManager.DrawCard(effect.value, false);
                break;
            case CardEffectType.HandSizeIncrease:
                GameManager.Instance._DeckManager.IncreaseHandSize(effect.value);
                break;
            // If the Player is healing
            case CardEffectType.Heal:
                GameManager.Instance._PlayerManager.ChangeLife(effect.value);
                break;
            // If the Player is Gaining shields
            case CardEffectType.GainShield:
                GameManager.Instance._PlayerManager.GainShield(effect.value);
                break;
            // If the player is gaining bombs
            case CardEffectType.GainBomb:
                GameManager.Instance._PlayerManager.GainBomb(effect.value);
                break;
            // If the player is gaining blanks
            case CardEffectType.GainBlank:
                GameManager.Instance._PlayerManager.GainBlank(effect.value);
                break;
            // If the player is shooting bullets from new angles
            case CardEffectType.BulletDamage:
                ModifyDamage(effect.value);
                break;
            // If the player is shooting bullets faster
            case CardEffectType.FasterFirerate:
                ModifySpeed(effect.value);
                break;
            // If the player is shooting bigger or smaller bullets
            case CardEffectType.BulletSize:
                ModifySize(effect.value);
                break;
            // If the player is changing what weapon they are using
            case CardEffectType.ToggleWeapon:
                ToggleWeapon(effect.weaponType.ToString(), effect.isActive);
                break;
            // A unrecognized effect was done by the card
            default:
                Debug.LogWarning($"Unrecognized effect type: {effect.effectType}");
                break;
        }
    }

    // These are temporary functions they will be replaced with relavent functions just help with testing
    private void DrawCards(int amount) => Debug.Log($"Draw {amount} cards");
    private void GainShield(int amount) => Debug.Log($"Gain {amount} shields");
    private void GainBlank(int amount) => Debug.Log($"Gain {amount} of Blanks");
    private void GainBomb(int amount) => Debug.Log($"Gain {amount} of bombs");
    private void Heal(int amount) => Debug.Log($"Heal {amount} health");
    private void ModifyDamage(int amount) => Debug.Log($"Change bullet damage by {amount} times");
    private void ModifySpeed(int amount) => Debug.Log($"Change bullet speed by {amount} times");
    private void ModifySize(int amount) => Debug.Log($"Change bullet size by {amount} times");
    private void ToggleWeapon(string name, bool isActive) => Debug.Log($"Your weapon changed to {name} and it is set to {isActive}");
}

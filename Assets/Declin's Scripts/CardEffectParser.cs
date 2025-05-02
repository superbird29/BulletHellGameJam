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
    [SerializeField] public Card[] cards; //Test cards

    public void Start()
    {
        for(int i  = 0; i < cards.Length; i++)
        {
            ExecuteCard(cards[i]);
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
                DrawCards(effect.value);
                break;
            // If the Player is healing
            case CardEffectType.Heal:
                Heal(effect.value);
                break;
            // If the Player is Gaining shields
            case CardEffectType.GainShield:
                GainShield(effect.value);
                break;
            // If the player is shooting bullets from new angles
            case CardEffectType.ModifyBulletAngle:
                ModifyAngle(effect.value);
                break;
            // If the player is shooting bullets faster
            case CardEffectType.ModifyBulletSpeed:
                ModifySpeed(effect.value);
                break;
            // If the player is shooting bigger or smaller bullets
            case CardEffectType.ModifyBulletSize:
                ModifySize(effect.value);
                break;
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
    private void Heal(int amount) => Debug.Log($"Heal {amount} health");
    private void ModifyAngle(int amount) => Debug.Log($"Change bullet angle by {amount} degrees");
    private void ModifySpeed(int amount) => Debug.Log($"Change bullet speed by {amount} times");
    private void ModifySize(int amount) => Debug.Log($"Change bullet size by {amount} times");
    private void ToggleWeapon(string name, bool isActive) => Debug.Log($"Your weapon changed to {name} and it is set to {isActive}");
}

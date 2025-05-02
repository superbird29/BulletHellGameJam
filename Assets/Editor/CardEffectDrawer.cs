using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using CardGame;

/// <summary>
/// Handles the logic of the card generator to be more user friendly
/// </summary>
[CustomPropertyDrawer(typeof(CardEffect))]
public class CardEffectDrawer : PropertyDrawer
{
    /// <summary>
    /// Handles generating the GUI for the card asset in the inspector
    /// </summary>
    /// <param name="position">Where it will be generated</param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Starts the UI Block for handling the layout
        EditorGUI.BeginProperty(position, label, property);
        float lineHeight = EditorGUIUtility.singleLineHeight + 2;

        // Calculates where the field will be drawn so that they don't overlap
        Rect categoryRect = new Rect(position.x, position.y, position.width, lineHeight);
        Rect effectRect = new Rect(position.x, position.y + lineHeight, position.width, lineHeight);
        Rect valueRect = new Rect(position.x, position.y + lineHeight * 2, position.width, lineHeight);

        // Draw category dropdown
        var categoryProp = property.FindPropertyRelative("category");
        EditorGUI.PropertyField(categoryRect, categoryProp);

        // Filter effectType options based on selected category and then creates the dropdown
        var effectProp = property.FindPropertyRelative("effectType");
        CardEffectCategory category = (CardEffectCategory)categoryProp.enumValueIndex;
        CardEffectType[] filteredOptions = GetFilteredOptions(category);

        string[] displayNames = filteredOptions.Select(e => e.ToString()).ToArray();
        int currentIndex = Array.IndexOf(filteredOptions, (CardEffectType)effectProp.enumValueIndex);
        if (currentIndex == -1) currentIndex = 0;

        int newIndex = EditorGUI.Popup(effectRect, "Effect Type", currentIndex, displayNames);
        effectProp.enumValueIndex = (int)filteredOptions[newIndex];


        // Show weapon toggles only if effect type is weapon-related
        if ((CardEffectCategory)categoryProp.enumValueIndex == CardEffectCategory.Weapon)
        {
            var weaponTypeProp = property.FindPropertyRelative("weaponType");
            var isActiveProp = property.FindPropertyRelative("isActive");

            Rect weaponRect = new Rect(position.x, position.y + lineHeight * 2, position.width, lineHeight);
            Rect toggleRect = new Rect(position.x, position.y + lineHeight * 3, position.width, lineHeight);

            EditorGUI.PropertyField(weaponRect, weaponTypeProp);
            EditorGUI.PropertyField(toggleRect, isActiveProp);
        }
        else{
            // Property field for the value of the card
            var valueProp = property.FindPropertyRelative("value");
            EditorGUI.PropertyField(valueRect, valueProp);
        }
        
        // Closes the property block
        EditorGUI.EndProperty();
    }

    /// <summary>
    /// Tells how much height there will need to be for the inspector to create
    /// </summary>
    /// <param name="property">The property that is being used</param>
    /// <param name="label">The name of the GUI Content</param>
    /// <returns>the amount of vertical space needed to be held</returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
       var category = (CardEffectCategory)property.FindPropertyRelative("category").enumValueIndex;

        int lines = 3; // base: category + effectType

        if(category == CardEffectCategory.Weapon)
        {
            lines +=1;
        }

        return lines * (EditorGUIUtility.singleLineHeight + 2);
    }

    /// <summary>
    /// Returns the Categories that are in the cards
    /// </summary>
    /// <param name="category">The category that is currently being checked</param>
    /// <returns>The different options</returns>
    private CardEffectType[] GetFilteredOptions(CardEffectCategory category)
    {
        switch (category)
        {
            // If Player was selected get the enums for player based actions
            case CardEffectCategory.Player:
                return new[] { CardEffectType.Heal, CardEffectType.GainShield, CardEffectType.TakeDamage };
            // If Projectile was selected get the enums for projectile settings
            case CardEffectCategory.Projectile:
                return new[] { CardEffectType.ModifyBulletAngle, CardEffectType.ModifyBulletSpeed, CardEffectType.ModifyBulletSize };
            // If Card was selected get the enums for Card actions
            case CardEffectCategory.Card:
                return new[] { CardEffectType.Draw, CardEffectType.Discard};
            case CardEffectCategory.Weapon:
                return new[] { CardEffectType.ToggleWeapon};
            default:
                return Enum.GetValues(typeof(CardEffectType)).Cast<CardEffectType>().ToArray();
        }
    }
}

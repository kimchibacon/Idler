//=============================================================================
// UpgradeTemplate.cs
//
// ScriptableObject data class for Upgrade types. 
//=============================================================================

using UnityEngine;

namespace Idler.Modifiers.Templates
{
    //=========================================================================
    // UpgradeTemplate
    //=========================================================================
    [CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Attribute Upgrade")]
    public class UpgradeTemplate : ModifierTemplateBase
    {
        public int maxLevel;
        public float startingCost;
        public float costMultiplier;
    }
}
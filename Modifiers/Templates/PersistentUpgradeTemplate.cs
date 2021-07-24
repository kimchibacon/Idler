//=============================================================================
// PersistentUpgradeTemplate.cs
//
// ScriptableObject data class for PersistenUpgrade types. 
//=============================================================================

using UnityEngine;

namespace Idler.Modifiers.Templates
{
    //=========================================================================
    // PersistentUpgradeTemplate
    //=========================================================================
    [CreateAssetMenu(fileName = "PersistentUpgrade", menuName = "Scriptable Objects/Persistent Upgrade")]
    public class PersistentUpgradeTemplate : ModifierTemplateBase
    {
        public int maxLevel;
        public float startingCost;
        public float costMultiplier;
    }
}

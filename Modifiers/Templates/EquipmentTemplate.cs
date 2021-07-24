//=============================================================================
// EquipmentTemplate.cs
//
// ScriptableObject data class for Equipment Modifiers. 
//=============================================================================

using UnityEngine;

namespace Idler.Modifiers.Templates
{
    //=========================================================================
    // EquipmentTemplate
    //=========================================================================
    [CreateAssetMenu(fileName = "Equipment", menuName = "Scriptable Objects/Equipment")]
    public class EquipmentTemplate : ModifierTemplateBase
    {
        public float cost;
        public string[] slots;
    }
}
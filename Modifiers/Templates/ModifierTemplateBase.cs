//=============================================================================
// ModifierTemplateBase.cs
//
// ScriptableObject data class for Modifier types. 
//=============================================================================

using Idler.CustomEditors;
using UnityEngine;

namespace Idler.Modifiers.Templates
{
    //=========================================================================
    // ModifierTemplateBase
    //=========================================================================
    public abstract class ModifierTemplateBase : ScriptableObject
    {
        [ReadOnlySerializable] public string resourceId;
        public string description;
        public Attributes.Templates.AttributeTemplate[] affectedAttributes;
        public float[] attributeModifierValues;
        public Operation[] upgradeOperations;
        public Texture2D icon;
    }
}
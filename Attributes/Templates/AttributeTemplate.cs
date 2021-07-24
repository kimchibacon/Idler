//=============================================================================
// AttributeTemplate.cs
//
// ScriptableObject data class for Attribute types. 
//=============================================================================

using System;
using Idler.CustomEditors;
using UnityEngine;

namespace Idler.Attributes.Templates
{
    [CreateAssetMenu(fileName = "AttributeTemplate", menuName = "Scriptable Objects/Attribute")]
    [Serializable]
    public class AttributeTemplate : ScriptableObject
    {
        [ReadOnlySerializable] public string resourceId;
        public float initialValue;
        public ValueType valueType;
    }
}

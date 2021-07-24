//=============================================================================
// ModifierBase.cs
//
// A base class for all objects that can modify Attributes.
//=============================================================================

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Idler.Modifiers
{
    //=========================================================================
    // ModifierBase
    //=========================================================================
    public abstract class ModifierBase
    {
        public readonly string name;
        public readonly string description;
        public readonly Dictionary<string, ModifierInstruction> modInstructions;
        public readonly Texture2D icon;

        //=========================================================================
        //=========================================================================
        protected ModifierBase(Templates.ModifierTemplateBase template)
        {
            name = template.name;
            description = template.description;
            modInstructions = new Dictionary<string, ModifierInstruction>();

            Assert.IsTrue(template.affectedAttributes.Length == template.attributeModifierValues.Length &&
                          template.affectedAttributes.Length == template.upgradeOperations.Length,
                $"Modifier '{name}' has mismatched number of Affected Attributes, Operations and Modifier Values");
            
            for (var i = 0; i < template.affectedAttributes.Length; ++i)
            {
                var instruction = new ModifierInstruction(template.attributeModifierValues[i], template.upgradeOperations[i]);
                modInstructions.Add(template.affectedAttributes[i].resourceId, instruction);
            }

            icon = template.icon;
        }
    }

    //=========================================================================
    // ModifierInstruction
    //=========================================================================
    public class ModifierInstruction
    {
        public readonly float value;
        public readonly Operation operation;

        //=========================================================================
        //=========================================================================
        public ModifierInstruction(float value, Operation operation)
        {
            this.value = value;
            this.operation = operation;
        }
    }
}
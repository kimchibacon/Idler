//=============================================================================
// CharacterBase.cs
//
// Character base class for entity that can have Attributes and Modifiers.
//=============================================================================

using System.Collections.Generic;
using Idler.Resources;
using UnityEngine;

namespace Idler.Characters
{
    //=========================================================================
    // CharacterBase
    //=========================================================================
    public abstract class CharacterBase : MonoBehaviour
    {
        protected Dictionary<string, Attributes.Attribute> attributes;
        protected Dictionary<string, Modifiers.Upgrade> upgrades;
        protected Dictionary<string, Modifiers.PersistentUpgrade> persistentUpgrades;
        protected Dictionary<string, Modifiers.Equipment> equipment;

        protected virtual void Start()
        {
            attributes = new Dictionary<string, Attributes.Attribute>();
            upgrades = new Dictionary<string, Modifiers.Upgrade>();
            persistentUpgrades = new Dictionary<string, Modifiers.PersistentUpgrade>();
            
            // TODO: Create "inventory" and "slot" types for character, store/apply equipment there
            equipment = new Dictionary<string, Modifiers.Equipment>();
        }

        //=========================================================================
        //=========================================================================
        protected void ApplyAttribute(string rid)
        {
            if (attributes.TryGetValue(rid, out var attribute))
            {
                return;
            }

            attribute = ResourceLibrary.Instance.CreateAttribute(rid);
            attributes.Add(rid, attribute);
        }
        
        //=========================================================================
        //=========================================================================
        protected void RemoveAttribute(string rid)
        {
            attributes.Remove(rid);
        }

        //=========================================================================
        //=========================================================================
        protected void ApplyUpgrade(string rid)
        {
            if (upgrades.TryGetValue(rid, out var upgrade))
            {
                upgrade.level++;
            }
            else
            {
                upgrade = ResourceLibrary.Instance.CreateUpgrade(rid);
                upgrades.Add(rid, upgrade);
            }
        }
        
        //=========================================================================
        //=========================================================================
        protected void RemoveUpgrade(string rid)
        {
            upgrades.Remove(rid);
        }
        
        //=========================================================================
        //=========================================================================
        protected void ApplyPersistentUpgrade(string rid)
        {
            if (persistentUpgrades.TryGetValue(rid, out var upgrade))
            {
                upgrade.level++;
            }
            else
            {
                upgrade = ResourceLibrary.Instance.CreatePersistentUpgrade(rid);
                persistentUpgrades.Add(rid, upgrade);
            }
        }
        
        //=========================================================================
        //=========================================================================
        protected void RemovePersistentUpgrade(string rid)
        {
            persistentUpgrades.Remove(rid);
        }
        
        //=========================================================================
        //=========================================================================
        protected void ApplyEquipment(string rid)
        {
            if (equipment.TryGetValue(rid, out var item))
            {
                return;
            }
           
            item = ResourceLibrary.Instance.CreateEquipment(rid);
            equipment.Add(rid, item);
           
        }
        
        //=========================================================================
        //=========================================================================
        protected void RemoveEquipment(string rid)
        {
            equipment.Remove(rid);
        }

        //=========================================================================
        //=========================================================================
        protected float GetAttributeFinalValue(Attributes.Attribute attribute)
        {
            var finalAttribute = new Attributes.Attribute(attribute);
            
            foreach (var upgrade in upgrades.Values)
            {
                if (upgrade.modInstructions.TryGetValue(attribute.rid, out var instruction))
                {
                    finalAttribute.ApplyModifier(instruction, upgrade.level);
                }
            }

            foreach (var upgrade in persistentUpgrades.Values)
            {
                if (upgrade.modInstructions.TryGetValue(attribute.rid, out var instruction))
                {
                    finalAttribute.ApplyModifier(instruction, upgrade.level);
                }
            }

            foreach (var item in equipment.Values)
            {
                if (item.modInstructions.TryGetValue(attribute.rid, out var instruction))
                {
                    finalAttribute.ApplyModifier(instruction);
                }
            }

            return finalAttribute.AsFloat();
        }
    }
}
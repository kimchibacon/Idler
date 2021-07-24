//=============================================================================
// Equipment.cs
//
// A Modifier class that will modify listed Attributes only when equipped.
//=============================================================================

namespace Idler.Modifiers
{
    //=========================================================================
    // Equipment
    //=========================================================================
    public class Equipment : ModifierBase
    {
        public readonly float cost;
        public readonly string[] slots;
        
        //=========================================================================
        //=========================================================================
        public Equipment(Templates.EquipmentTemplate template) : base(template)
        {
            
            slots = new string[template.slots.Length];
            cost = template.cost;
            for (var i = 0; i < template.slots.Length; ++i)
            {
                slots[i] = template.slots[i];
            }
        }
    }
}
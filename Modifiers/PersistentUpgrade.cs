//=============================================================================
// PersistentUpgrade.cs
//
// A Modifier class that will modify listed Attributes when purchased.
// Increasing level increases affect multiplier. Permanent. 
//=============================================================================

namespace Idler.Modifiers
{
    //=========================================================================
    // PersistentUpgrade
    //=========================================================================
    public class PersistentUpgrade : ModifierBase
    {
        public int level;
        public readonly int maxLevel;
        public readonly float startingCost;
        public readonly float costMultiplier;
        
        //=========================================================================
        //=========================================================================
        public PersistentUpgrade(Templates.PersistentUpgradeTemplate template) : base(template)
        {
            level = 1;
            maxLevel = template.maxLevel;
            startingCost = template.startingCost;
            costMultiplier = template.costMultiplier;
        }
    }
}

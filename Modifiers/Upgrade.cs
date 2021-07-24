//=============================================================================
// Upgrade.cs
//
// A Modifier class that will modify listed Attributes when purchased.
// Increasing level increases affect multiplier. Lost if user chooses to
// prestige. 
//=============================================================================

namespace Idler.Modifiers
{
    //=========================================================================
    // Upgrade
    //=========================================================================
    public class Upgrade : ModifierBase
    {
        public int level;
        public readonly int maxLevel;
        public readonly float startingCost;
        public readonly float costMultiplier;

        //=========================================================================
        //=========================================================================
        public Upgrade(Templates.UpgradeTemplate template) : base(template)
        {
            level = 1;
            maxLevel = template.maxLevel;
            startingCost = template.startingCost;
            costMultiplier = template.costMultiplier;
        }
    }
}

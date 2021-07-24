using System;
using System.Collections.Generic;
using System.Reflection;
//=============================================================================
// ResourceIds.cs
//
// Static collection of ids used to identify resources across the library.
//=============================================================================

namespace Idler.Resources
{
    //=========================================================================
    // ResourceIds
    //=========================================================================
    public static class ResourceIds
    {
        #region Attributes
        public const string Currency = "Currency";
        public const string EggLayingRate = "EggLayingRate";
        public const string EggValue = "EggValue";
        public const string InternalHatcheryRate = "InternalHatcheryRate";
        public const string MaxShippingRate = "MaxShippingRate";
        public const string NumberOfChickens = "NumberOfChickens";
        public const string NumberOfShippingTrucks = "NumberOfShippingTrucks";
        #endregion
        
        #region Upgrades
        public const string ComfortableNests = "ComfortableNests";
        public const string ImprovedGenetics = "ImprovedGenetics";
        #endregion
        
        #region Equipment
        public const string SemiTruck = "SemiTruck";
        #endregion
        
        //=========================================================================
        //=========================================================================
        public static string[] GetAllResourceIds()
        {
            var fields = typeof(ResourceIds).GetFields(BindingFlags.Static | BindingFlags.Public);
            var fieldValues = new string[fields.Length];

            for (var i = 0; i < fields.Length; ++i)
            {
                fieldValues[i] = (string)fields[i].GetValue(null);
            }
            
            Array.Sort(fieldValues, string.Compare);
            return fieldValues;
        }
    }
    
     
}
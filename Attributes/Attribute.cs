//=============================================================================
// Attribute.cs
//
// A type that represents an attribute value tracked by a resource ID. 
//=============================================================================

using System;

namespace Idler.Attributes
{
    [Serializable]
    public class Attribute
    {
        public float value;
        public readonly string rid;
        
        public Attribute(Templates.AttributeTemplate template)
        {
            value = template.initialValue;
            rid = template.resourceId;
        }

        public Attribute(Attribute attr)
        {
            value = attr.value;
            rid = attr.rid;
        }

        public int AsInt()
        {
            return (int)value;
        }

        public float AsFloat()
        {
            return value;
        }
        
        public void ApplyModifier(Modifiers.ModifierInstruction instruction, int modifierLevel = 1) 
        {
            switch (instruction.operation)
            {
                case Modifiers.Operation.Add:
                    value += (instruction.value * modifierLevel);
                    break;
                case Modifiers.Operation.Subtract:
                    value -= (instruction.value * modifierLevel);
                    break;
                case Modifiers.Operation.Multiply:
                    value *= (instruction.value * modifierLevel);
                    break;
                case Modifiers.Operation.Divide:
                    value /= (instruction.value * modifierLevel);
                    break;
                case Modifiers.Operation.Pow:
                    value = (float)System.Math.Pow(value, (instruction.value * modifierLevel));
                    break;
                case Modifiers.Operation.AddPercentage:
                    value *= (1 + (instruction.value * modifierLevel));
                    break;
                case Modifiers.Operation.SubtractPercentage:
                    value /= (1 + (instruction.value * modifierLevel));
                    break;
                case Modifiers.Operation.Assign:
                    this.value = (instruction.value * modifierLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(instruction.operation), instruction.operation, null);
            }
        }

        public static float operator +(Attribute l, Attribute r)
        {
            return l.value + r.value;
        }
        
        public static float operator +(float l, Attribute r)
        {
            return l + r.value;
        }
        
        public static float operator +(Attribute l, float r)
        {
            return l.value + r;
        }

        public static float operator -(Attribute l, Attribute r)
        {
            return l.value - r.value;
        }
        
        public static float operator -(float l, Attribute r)
        {
            return l - r.value;
        }
        
        public static float operator -(Attribute l, float r)
        {
            return l.value - r;
        }
        
        public static float operator *(Attribute l, Attribute r)
        {
            return l.value * r.value;
        }
        
        public static float operator *(float l, Attribute r)
        {
            return l * r.value;
        }
        
        public static float operator *(Attribute l, float r)
        {
            return l.value * r;
        }
        
        public static float operator /(Attribute l, Attribute r)
        {
            return l.value / r.value;
        }
        
        public static float operator /(float l, Attribute r)
        {
            return l / r.value;
        }
        
        public static float operator /(Attribute l, float r)
        {
            return l.value / r;
        }
        
        public static implicit operator float(Attribute r)
        {
            return r.value;
        }
    }
}
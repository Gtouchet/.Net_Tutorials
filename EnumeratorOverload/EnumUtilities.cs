using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EnumeratorCustomAttributes;

internal class WrongAttributeException : Exception
{
    public WrongAttributeException(string message) : base(message) { }
}

internal class NoAttributeException : Exception
{
    public NoAttributeException(string message) : base(message) { }
}

internal static class EnumUtilities
{
    /*
     * For description and display
     */
    public static string Description<E>(this E value) where E : Enum
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        if (field == null)
        {
            return string.Empty;
        }
        DescriptionAttribute? descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Description;
    }

    public static string? Display<E>(this E value) where E : Enum
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        if (field == null)
        {
            return string.Empty;
        }
        DisplayAttribute? displayAttribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
        return displayAttribute == null ? value.ToString() : displayAttribute.Name;
    }

    /*
     * For custom class attribute
     */
    public static A GetValuesOf<A>(this Enum value) where A : Attribute
    {
        Type enumType = value.GetType();
        MemberInfo[] memberInfos = enumType.GetMember(value.ToString());
        if (!memberInfos[0].CustomAttributes.Any())
        {
            throw new NoAttributeException($"No attribute data for {value.GetType()}.{value}");
        }

        object[] attributes = memberInfos[0].GetCustomAttributes(typeof(A), false);
        if (attributes.Length == 0)
        {
            Type validType = memberInfos[0].CustomAttributes.First().AttributeType;
            throw new WrongAttributeException(
                $"Invalid attribute type {typeof(A)} for {value.GetType()}.{value}, valid type is {validType}\n" +
                $"To correct this, call the method like this : GetValuesOf<{validType}>()");
        }
        
        return (A)attributes[0];
    }
}

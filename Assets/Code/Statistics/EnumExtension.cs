using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace CowtasticGameStudio.MuuliciousHarvest
{
    public static class EnumExtensions
    {
        public static string GetEnumString(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            EnumMemberAttribute attribute = (EnumMemberAttribute) Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute));
            return attribute == null ? value.ToString() : attribute.Value;
        }
    }
}

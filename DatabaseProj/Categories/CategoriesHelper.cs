using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Categories
{
    public static class CategoriesHelper
    {
        public static string GetEnumDisplayName(this Enum enumType)
        {
            return enumType.GetType().GetMember(enumType.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }
    }
}

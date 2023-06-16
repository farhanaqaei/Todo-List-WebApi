using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TodoList.WebApi.Extensions;

public static class EnumExtensions
{
	public static string GetEnumName(this System.Enum myEnum)
	{
		var enumDisplayName = myEnum.GetType()
			.GetMember(myEnum.ToString())
			.FirstOrDefault();

		if (enumDisplayName != null)
		{
			return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();
		}

		return "";
	}
}

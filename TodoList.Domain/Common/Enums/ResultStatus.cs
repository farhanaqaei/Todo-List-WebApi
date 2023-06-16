using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Domain.Common.Enums;

public enum ResultStatus
{
	[Display(Name = "sth went wrong, please try again later")]
	Failed,
	Succeded,
	NotFound,
	NotForUser,
	[Display(Name = "there is sth wrong with the data you provided")]
	WrongData,
	[Display(Name = "username or password is wrong")]
	WrongCredentials
}

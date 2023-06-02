namespace TodoList.Application.UserServices.Interfaces;

public interface IPasswordHelper
{
	string EncodePasswordMd5(string password);
}

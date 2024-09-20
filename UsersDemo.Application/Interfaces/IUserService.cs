using UsersDemo.Domain.Models;
using UsersDemo.Domain.Traits;

namespace UsersDemo.Application.Interfaces;

public interface IUserService
{
	Task<IEnumerable<User>> GetAll();
	Task<User> GetById(int id);
	Task<int> Create(RegisterDto registerDto);
	Task<User> Update(ChangeUserDataDto changeUserDataDto);
	Task<int> Delete(int id);
}
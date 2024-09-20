using UsersDemo.Domain.Models;

namespace UsersDemo.Persistence.Interfaces;

public interface IUserRepository
{
	Task<IEnumerable<User>> GetAll();
	Task<User> GetById(int id);
	Task<int> Create(User user);
	Task<User> Update(User user);
	Task<int> Delete(int id);
}
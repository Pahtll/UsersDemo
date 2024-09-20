using Microsoft.EntityFrameworkCore;
using UsersDemo.Domain.Models;
using UsersDemo.Persistence.Interfaces;

namespace UsersDemo.Persistence.Repositories;

public class UserRepository(
	UsersDemoDbContext context
	) : IUserRepository
{
	public async Task<IEnumerable<User>> GetAll()
	{
		return await context.Users
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<User> GetById(int id)
	{
		return await context.Users
			       .AsNoTracking()
			       .FirstOrDefaultAsync(u => u.Id == id)
		       ?? throw new ArgumentException("User with this ID not found");
	}

	public async Task<int> Create(User user)
	{
		await context.Users
			.AddAsync(user);

		await context.SaveChangesAsync();

		return user.Id;
	}

	public async Task<User> Update(User user)
	{
		await context.Users
			.Where(u => u.Id == user.Id)
			.ExecuteUpdateAsync(up =>
				up.SetProperty(u => u.Username, user.Username)
					.SetProperty(u => u.Email, user.Email));
		
		return user;
	}

	public async Task<int> Delete(int id)
	{
		await context.Users
			.Where(u => u.Id == id)
			.ExecuteDeleteAsync();

		return id;
	}
}
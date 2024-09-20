using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersDemo.Domain.Models;
using UsersDemo.Persistence.Interfaces;

namespace UsersDemo.Persistence.Repositories;

public class UserRepository(
	UsersDemoDbContext context,
	ILogger<UserRepository> logger
	) : IUserRepository
{
	private const string LoggingClass = nameof(UserRepository);
	
	public async Task<IEnumerable<User>> GetAll()
	{
		logger.LogDebug("{LoggingClass}: Trying to get all users", LoggingClass);
		return await context.Users
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<User> GetById(int id)
	{
		logger.LogDebug("{LoggingClass}: Trying to get user by ID", LoggingClass);
		
		return await context.Users
			       .AsNoTracking()
			       .FirstOrDefaultAsync(u => u.Id == id)
		       ?? throw new ArgumentException("User with this ID not found");
	}

	public async Task<int> Create(User user)
	{
		logger.LogDebug("{LoggingClass}: Trying to create user", LoggingClass);
		
		try
		{
			await context.Users
				.AddAsync(user);

			await context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			logger.LogError("{LoggingClass}: Error while creating user: \"{ErrorMsg}\"", LoggingClass, e.Message);
			throw new Exception("Error while creating user: " + e.Message);
		}

		return user.Id;
	}

	public async Task<User> Update(User user)
	{
		logger.LogDebug("{LoggingClass}: Trying to update user", LoggingClass);
		
		try
		{
			await context.Users
				.Where(u => u.Id == user.Id)
				.ExecuteUpdateAsync(up =>
					up.SetProperty(u => u.Username, user.Username)
						.SetProperty(u => u.Email, user.Email));
		}
		catch (Exception e)
		{
			logger.LogError("{LoggingClass}: Error while updating user: \"{ErrorMsg}\"", LoggingClass, e.Message);
			throw new Exception("Error while updating user: " + e.Message);
		}
		
		return user;
	}

	public async Task<int> Delete(int id)
	{
		logger.LogDebug("{LoggingClass}: Trying to delete user", LoggingClass);
		
		try
		{
			await context.Users
				.Where(u => u.Id == id)
				.ExecuteDeleteAsync();
		}
		catch (Exception e)
		{
			logger.LogError("{LoggingClass}: Error while deleting user: \"{ErrorMsg}\"", LoggingClass, e.Message);
			throw new Exception("Error while deleting user: " + e.Message);
		}

		return id;
	}
}
using Microsoft.Extensions.Logging;
using UsersDemo.Application.Interfaces;
using UsersDemo.Domain.Models;
using UsersDemo.Domain.Traits;
using UsersDemo.Persistence.Interfaces;

namespace UsersDemo.Application.Services;

public class UserService(
	IUserRepository userRepository,
	ILogger<UserService> logger
	) : IUserService
{
	private const string LoggingClass = nameof(UserService);
	
	public async Task<IEnumerable<User>> GetAll()
	{
		logger.LogDebug("{LoggingClass}: Trying to get all users", LoggingClass);
		
		return await userRepository.GetAll();
	}

	public async Task<User> GetById(int id)
	{
		logger.LogDebug("{LoggingClass}: Trying to get user by ID", LoggingClass);
		return await userRepository.GetById(id);
	}

	public async Task<int> Create(RegisterDto registerDto)
	{
		logger.LogDebug("{LoggingClass}: Trying to validate registerDto", LoggingClass);;
		
		// TODO: Implement good validation instead of this
		if (string.IsNullOrEmpty(registerDto.Username))
		{
			throw new ArgumentException("Username is required");
		}

		if (string.IsNullOrEmpty(registerDto.Email))
		{
			logger.LogDebug("{LoggingClass}: Email is required", LoggingClass);
			throw new ArgumentException("Email is required");
		}

		var user = new User
		{
			Username = registerDto.Username,
			Email = registerDto.Email
		};
		
		try
		{
			return await userRepository.Create(user);
		}
		catch (Exception e)
		{
			logger.LogError("{LoggingClass}: Error while creating user: \"{ErrorMsg}\"", LoggingClass, e.Message);
			throw new Exception("Error while creating user: " + e.Message);
		}
	}

	public async Task<User> Update(ChangeUserDataDto changeUserDataDto)
	{
		logger.LogDebug("{LoggingClass}: Trying to validate changeUserDataDto", LoggingClass);
		
		// TODO: Implement good validation instead of this
		if (string.IsNullOrEmpty(changeUserDataDto.Username))
		{
			logger.LogDebug("{LoggingClass}: Username is required", LoggingClass);
			throw new ArgumentException("Username is required");
		}

		if (string.IsNullOrEmpty(changeUserDataDto.Email))
		{
			logger.LogDebug("{LoggingClass}: Email is required", LoggingClass);
			throw new ArgumentException("Email is required");
		}

		var user = new User
		{
			Id = changeUserDataDto.Id,
			Username = changeUserDataDto.Username,
			Email = changeUserDataDto.Email
		};

		try
		{
			return await userRepository.Update(user);
		}
		catch (Exception e)
		{
			logger.LogError("{LoggingClass}: Error while updating user: \"{ErrorMsg}\"", LoggingClass, e.Message);
			throw new Exception("Error while updating user: " + e.Message);
		}
	}

	public async Task<int> Delete(int id)
	{
		try
		{
			return await userRepository.Delete(id);
		}
		catch (Exception e)
		{
			logger.LogError("{LoggingClass}: Error while deleting user: \"{ErrorMsg}\"", LoggingClass, e.Message);
			throw new Exception("Error while deleting user: " + e.Message);
		}
	}
}
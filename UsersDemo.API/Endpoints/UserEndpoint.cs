using Microsoft.AspNetCore.Mvc;
using UsersDemo.Application.Interfaces;
using UsersDemo.Domain.Models;
using UsersDemo.Domain.Traits;

namespace UsersDemo.API.Endpoints;

public static class UserEndpoint
{
	public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/users", GetAll);
		app.MapGet("/users/{id}", GetById);
		app.MapPost("/users", Create);
		app.MapPut("/users", Update);
		app.MapDelete("/users/{id}", Delete);

		return app;
	}

	private static async Task<IActionResult> GetAll(IUserService userService)
	{
		var users = await userService.GetAll();

		return users.Any()
			? new OkObjectResult(users)
			: new NoContentResult();
	}

	private static async Task<IActionResult> GetById(IUserService userService, int id)
	{
		try
		{
			var user = await userService.GetById(id);
			return new OkObjectResult(user);

		}
		catch (Exception e)
		{
			return new NotFoundObjectResult(e.Message);
		}
	}
	
	private static async Task<IActionResult> Create(IUserService userService, RegisterDto registerDto)
	{
		try
		{
			var id = await userService.Create(registerDto);
			return new CreatedResult($"/users/{id}", id);
		}
		catch (Exception e)
		{
			return new BadRequestObjectResult(e.Message);
		}
	}
	
	private static async Task<IActionResult> Update(IUserService userService, ChangeUserDataDto changeUserDataDto)
	{
		try
		{
			await userService.Update(changeUserDataDto);
			return new OkResult();
		}
		catch (Exception e)
		{
			return new BadRequestObjectResult(e.Message);
		}
	}
	
	private static async Task<IActionResult> Delete(IUserService userService, int id)
	{
		try
		{
			await userService.Delete(id);
			return new OkResult();
		}
		catch (Exception e)
		{
			return new BadRequestObjectResult(e.Message);
		}
	}
}
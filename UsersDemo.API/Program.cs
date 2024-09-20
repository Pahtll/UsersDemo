using Microsoft.EntityFrameworkCore;
using UsersDemo.API.Endpoints;
using UsersDemo.Application.Interfaces;
using UsersDemo.Application.Services;
using UsersDemo.Persistence;
using UsersDemo.Persistence.Interfaces;
using UsersDemo.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<UsersDemoDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("UsersDemo"));
});

builder.Services.AddControllers();
builder.Services.AddCors(cb =>
{
	cb.AddDefaultPolicy(dp =>
	{
		dp.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseCors();
app.MapUserEndpoints();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
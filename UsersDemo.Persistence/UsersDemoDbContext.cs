using Microsoft.EntityFrameworkCore;
using UsersDemo.Domain.Models;
using UsersDemo.Persistence.Configurations;

namespace UsersDemo.Persistence;

public class UsersDemoDbContext(
	DbContextOptions<UsersDemoDbContext> options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<User> Users { get; set; }
}
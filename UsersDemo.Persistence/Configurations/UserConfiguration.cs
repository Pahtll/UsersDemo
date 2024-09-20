using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersDemo.Domain.Models;

namespace UsersDemo.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);
		
		builder.Property(u => u.Username)
			.HasMaxLength(25)
			.IsRequired();

		builder.Property(u => u.Email)
			.HasMaxLength(55)
			.IsRequired();
	}
}
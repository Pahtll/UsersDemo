using System.ComponentModel.DataAnnotations;

namespace UsersDemo.Domain.Traits;

public record ChangeUserDataDto(
	[Required] int Id,
	[Required] [MaxLength(25)] string Username,
	[Required] [MaxLength(55)] string Email);
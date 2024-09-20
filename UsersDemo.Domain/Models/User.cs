using System.ComponentModel.DataAnnotations;

namespace UsersDemo.Domain.Models;

public class User
{
	[Key]
	public int Id { get; set; }
	
	[Required]
	[MaxLength(25)]
	public string Username { get; set; }
	
	[Required]
	[MaxLength(55)]
	public string Email { get; set; }
}
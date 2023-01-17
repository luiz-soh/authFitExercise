using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto.User
{
	public class AddUserEmailInput
	{
		public AddUserEmailInput()
		{
			Email = string.Empty;
			UserId = 0;
		}

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public int UserId { get; set; }
	}
}


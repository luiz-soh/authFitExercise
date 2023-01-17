﻿using System;
using models.Dto.User;
using Models.Dto.User;

namespace Application.Interfaces.User
{
	public interface IUserService
	{
		Task AddUserEmail(AddUserEmailInput input);
		Task<UserDto> GetUserData(int userId);
	}
}


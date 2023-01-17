using System;
using Models.Dto.User;

namespace Models.Dto.User
{
	public class UserDataOutput
	{
		public UserDataOutput()
		{
            UserData = new UserDto();
			Success = false;
		}

		public UserDataOutput(UserDto user)
		{
            UserData = user;
			Success = true;
		}

		public UserDto UserData;
		public bool Success;
	}
}


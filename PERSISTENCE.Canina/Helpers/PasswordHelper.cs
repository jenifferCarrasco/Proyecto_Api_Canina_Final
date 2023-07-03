using System;
using System.Text;

namespace PERSISTENCE.Canina.Helpers
{
	public static class PasswordHelper
	{
		public static string GeneratePassword(int length)
		{
			const string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
			StringBuilder password = new StringBuilder();
			Random random = new Random();

			for (int i = 0; i < length; i++)
			{
				int index = random.Next(0, allowedCharacters.Length);
				password.Append(allowedCharacters[index]);
			}

			return password.ToString();
		}
	}
}

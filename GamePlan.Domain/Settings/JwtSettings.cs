﻿namespace GamePlan.Domain.Settings
{
	public class JwtSettings
	{
		public const string DefaultSections = "Jwt";

		public string Issuer { get; set; }

		public string Audience { get; set; }

		public string Authority { get; set; }

		public string JwtKey { get; set; }

		public int Lifetime { get; set; }

		public int RefreshTokenValidityInDays { get; set; }
	}
}

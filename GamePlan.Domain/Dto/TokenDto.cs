using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlan.Domain.Dto
{
	public record TokenDto(string AccessToken, string RefreshToken);
}

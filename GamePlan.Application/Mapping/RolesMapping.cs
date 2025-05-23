﻿using AutoMapper;
using GamePlan.Domain.Dto.Role;
using GamePlan.Domain.Entity;

namespace GamePlan.Application.Mapping
{
	public class RolesMapping : Profile
	{
		public RolesMapping()
		{
			CreateMap<Roles, RolesDto>()
				.ForCtorParam(ctorParamName: "Id", m => m.MapFrom(s => s.Id))
				.ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
				.ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
				.ReverseMap();
		}
	}
}

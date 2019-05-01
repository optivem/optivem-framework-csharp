﻿using AutoMapper;

namespace Optivem.Framework.Infrastructure.Common.Mapping.AutoMapper
{
    // TODO: VC: Interfaces for entity and for response? to ensure people put it in correct sequence...

    public abstract class AutoMapperResponseProfile<TEntity, TResponse> : Profile
    {
        protected IMappingExpression<TEntity, TResponse> map;

        public AutoMapperResponseProfile()
        {
            map = CreateMap<TEntity, TResponse>();
        }
    }
}

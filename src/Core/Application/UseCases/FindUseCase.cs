﻿using Optivem.Core.Domain;
using System.Threading.Tasks;

namespace Optivem.Core.Application
{
    public class FindUseCase<TResponse, TEntity, TKey> : IFindUseCase<TKey, TResponse>
        where TEntity : class, IEntity<TKey>
    {
        public FindUseCase(IResponseMapper responseMapper, IReadonlyRepository<TEntity, TKey> repository)
        {
            ResponseMapper = responseMapper;
            Repository = repository;
        }

        protected IResponseMapper ResponseMapper { get; private set; }

        protected IReadonlyRepository<TEntity, TKey> Repository { get; private set; }

        public async Task<TResponse> HandleAsync(TKey request)
        {
            var id = request;
            var entity = await Repository.GetSingleOrDefaultAsync(id);
            var response = ResponseMapper.Map<TEntity, TResponse>(entity);
            return response;
        }
    }
}
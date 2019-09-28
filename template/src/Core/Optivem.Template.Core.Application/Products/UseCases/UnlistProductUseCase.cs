﻿using Optivem.Framework.Core.Application.UseCases;
using Optivem.Framework.Core.Common.Mapping;
using Optivem.Framework.Core.Domain;
using Optivem.Template.Core.Application.Products.Requests;
using Optivem.Template.Core.Application.Products.Responses;
using Optivem.Template.Core.Domain.Products;

namespace Optivem.Template.Core.Application.Products.UseCases
{
    public class UnlistProductUseCase : ExecuteAggregateUseCase<IProductRepository, DeactivateProductRequest, DeactivateProductResponse, Product, ProductIdentity, int>
    {
        public UnlistProductUseCase(IMapper mapper, IUnitOfWork unitOfWork) 
            : base(mapper, unitOfWork)
        {
        }

        protected override void Execute(DeactivateProductRequest request, Product aggregateRoot)
        {
            aggregateRoot.Deactivate();
        }
    }
}

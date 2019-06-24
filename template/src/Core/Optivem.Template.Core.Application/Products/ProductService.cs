﻿using Optivem.Core.Application;
using Optivem.Core.Application.Services;
using System;
using System.Threading.Tasks;

namespace Optivem.Template.Core.Application.Products
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IRequestHandler requestHandler) 
            : base(requestHandler)
        {
        }

        public Task<BrowseProductsResponse> BrowseProductsAsync(BrowseProductsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<CreateProductResponse> CreateProductAsync(CreateProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<FindProductResponse> FindProductAsync(FindProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ListProductsResponse> ListProductsAsync(ListProductsRequest request)
        {
            return ListAsync<ListProductsRequest, ListProductsResponse>(request);
        }

        public Task<ListProductsResponse> ListProductsAsync()
        {
            return ListAsync<ListProductsRequest, ListProductsResponse>();
        }

        public Task<RelistProductResponse> RelistProductAsync(RelistProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UnlistProductResponse> UnlistProductAsync(UnlistProductRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProductResponse> UpdateProductAsync(UpdateProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

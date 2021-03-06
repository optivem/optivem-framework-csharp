﻿using Microsoft.AspNetCore.Mvc;
using Atomiv.Web.AspNetCore;
using Generator.Core.Application.Products;
using Generator.Core.Application.Products.Requests;
using Generator.Core.Application.Products.Responses;
using System.Threading.Tasks;

namespace Generator.Web.RestApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : BaseController<IProductService>
    {
        public ProductsController(IProductService service)
            : base(service)
        {
        }

        [HttpGet(Name = "browse-products")]
        [ProducesResponseType(typeof(BrowseProductsResponse), 200)]
        public async Task<ActionResult<BrowseProductsResponse>> BrowseProductsAsync([FromQuery] int? page = null, [FromQuery] int? size = null)
        {
            var request = new BrowseProductsRequest
            {
                Page = page.Value,
                Size = size.Value,
            };

            var response = await Service.BrowseProductsAsync(request);
            return Ok(response);
        }

        [HttpGet("list", Name = "list-products")]
        [ProducesResponseType(typeof(ListProductsResponse), 200)]
        public async Task<ActionResult<ListProductsResponse>> ListProductsAsync()
        {
            var request = new ListProductsRequest { };
            var response = await Service.ListProductsAsync(request);
            return Ok(response);
        }
    }
}

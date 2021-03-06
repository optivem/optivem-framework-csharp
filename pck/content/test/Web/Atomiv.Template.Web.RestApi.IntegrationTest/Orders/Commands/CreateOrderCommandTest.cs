﻿using FluentAssertions;
using Atomiv.Template.Core.Application.Commands.Orders;
using Atomiv.Template.Core.Application.Queries.Orders;
using Atomiv.Template.Core.Common.Orders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Atomiv.Template.Web.RestApi.IntegrationTest.Orders.Commands
{
    public class CreateOrderCommandTest : BaseTest
    {
        public CreateOrderCommandTest(Fixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CreateOrder_ValidRequest_ReturnsResponse()
        {
            // Arrange

            var header = await GetDefaultHeaderDataAsync();

            var createCustomerResponses = await CreateSampleCustomersAsync();
            var createProductResponses = await CreateSampleProductsAsync();

            var customerId_0 = createCustomerResponses[0].Data.Id;

            var productId_0 = createProductResponses[0].Data.Id;
            var productId_1 = createProductResponses[1].Data.Id;

            var createRequest = new CreateOrderCommand
            {
                CustomerId = customerId_0,
                OrderItems = new List<CreateOrderItemCommand>
                {
                    new CreateOrderItemCommand
                    {
                        ProductId = productId_0,
                        Quantity = 10,
                    },

                    new CreateOrderItemCommand
                    {
                        ProductId = productId_1,
                        Quantity = 20,
                    }
                },
            };

            // Act

            var createHttpResponse = await Fixture.Api.Orders.CreateOrderAsync(createRequest, header);

            // Assert

            createHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createResponse = createHttpResponse.Data;

            createResponse.Id.Should().NotBeEmpty();
            createResponse.CustomerId.Should().Be(createRequest.CustomerId);
            createResponse.Status.Should().Be(OrderStatus.Draft);

            createResponse.OrderItems.Should().NotBeNull();

            createResponse.OrderItems.Count.Should().Be(createRequest.OrderItems.Count);

            for (int i = 0; i < createRequest.OrderItems.Count; i++)
            {
                var createRequestOrderDetail = createRequest.OrderItems[i];
                var createResponseOrderDetail = createResponse.OrderItems[i];

                createResponseOrderDetail.Id.Should().NotBeEmpty();
                createResponseOrderDetail.ProductId.Should().Be(createRequestOrderDetail.ProductId);
                createResponseOrderDetail.Quantity.Should().Be(createRequestOrderDetail.Quantity);
                createResponseOrderDetail.Status.Should().Be(OrderItemStatus.Pending);
            }

            var findRequest = new ViewOrderQuery { Id = createResponse.Id };
            var findHttpResponse = await Fixture.Api.Orders.ViewOrderAsync(findRequest, header);

            findHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var findResponse = findHttpResponse.Data;

            findResponse.Should().BeEquivalentTo(createResponse);
        }

        [Fact]
        public async Task CreateOrder_InvalidRequest_ThrowsInvalidRequestException()
        {
            // Arrange

            var header = await GetDefaultHeaderDataAsync();

            var customerId = Guid.NewGuid();

            var createRequest = new CreateOrderCommand
            {
                CustomerId = customerId,
                OrderItems = null,
            };

            // Act

            var createHttpResponse = await Fixture.Api.Orders.CreateOrderAsync(createRequest, header);

            // Assert

            createHttpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Atomiv.Web.AspNetCore.RestApi.IntegrationTest.Fake.Dtos.Customers;
using Atomiv.Web.AspNetCore.RestApi.IntegrationTest.Fake.Dtos.Customers.Exports;
using Atomiv.Web.AspNetCore.RestApi.IntegrationTest.Fake.Entities;
using Atomiv.Web.AspNetCore.RestApi.IntegrationTest.Fake.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Atomiv.Web.AspNetCore.RestApi.IntegrationTest.Fake.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMapper mapper, ILogger<CustomersController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerGetAllResponse), 200)]
        public ActionResult<CustomerGetAllResponse> Get()
        {
            _logger.LogInformation("Hello world....");

            _logger.LogError("Hello world.... This is an error!");

            var entities = repository;

            var responses = _mapper.Map<List<Customer>, List<CustomerGetAllResponse>>(entities);

            return Ok(responses);
        }

        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(CustomerGetResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<CustomerGetResponse> Get(int id)
        {
            var entity = repository.SingleOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<Customer, CustomerGetResponse>(entity);

            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerPostResponse), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 422)]
        [ProducesResponseType(500)]
        public ActionResult<CustomerPostResponse> Post([FromBody] CustomerPostRequest request)
        {
            // TODO: VC: Validation

            /*
            if(string.IsNullOrWhiteSpace(request.FirstName))
            {
                return BadRequest();
            }
            */

            var id = repository.Max(e => e.Id) + 1;

            var entity = new Customer
            {
                Id = id,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
                Cards = new List<Card>(),
            };

            repository.Add(entity);

            var response = _mapper.Map<Customer, CustomerPostResponse>(entity);

            return CreatedAtAction("Get", new { id = response.Id }, response); ;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerPutResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<CustomerPutResponse> Put(int id, [FromBody] CustomerPutRequest request)
        {
            var entity = repository.SingleOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;

            var response = _mapper.Map<Customer, CustomerPutResponse>(entity);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            var entity = repository.SingleOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            repository.Remove(entity);

            return NoContent();
        }

        #region Exports

        [HttpGet("exports")]
        [ProducesResponseType(typeof(IEnumerable<CustomerExportGetCollectionResponse>), 200)]
        public ActionResult<IEnumerable<CustomerExportGetCollectionResponse>> GetExports()
        {
            var entities = repository;

            var responses = _mapper.Map<List<Customer>, List<CustomerExportGetCollectionResponse>>(entities);

            return Ok(responses);
        }

        #endregion Exports

        #region Imports

        [HttpPost("imports")]
        [ProducesResponseType(202)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] List<CustomerImportCollectionPostRequest> requests)
        {
            // TODO: VC: Simulate queue

            foreach (var request in requests)
            {
                // TODO: VC: Statically typed mapper, just like for UnitOfWork typed repositories...

                var entity = _mapper.Map<CustomerImportCollectionPostRequest, Customer>(request);

                repository.Add(entity);
            }

            return Accepted();
        }

        #endregion Imports

        #region Helper

        private static List<Customer> repository = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    UserName = "jsmith",
                    FirstName = "John",
                    LastName = "Smith",
                    CreatedDateTime = new DateTime(2019, 1, 1, 8, 20, 36),
                    ModifiedDateTime = new DateTime(2019, 1, 2, 9, 30, 42),

                    Cards = new List<Card>
                    {
                        new Card
                        {
                            Number = "12345678",
                            Expiration = new CardExpiration
                            {
                                ExpirationYear = 2020,
                                ExpirationMonth = 2
                            },
                        }
                    },
                },

                new Customer
                {
                    Id = 2,
                    UserName = "mcdonald",
                    FirstName = "Mary",
                    LastName = "McDonald",
                    CreatedDateTime = new DateTime(2018, 7, 4, 14, 40, 12),
                    ModifiedDateTime = new DateTime(2018, 9, 8, 18, 50, 18),

                    Cards = new List<Card>
                    {
                        new Card
                        {
                            Number = "34235233",
                            Expiration = new CardExpiration
                            {
                                ExpirationYear = 2015,
                                ExpirationMonth = 2
                            },
                        },

                        new Card
                        {
                            Number = "04232353",
                            Expiration = new CardExpiration
                            {
                                ExpirationYear = 2014,
                                ExpirationMonth = 3
                            },
                        },
                    },
                }
            };

        #endregion Helper
    }
}
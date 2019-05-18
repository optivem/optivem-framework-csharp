﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Optivem.Core.Application;
using Optivem.Core.Domain;
using Optivem.Infrastructure.Mapping.AutoMapper;
using Optivem.Infrastructure.Messaging.MediatR;
using Optivem.Infrastructure.Persistence.EntityFrameworkCore;
using Optivem.NorthwindLite.Core.Application;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Queries.BrowseAll;
using Optivem.NorthwindLite.Core.Application.Interface.Customers.Queries.List;
using Optivem.NorthwindLite.Core.Application.Interface.Services;
using Optivem.NorthwindLite.Core.Application.UseCases.Customers;
using Optivem.NorthwindLite.Core.Domain.Entities;
using Optivem.NorthwindLite.Infrastructure.Persistence;

namespace Optivem.NorthwindLite.Web
{
    public class Startup
    {
        private const string ContextConnectionStringKey = "Context";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // TODO: VC: Move to base, automatic lookup of everything implementing IService, auto-DI

            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var mediatRAssemblies = allAssemblies; // TODO: VC
            var autoMapperAssemblies = allAssemblies; // TODO: VC


            // Application - Use Cases
            services.AddScoped<IUseCase<ListCustomersRequest, ListCustomersResponse>, ListCustomersUseCase>();

            // Application - Services
            services.AddScoped<ICustomerService, CustomerService>();

            // Infrastructure - Repository
            var connection = Configuration.GetConnectionString(ContextConnectionStringKey);
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork<Context>>();
            services.AddScoped<IReadonlyRepository<Customer, int>, CustomerRepository>();

            // Infrastructure - Mapping
            services.AddAutoMapper(autoMapperAssemblies);
            services.AddScoped<IResponseMapper, ResponseMapper>();



            // Infrastructure - Messaging
            services.AddMediatR(mediatRAssemblies);
            services.AddScoped<IUseCaseMediator, UseCaseMediator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

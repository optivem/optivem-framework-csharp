﻿using Atomiv.Infrastructure.SequentialGuid;
using Atomiv.Template.Core.Domain.Customers;
using System;

namespace Atomiv.Template.Infrastructure.Domain.Persistence.IdentityGenerators
{
    public class CustomerIdentityGenerator : IdentityGenerator<CustomerIdentity>
    {
        protected override CustomerIdentity Create(Guid guid)
        {
            return new CustomerIdentity(guid);
        }
    }
}
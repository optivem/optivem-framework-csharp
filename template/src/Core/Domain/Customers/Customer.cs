﻿using Optivem.Framework.Core.Domain;

namespace Optivem.Template.Core.Domain.Customers
{
    public class Customer : AggregateRoot<CustomerIdentity>
    {
        public Customer(CustomerIdentity id, string firstName, string lastName) 
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobrePraktyki.DesignPrinciples
{
    public class CustomerService
    {
        private ICollection<Customer> Customers { get; } = new List<Customer> { new Customer(1), new Customer(2), new Customer(3), new Customer(4), new Customer(5) };

        public bool DeleteCustomer(Customer customer)
        {
            return Customers.Remove(customer);
        }

        public Customer FindByDebit(float debit)
        {
            return Customers.SingleOrDefault(x => x.AllowedDebit == debit);
        }

        public bool Charge(int customerId, float amount)
        {
            var customer = Customers.SingleOrDefault(x => x.Id == customerId);
            if (customer == null)
                return false;

            if (customer.Income - customer.Outcome + customer.AllowedDebit < amount)
                return false;

            customer.Outcome += amount;
            return true;
        }

        public void Fund(int customerId, float amount)
        {
            var customer = Customers.Where(x => x.Id == customerId).SingleOrDefault();
            if (customer == null)
                return;
            customer.Income += amount;
        }

        public float? GetBalance(int customerId)
        {
            var customer = Customers.Where(x => x.Id == customerId).SingleOrDefault();
            return customer?.Income - customer?.Outcome;
        }
    }
}
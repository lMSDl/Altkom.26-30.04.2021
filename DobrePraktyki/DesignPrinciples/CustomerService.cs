using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobrePraktyki.DesignPrinciples
{
    public class CustomerService
    {
        private ICollection<Customer> Customers { get; } = new List<Customer> { new Customer(1), new Customer(2), new Customer(3), new Customer(4), new Customer(5) };

        public Customer FindById(int id)
        {
            return Customers.SingleOrDefault(x => x.Id == id);
        }

        public bool Charge(int customerId, float amount)
        {
            var customer = FindById(customerId);
            if (customer == null)
                return false;

            if (GetBalance(customerId) + customer.AllowedDebit < amount)
                return false;

            customer.Outcome += amount;
            return true;
        }

        public void Fund(int customerId, float amount)
        {
            var customer = FindById(customerId);
            if (customer == null)
                return;
            customer.Income += amount;
        }

        public float? GetBalance(int customerId)
        {
            var customer = FindById(customerId);
            return customer?.Income - customer?.Outcome;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobrePraktyki.DesignPrinciples
{
    public class PaymentService
    {
        public bool Charge(PaymentAccount paymentAccount, float amount)
        {
            return paymentAccount?.Charge(amount) ?? false;
        }

        public void Fund(PaymentAccount paymentAccount, float amount)
        {
            paymentAccount.Fund(amount);
        }
    }
}

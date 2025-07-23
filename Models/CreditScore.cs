using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditScoreCalculator.Models
{
    public class CustomerRecords
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public double PaymentHistory { get; set; } // 0 to 100
        public double CreditUtilization { get; set; } // 0 to 100
        public int AgeOfCreditHistory { get; set; } // in years
    };
}

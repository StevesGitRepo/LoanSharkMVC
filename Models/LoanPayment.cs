using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LoanSharkMVC.Models
{
    public class LoanPayment
    {
        public int Month { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]

        public decimal Payment { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal MonthlyPrincipal { get; set; }

        public decimal MonthlyInterest { get; set; }

        public decimal TotalInterest { get; set; }

        public decimal Balance { get; set; }

        public decimal Amount { get; set; }

    }

}

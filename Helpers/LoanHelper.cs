using LoanSharkMVC.Models;

namespace LoanSharkMVC.Helpers
{
    public class LoanHelper
    {
        public Loan GetPayments(Loan loan)
        {
            //Calculate my Monthly Payment: These are the inputs
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term, loan.DownPayment);
            //Create loop from 1 to term
            var balance = loan.Amount;
            var totalInterest = 0.00m;
            var monthlyInterest = 0.00m;
            var monthlyPrincipal = 0.00m;
            var monthlyRate = CalcMonthlyRate(loan.Rate);
            var downPayment = 0.00m;

            for (int month = 0; month < loan.Term; month++)
            {
                //loop over each month until the term of the loan is reached
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                //Push the object into the loan model
                loan.Payments.Add(loanPayment);
            }

            loan.Amount = loan.Amount - loan.DownPayment;
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

            //return the loan to the view
            return loan;
        }

        private decimal CalcPayment(decimal amount, decimal rate, int term, decimal downPayment)
        {
            decimal payment = 0.0m;

            var monthlyRate = CalcMonthlyRate(rate);

            var rateD = Convert.ToDouble(monthlyRate);

            var amountD = Convert.ToDouble(amount);

            var downPaymentD = Convert.ToDouble(downPayment);

            var paymentD = ((amountD - downPaymentD) * rateD) / (1 - Math.Pow(1 + rateD, -term));


            return Convert.ToDecimal(paymentD);
        }

        private decimal CalcMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }

        private decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}

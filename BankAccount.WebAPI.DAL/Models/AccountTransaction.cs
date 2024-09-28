using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL
{
    public class AccountTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public Account Account { get; set; }
    }

}

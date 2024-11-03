using System.ComponentModel.DataAnnotations;

namespace BankAccount.WebAPI.DAL
{
    public class CustomerAccountCard
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string DebitCardType { get; set; }
        public decimal MaximumAmount { get; set; }
        
        
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.WebAPI.DAL
{
    public class DebitCard
    {
        [Key]
        public int DebitCardId { get; set; }
        public int AccountId { get; set; }
        public string DebitCardType { get; set; }
        public decimal MaximumAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }

}

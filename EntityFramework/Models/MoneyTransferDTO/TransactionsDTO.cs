using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Models.MoneyTransferDTO
{
    public class TransactionsDTO
    {
 
        public List<Transactions>? Added { get; set; }
        public List<Transactions>? Substracted { get; set;}

        public TransactionsDTO(List<Transactions> added, List<Transactions> substracted)
        {
            string prefixMinus = "- ";

            Added = added;

            foreach(var i in substracted)
            {
                i.money = prefixMinus + i.money;
                Console.WriteLine(i.money);
            }

            Substracted = substracted;
        }

    }
}

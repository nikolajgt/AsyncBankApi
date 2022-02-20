namespace EntityFramework.Models.MoneyTransferDTO
{
    public class AllTransactionsSortedDTO
    {
        public List<Transactions>? Transactions { get; set; }

        public AllTransactionsSortedDTO(List<Transactions>? trans1, List<Transactions>? trans2)
        {
            trans1.AddRange(trans2);
            Transactions = trans1.OrderByDescending(x => x.date).ToList();
        }
    }
}

namespace EntityFramework.Models.MoneyTransferDTO
{
    public class TransferSubMoneyModel
    {
        public SubBankAccounts? SubAccountFrom { get; set; }
        public SubBankAccounts? SubAccountTo { get; set; }

        public TransferSubMoneyModel(SubBankAccounts from, SubBankAccounts to)
        {
            SubAccountTo = to;
            SubAccountFrom = from;
        }
    }
}

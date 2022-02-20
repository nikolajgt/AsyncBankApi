namespace EntityFramework.Models.MoneyTransferDTO
{
    public class AllUsersLoansDTO
    {

        public List<Loans>? Loans { get; set; }

        public AllUsersLoansDTO(List<Loans>loans)
        {
            Loans = loans;
        }
    }
}

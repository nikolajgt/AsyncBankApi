using EntityFramework.Models;
using EntityFramework.Models.JWT;
using EntityFramework.Models.MoneyTransferDTO;

namespace EntityFramework.Interface
{
    public interface IBankRepository
    {

        // TEST POST METHODS
        Task<bool> AccountMoneyTransfer(TransferMoneyModel model);
        Task<bool> SubAccountTransfer(TransferSubMoneyModel? model);
        Task CreateNewBank(Bank bank);
        Task<bool> CreateUserUnderBank(string bankname, Users user);
        Task<bool> CreateSubAccountForUser(string userid, SubBankAccounts subBank);  
        Task<bool> AddTransactions(Transactions model);

        // LOGIN //
        Task<Users> Login(string? username, string? password);
        Task<Users> TokenRefreshRevoke(string token);

        //UPDATE
        Task<bool> UpdateListOfUsers(List<Users> users);
        Task<bool> UpdateSingleUser(Users user);

        //Remove
        Task RemoveLoanFromRki(Rki rki);
        Task RemoveLoanFromLoan(Loans loan);


        //GET
        Task<TransactionsDTO> GetUsersTransactions(string? userid);
        Task<Users> GetAllUserData(string userid);
        Task<List<Bank>> GetAllBanks();

        Task<List<Loans>> GetAllUsersLoans();
        Task<List<Loans>> GetAllUserLoans(string userid);
        Task<int?> GetBankID(string bankname);
        Task<TransferSubMoneyModel> GetUserSubAccount(string subaccountfrom, string subaccountto, string userid);



    }
}

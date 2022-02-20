using EntityFramework.Models;
using EntityFramework.Models.DTO;
using EntityFramework.Models.MoneyTransferDTO;

namespace EntityFramework.Interface
{
    public interface IUserService
    {
        Task<bool> TransferMoneyToNewAccount(string useridFrom, string useridTo, int money);
        Task<bool> TransferMoneyToSubAccount(int balance, string toSubAccount, string fromSubAccount, string userid);
        Task<bool> CreateSubAccountForUser(string userid, string subname, int balance, SubBankAccountType type);
        Task<bool> PayBackLoan(int loanid, int balance, string userid);
        Task<GetUserAccountsDTO> GetUserAccounts(string userid);
        Task<bool> ChangeBank(string userid, string bankname);
        Task<AllTransactionsSortedDTO> GetUsersTransactions(string userid);
        Task<AllUsersLoansDTO> AllUserLoans(string userid);
        Task<Users> GetAllUserData(string userid);
    }
}

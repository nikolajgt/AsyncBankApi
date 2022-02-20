using EntityFramework.Models;

namespace EntityFramework.Interface
{
    public interface IAdminService
    {

        Task<TransferMoneyModel> TransferMoneyToNewAccount(string useridFrom, string useridTo, int money);
        Task<bool> TransferMoneyToSubAccount(int balance, string toSubAccount, string fromSubAccount, string userid);
        Task CreateNewBank(string bankname, string address);
        Task<bool> CreateNewUserUnderBank(string bankname, string firstname, string lastname, string username, string password, int balance);
        Task<bool> CreateSubAccountForUser(string userid, string subname, int balance, SubBankAccountType type);
        //void RemoveFromRki(Users? user);
        Task<bool> CreateLoanForUser(string userid, int balance, int months);

        //getters
        Task<Users> GetAllUserData(string userid);
        Task<List<Bank>> GetAllBanks();



    }
}

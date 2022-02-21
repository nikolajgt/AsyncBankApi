using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Models.DTO;
using EntityFramework.Models.MoneyTransferDTO;

namespace EntityFramework.Services
{
    public class UserService : IUserService
    {

        private readonly IBankRepository _repository;
        public UserService(IBankRepository repository)
        {
            _repository = repository;
        }

        //TRANSFER MONEY TO ACCOUNT ON DIFFERENT BANKS
        public async Task<bool> TransferMoneyToNewAccount(string useridFrom, string useridTo, int money)
        {
            var userTo =  await GetAllUserData(useridTo);
            var userFrom =  await GetAllUserData(useridFrom);

            userTo.Balance += money;
            userFrom.Balance -= money;

            var modelUser = new TransferMoneyModel(userTo, userFrom);
            var test = new Transactions(userFrom.Balance, money, userTo.UserID, userFrom.UserID);


            _repository.AddTransactions(test);
            return await _repository.AccountMoneyTransfer(modelUser);
        }

        //TRANSFER MONEY TO YOUR OWN SUB ACCOUNT

        public async Task<bool> TransferMoneyToSubAccount(int balance, string toSubAccount, string fromSubAccount, string userid)
        {
            var response = await _repository.GetUserSubAccount(toSubAccount, fromSubAccount,  userid);

            response.SubAccountFrom.Balance -= balance;
            response.SubAccountTo.Balance += balance;

            return await _repository.SubAccountTransfer(response);
        }

        //Method for paying back the loan money. You can choose between your 3 sub accounts

        public async Task<bool> PayBackLoan(int loanid, int balance, string userid)
        {
            var response =  await _repository.GetAllUserData(userid);
            var loan = response.Loans.OrderBy(x => x.LoanID).ToList();
            foreach(var i in loan)
            {
                Console.WriteLine(i.LoanID);
            }

            switch (loanid)
            {
                case 1:
                    loan[0].PayBackBalance -= balance;
                    break;
                case 2:
                    loan[1].PayBackBalance -= balance;
                    break;
                case 3:
                    loan[2].PayBackBalance -= balance;
                    break;
                default:
                    return false;
            }

            return await _repository.UpdateSingleUser(response);
        }

        public async Task<GetUserAccountsDTO> GetUserAccounts(string userid)
        {
            var response =  await _repository.GetAllUserData(userid);
            var newModel = new GetUserAccountsDTO
            {
                Firstname = response.Firstname,
                Lastname = response.Lastname,
                MainAccountBalance = response.Balance,
                SubBankAccounts = response.SubBankAccounts,
            };
            return newModel;
        }

        // User can sign up for bank change
        public async Task<bool> ChangeBank(string userid, string bankname)
        {
            var response = await GetAllUserData(userid);
            var bankid = await GetBankID(bankname);
            response.BankID = bankid;
            return await _repository.UpdateSingleUser(response);
        }

        //CREATES A SUB ACCOUNT FOR USER
        public async Task<bool> CreateSubAccountForUser(string userid, string subname, int balance, SubBankAccountType type)
        {
            var subaccount = new SubBankAccounts(subname, balance, type);
            return await _repository.CreateSubAccountForUser(userid, subaccount);
        }


        public async Task<AllTransactionsSortedDTO> GetUsersTransactions(string userid)
        {
            var response = await _repository.GetUsersTransactions(userid);
            return new AllTransactionsSortedDTO(response.Added, response.Substracted);
        }

        public async Task<AllUsersLoansDTO> AllUserLoans(string userid)
        {
            var response = await _repository.GetAllUserLoans(userid);
            return new AllUsersLoansDTO(response);
        }

        public async Task<Users> GetAllUserData(string userid)
        {
            return await _repository.GetAllUserData(userid);
        }

        private async Task<int?> GetBankID(string bankname)
        {
            return await _repository.GetBankID(bankname);
        }


    }
}

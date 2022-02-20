using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Models.MoneyTransferDTO;

namespace EntityFramework.Services
{
    public class AdminService : IAdminService
    {

        private readonly IBankRepository _repository;
        public AdminService(IBankRepository repository)
        {
            _repository = repository;
        }


        //TRANSFER MONEY TO ACCOUNT ON DIFFERENT BANK

        public async Task<TransferMoneyModel> TransferMoneyToNewAccount(string useridFrom, string useridTo, int money)
        {
            var userTo =  await GetAllUserData(useridTo);
            var userFrom = await GetAllUserData(useridFrom);

            userTo.Balance += money;
            userFrom.Balance -= money;

            var modelUser = new TransferMoneyModel(userTo, userFrom);
            var added = new Transactions(userFrom.Balance, money, userTo.UserID, userFrom.UserID);

            await _repository.AddTransactions(added);
            await _repository.AccountMoneyTransfer(modelUser);

            return modelUser;
        }

        //TRANSFER MONEY TO YOUR OWN SUB ACCOUNT

        public async Task<bool> TransferMoneyToSubAccount(int balance, string toSubAccount, string fromSubAccount, string userid)
        {
            var response = await _repository.GetUserSubAccount(toSubAccount, fromSubAccount, userid);

            response.SubAccountFrom.Balance -= balance;
            response.SubAccountTo.Balance += balance;

            return await _repository.SubAccountTransfer(response);
            
        }


        
        //CREATES A NEW BANK
        public async Task CreateNewBank(string bankname, string address)
        {
            var bank = new Bank(bankname, address);

            await _repository.CreateNewBank(bank);
        }


        //CREATES A NEW USER UNDER BANK
        public async Task<bool> CreateNewUserUnderBank(string bankname, string firstname, string lastname, string username, string password, int balance)
        {
            var user = new Users(firstname, lastname, username, password, balance);

            return await _repository.CreateUserUnderBank(bankname, user);
        }


        //CREATES A SUB ACCOUNT FOR USER
        public async Task<bool> CreateSubAccountForUser(string userid, string subname, int balance, SubBankAccountType type)
        {
            var subaccount = new SubBankAccounts(subname, balance, type);
            return await _repository.CreateSubAccountForUser(userid, subaccount);
        }



        //CREATES LOAN FOR USER
        public async Task<bool> CreateLoanForUser(string userid, int balance, int months)
        {
            var user = await GetAllUserData(userid);
            var rkiResponse = await RkiCheck(user);
            var loanChecker = await LoanChecker(user);


            if (loanChecker || rkiResponse)
                return false;
     

            var loan = new Loans(balance, months);

            user.Loans.Add(loan);

            return await _repository.UpdateSingleUser(user);
        }

        public async Task<Users> GetAllUserData(string userid)
        {
            return await _repository.GetAllUserData(userid);
        }

        public async Task<List<Bank>> GetAllBanks()
        {
            return await _repository.GetAllBanks();
        }



        //CHECKS IF YO HAVE 3 OR OVER LOANS, IF YOU HAVE RETURN FALSE AND NO LOAN

        private async Task<bool> LoanChecker(Users user)
        {
            if(user == null)
            {
                return false;
            }else if(user.Loans.Count >= 3)
            {
                return true;
            }


            return false;
        }

        //IF THE LOAN IS EXPIRED AND NOT PAYED BACK, SEND IT TO RKI

        private async Task<bool> RkiCheck(Users user)
        {
            if (user.Loans == null)
                return false;

            foreach (var item in user.Loans)
            {
                if (item.Expire <= DateTime.Now && item.PayBackBalance > 0)
                {
                    user.Rki.Add(new Rki(item.LoanID, item.Issued, item.Expire, item.PayBackBalance, item.TotalLoanOfMoney, user.UserID));

                    return await _repository.UpdateSingleUser(user);
                }
            }
            return false;
            CheckForRkiRemoval(user);
        }

        private async Task<bool> CheckForRkiRemoval(Users? user)
        {
            var loan = user.Loans.FirstOrDefault(x => x.PayBackBalance <= 0);
            var rki = user.Rki.FirstOrDefault(x => x.PayBackBalance <= 0);
            user.Loans.Remove(loan);
            user.Rki.Remove(rki);
            
            return await _repository.UpdateSingleUser(user);
        }


    }
}

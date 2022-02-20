using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Models.MoneyTransferDTO;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly MyContext _bankRepository;
        public BankRepository(MyContext backRepository)
        {
            _bankRepository = backRepository;
        }


        // Send money to user in different bank
        public async Task<bool> AccountMoneyTransfer(TransferMoneyModel model)
        {
            var responseTo = await _bankRepository.Users.FirstOrDefaultAsync(x => x.UserID == model.userTo.UserID);
            var responseFrom = await _bankRepository.Users.FirstOrDefaultAsync(x => x.UserID == model.userFrom.UserID);

            if (responseTo == null && responseFrom == null)
                return false;

            responseTo.Balance = model.userTo.Balance;
            responseFrom.Balance = model.userFrom.Balance;
            await _bankRepository.SaveChangesAsync();
            return true;
        }

        //Send money to your subaccounts
        public async Task<bool> SubAccountTransfer(TransferSubMoneyModel? model)
        {
            var userSubTo = await _bankRepository.SubBankAccounts.FirstOrDefaultAsync(x => x.SubBankAccountID == model.SubAccountTo.SubBankAccountID);
            var userSubFrom = await _bankRepository.SubBankAccounts.FirstOrDefaultAsync(x => x.SubBankAccountID == model.SubAccountFrom.SubBankAccountID);


            if (userSubTo == null && userSubFrom == null)
                return false;

            userSubTo.Balance = model.SubAccountTo.Balance;
            userSubFrom.Balance = model.SubAccountFrom.Balance;

            await _bankRepository.SaveChangesAsync();
            return true;
        }


        // Creates a new bank
        public async Task CreateNewBank(Bank bank)
        {
            await _bankRepository.Banks.AddAsync(bank);
            await _bankRepository.SaveChangesAsync();
        }

        //Creates user under a bank
        public async Task<bool> CreateUserUnderBank(string bankname, Users user)
        {
            var response = await _bankRepository.Banks.FirstOrDefaultAsync(x => x.BankName == bankname);
            if (response == null)
                return false;

            response.Users?.Add(user);
            await _bankRepository.SaveChangesAsync();
            return true;
        }

        //Creates subaccount for user
        public async Task<bool> CreateSubAccountForUser(string userid, SubBankAccounts subBank)
        {
            var response =  await _bankRepository.Users.SingleOrDefaultAsync(x => x.UserID == userid);
            if (response == null)
                return false;

            response.SubBankAccounts?.Add(subBank);
            return (await _bankRepository.SaveChangesAsync()) > 0;
        }

        public async Task<bool> AddTransactions(Transactions model)
        {
            await _bankRepository.Transactions.AddAsync(model);
            return (await _bankRepository.SaveChangesAsync()) > 0;

        }



        // LOGIN METHODS // JWT  ///
        public async Task<Users> Login(string? username, string? password)
        {
            var response = await _bankRepository.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            if (response == null)
                return null;

            return response;
        }

        public async Task<Users> TokenRefreshRevoke(string token)
        {
            var response = await _bankRepository.Users.FirstOrDefaultAsync(x => x.RefreshToken.Any(y => y.Token == token));
            return response;
        }




        //UPDATES

        // Update list of users. Havent really tested it, espically becuase it SAID(Yes past tense) add instead of update
        public async Task<bool> UpdateListOfUsers(List<Users> users)
        {
            foreach (Users user in users)
            {
                _bankRepository.Users.Update(user);
            }
            return (await _bankRepository.SaveChangesAsync()) > 0;
        }


        //Updates a single user
        public async Task<bool> UpdateSingleUser(Users user)
        {
            try
            {
                _bankRepository.Users.Update(user);
               
                return (await _bankRepository.SaveChangesAsync()) > 0;
            }
            catch (Exception e)
            {
                return false;
                Console.WriteLine(e.Message);
            }
        }


        //REMOVE

        public async Task RemoveLoanFromRki(Rki rki)
        {
            _bankRepository.Rki.Remove(rki);
            await _bankRepository.SaveChangesAsync();
        }

        public async Task RemoveLoanFromLoan(Loans loan)
        {
            _bankRepository.Loans.Remove(loan);
            await _bankRepository.SaveChangesAsync();
        }



        // GETTERS

        //Gets all userdata and fk from userid
        public async Task<Users> GetAllUserData(string userid)                                              //Acts wierd when in async, as the data is to big or it simpliy dont work. No exception and it dont hit the next breakpoint. 
        {                                                                                                     //It just stops as nothing happend
            try
            {
                var response = await _bankRepository.Users.Where(x => x.UserID == userid).SingleOrDefaultAsync();
                if (response == null)
                    return null;

                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Get all banks
        public async Task<List<Bank>> GetAllBanks()
        {
            return await _bankRepository.Banks.ToListAsync();
        }

        //Gets all users subaccounts
        public async Task<TransferSubMoneyModel> GetUserSubAccount(string subaccountfrom, string subaccountto, string userid)
        {
            var response = await GetAllUserData(userid);

            var subAccountFrom =  response.SubBankAccounts.FirstOrDefault(x => x.SubAccountName == subaccountfrom);
            var subAccountTo =  response.SubBankAccounts.FirstOrDefault(x => x.SubAccountName == subaccountto);

            var newModel = new TransferSubMoneyModel(subAccountFrom, subAccountTo);

            return newModel;
        }

 
        //Gets all users loans
        public async Task<List<Loans>> GetAllUsersLoans()
        {
            return await _bankRepository.Loans.ToListAsync();
        }

        public async Task<List<Loans>> GetAllUserLoans(string userid)
        {
            return await _bankRepository.Loans.Where(x => x.UserID == userid).ToListAsync();
        }

        public async Task<int?> GetBankID(string bankname)
        {
            var response = await _bankRepository.Banks.FirstOrDefaultAsync(x =>x.BankName == bankname);
            return response?.BankID;
        }

        public async Task<TransactionsDTO> GetUsersTransactions(string? userid)
        {
            var responseSubstracted = await _bankRepository.Transactions.Where(x => x.userID.Contains(userid)).ToListAsync();
            var responseAdded = await _bankRepository.Transactions.Where(x => x.reciver.Contains(userid)).ToListAsync();
            return new TransactionsDTO(responseAdded, responseSubstracted);
        }



        //PRIVATES

        //CHECKS IF RECORD EXIST IN RKI
        private async Task<bool> CheckRkiIfRecordExist(int? loanid)
        {
            var response = await _bankRepository.Rki.FirstOrDefaultAsync(x => x.LoanID == loanid);
            if (response != null)
                return false;

            return true;
        }
    }
}

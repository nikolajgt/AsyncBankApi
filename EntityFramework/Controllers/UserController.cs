using EntityFramework.Interface;
using EntityFramework.Models;
using EntityFramework.Models.DTO;
using EntityFramework.Models.JWT;
using EntityFramework.Models.MoneyTransferDTO;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EntityFramework.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _service;
 
        public UserController(IUserService service, IConfiguration config)
        {
            _service = service;
        }

        [HttpPost("Send-money-account")]
        public async Task<bool> TransferSubMoney([FromBody] RevokeTokenRequest token, string subaccountto, string subacccountfrom, int balance)
        {
            var id = JWTDecrypterId(token.Token);
            return await _service.TransferMoneyToSubAccount(balance, subaccountto, subacccountfrom, id);
        }

        [HttpPost("Transfer-money-user")]
        public async Task<bool> TransferMoney([FromBody] RevokeTokenRequest token, string useridTo, int balance)
        {
            var id = JWTDecrypterId(token.Token);
            return await _service.TransferMoneyToNewAccount(id, useridTo, balance);
        }

        [HttpPost("Create-subaccount-user")]
        public async Task<bool> CreateSubAccountForUser([FromBody] RevokeTokenRequest token, int subbalance, string subname, SubBankAccountType type)
        {
            var id = JWTDecrypterId(token.Token);
            return await _service.CreateSubAccountForUser(id, subname, subbalance, type);
        }

        [HttpPost("Pay-back-loan")]
        public async Task<bool> PayBackLoan([FromBody] RevokeTokenRequest token, int loanid, int balance)
        {
            var id = JWTDecrypterId(token.Token);
            return await _service.PayBackLoan(loanid, balance, id);
        }
 
        [HttpGet("Get-user-sub-accounts/{token}")]
        public async Task<GetUserAccountsDTO> GetUserAccounts(string token)
        {
            var id = JWTDecrypterId(token);
            return  await _service.GetUserAccounts(id);
        }

        [HttpGet("Get-user-transactions/{token}")]
        public async Task<AllTransactionsSortedDTO> GetUserTransactions(string token)
        {
            var id = JWTDecrypterId(token);
            return await _service.GetUsersTransactions(id);
        }


        [HttpGet("Get-user-loans/{token}")]
        public async Task<AllUsersLoansDTO> GetUserLoans(string token)
        {
            var id = JWTDecrypterId(token);
            return await _service.AllUserLoans(id);
        }

        [HttpPut("Change bank")]
        public async Task<bool> ChangeBank([FromBody] RevokeTokenRequest token, string bankname)
        {
            var id = JWTDecrypterId(token.Token);
            return await _service.ChangeBank(id, bankname);
        }

        private string JWTDecrypterId(string token)
        {
            if (token == null)
                return null;
            Console.WriteLine("TOOOOOOKEN ---- > ", token);
            var parsedToken = new JwtSecurityToken(token);
            return parsedToken.Claims.FirstOrDefault().Value;
        }
    }
}

using EntityFramework.Interface;
using EntityFramework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpPost("Send money to sub account")]
        public async Task TransferSubMoney(string subaccountto, string subacccountfrom, int balance, string userid)
        {
            await _service.TransferMoneyToSubAccount(balance, subaccountto, subacccountfrom, userid);
        }

        [HttpPost("Transfer money to a user")]
        public async Task TransferMoney(string useridTo, string useridFrom, int balance)
        {
            await _service.TransferMoneyToNewAccount(useridFrom, useridTo, balance);
        }

        [HttpPost("Create loan for user")]
        public async Task<bool> CreateLoan(string user, int month, int balance)
        {
            return await _service.CreateLoanForUser(user, balance, month);
        }

        [HttpPost("Post Bank")]
        public async Task CreateNewBank(string bankname, string address)
        {
            await _service.CreateNewBank(bankname, address);
        }

        [HttpPost("Post User under Bank")]
        public async Task<bool> CreateUserUnderBank(string bankname, string firstname, string lastname, string username, string password, int balance)
        {
            return await _service.CreateNewUserUnderBank(bankname, firstname, lastname, username, password, balance);
        }

        [HttpPost("Post subaccount for user")]
        public async Task<bool> CreateSubAccountForUser(string userid, int subbalance, string subname, SubBankAccountType type)
        {
            return await _service.CreateSubAccountForUser(userid, subname, subbalance, type);
        }

        [HttpGet("Get All User Data")]
        public async Task<Users> GetAllUserData(string userid)
        {
            return await _service.GetAllUserData(userid);
        }

        [HttpGet("Get All Banks")]
        public async Task<List<Bank>> GetAllBanks()
        {
            return await _service.GetAllBanks();
        }
    }
}

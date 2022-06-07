using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Repositories;
using ReportSystemData.Response;
using ReportSystemData.Service.Base;
using ReportSystemData.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface IAccountService : IBaseService<Account>
    {
        List<Account> GetAllAccount();
        Account Login(LoginPara login);
        bool CheckAvaiAccount(string email);
        Task<SuccessResponse> RegisterAsync(CreateAccountViewModel account);
        Account GetAccountByID(string email);
    }
    public partial class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IAccountInfoService _accountInfoService;
        public AccountService(DbContext context, IAccountRepository repository, IAccountInfoService accountInfoService) : base(context, repository)
        {
            _dbContext = context;
            _accountInfoService = accountInfoService;
        }
        public List<Account> GetAllAccount()
        {
            var account = Get().Include(role => role.Role).Include(info => info.AccountInfo).ToList();
            return account;
        }

        public Account Login(LoginPara login)
        {
            var account = Get().Where(acc => acc.Email.Equals(login.email) && acc.Password.Equals(login.password))
                .Include(role => role.Role).Include(info => info.AccountInfo).FirstOrDefault();
            if (account == null)
            {
                throw new ErrorResponse("Invalid email or password!!!", (int)HttpStatusCode.NotFound);
            }
            return account;
        }
        public Account GetAccountByID(string email)
        {
            var acc = Get().Where(ac => ac.Email.Equals(email))
                .Include(r => r.AccountInfo)
                .Include(r => r.Role)
                .FirstOrDefault();
            if(acc != null)
            {
                return acc;
            }
            return null;
        }

        public bool CheckAvaiAccount(string email)
        { 
            var listAccount = Get().ToList();
            foreach (Account account in listAccount)
            {
                if(account.Email.Equals(email))
                { 
                    return true;
                }
            }
            return false;
        }

        public async Task<SuccessResponse> RegisterAsync(CreateAccountViewModel account)
        {
            var checkAccount = CheckAvaiAccount(account.Email);
            if(!checkAccount)
            {
                var accountTmp = new Account()
                {
                    Email = account.Email,
                    Password = account.Password,
                    RoleId = account.RoleId
                };
                var accountInfoTmp = new AccountInfo()
                {
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    Username = account.Username,
                    Address = account.Address,
                    IdentityCard = account.IdentityCard
                };
                await CreateAsyn(accountTmp);
                var check = await _accountInfoService.CreateAccountInfoAsync(accountInfoTmp);
                if(check)
                {
                    var acc = GetAccountByID(account.Email);
                    if(acc != null)
                    {
                        return new SuccessResponse((int)HttpStatusCode.OK, "Create Success");
                    }
                    throw new ErrorResponse("Email not found!!!", (int)HttpStatusCode.NotFound);
                }
                throw new ErrorResponse("Your Email is already exist. Please choose another email!!!", (int)HttpStatusCode.NoContent);
            }
            throw new ErrorResponse("Your Email is already exist. Please choose another email!!!", (int)HttpStatusCode.NoContent);
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReportSystemData.Models;
using ReportSystemData.Repositories;
using ReportSystemData.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSystemData.Service
{
    public partial interface IAccountInfoService : IBaseService<AccountInfo>
    {
        Task<bool> CreateAccountInfoAsync(AccountInfo accInfo);
    }
    public class AccountInfoService : BaseService<AccountInfo>, IAccountInfoService
    {
        public AccountInfoService(DbContext context, IAccountInfoRepository repository) : base(context, repository)
        {
            _dbContext = context;
        }

        public async Task<bool> CreateAccountInfoAsync(AccountInfo accInfo)
        {
            var checkAccountInfo = CheckAvaiAccountInfo(accInfo.Email);
            if (!checkAccountInfo)
            {
                await CreateAsyn(accInfo);
                return true;
            }
            return false;
        }

        public bool CheckAvaiAccountInfo(string email)
        {
            var check = Get().Where(acc => acc.Email.Equals(email)).FirstOrDefault();
            if (check != null)
            {
                return true;
            }
            return false;
        }
    }
}

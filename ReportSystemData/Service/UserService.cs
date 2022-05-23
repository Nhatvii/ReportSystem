using Microsoft.EntityFrameworkCore;
using ReportSystemData.Dtos.User;
using ReportSystemData.Models;
using ReportSystemData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportSystemData.Service
{
    //public class UserService : IUser
    //{
    //    private readonly _24HReportSystemContext _context;
    //    public UserService(_24HReportSystemContext context)
    //    {
    //        _context = context;
    //    }
    //    public IEnumerable<User> GetAllUser()
    //    {
    //        return _context.User.Include(user => user.Role).Include(user => user.UserInfo).ToList();
    //    }

    //    public User Login(string email, string password)
    //    {
    //        var listUser = _context.User;
    //        var tmp = new User();
    //        if (listUser != null)
    //        {
    //            foreach (User user in listUser)
    //            {
    //                if (user.Email.Trim().Equals(email) && user.Password.Trim().Equals(password))
    //                {
    //                    tmp = user;
    //                    break;
    //                }
    //            }
    //        }
    //        if (tmp.Email != null)
    //        {
    //            return tmp;
    //        }
    //        return null;
    //    }

    //    public bool Register(CreateUserDTO createUser)
    //    {
    //        foreach (User tmp in _context.User.ToList())
    //        {
    //            if (tmp.Email.Trim().Equals(createUser.Email.Trim()))
    //            {
    //                return false;
    //            }
    //        }
    //        if (createUser.Email.Equals("string") || createUser.Email == null)
    //        {
    //            return false;
    //        }
    //        if (createUser.Password.Equals("string") || createUser.Password == null)
    //        {
    //            return false;
    //        }
    //        if (createUser.Username.Equals("string") || createUser.Username == null)
    //        {
    //            return false;
    //        }
    //        if (createUser.Address.Equals("string") || createUser.Address == null)
    //        {
    //            return false;
    //        }
    //        if (createUser.PhoneNumber == 0 || createUser.PhoneNumber == null)
    //        {
    //            return false;
    //        }
    //        if (createUser.IdentityCard == 0 || createUser.IdentityCard == null)
    //        {
    //            return false;
    //        }
    //        var flag = false;
    //        foreach (Role tmp in _context.Role.ToList())
    //        {
    //            if (tmp.RoleId == createUser.RoleId)
    //            {
    //                flag = true;
    //            }
    //        }
    //        if (!flag)
    //        {
    //            return false;
    //        }

    //        var user = new User()
    //        {
    //            Email = createUser.Email,
    //            Password = createUser.Password,
    //            RoleId = createUser.RoleId
    //        };
    //        var userInfo = new UserInfo()
    //        {
    //            Email = createUser.Email,
    //            PhoneNumber = createUser.PhoneNumber,
    //            Username = createUser.Username,
    //            Address = createUser.Address,
    //            IdentityCard = createUser.IdentityCard
    //        };
    //        _context.User.Add(user);
    //        _context.UserInfo.Add(userInfo);
    //        _context.SaveChanges();
    //        return true;
    //    }
    //}
}

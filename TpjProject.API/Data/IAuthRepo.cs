using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TpjProject.API.Models; 
namespace TpjProject.API.Data
{
    public interface IAuthRepo
    {
         Task<User> Register ( User user , string passwrod);
         Task<User> Login (string username , string password);
         Task<bool> UserExist(string usrname); 

    }
}
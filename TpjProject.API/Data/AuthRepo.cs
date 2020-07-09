
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TpjProject.API.Data;
using Microsoft.EntityFrameworkCore;
using TpjProject.API.DTOs;
using TpjProject.API.Models;

namespace TpjProject.API.Data
{
    public class AuthRepo : IAuthRepo
    {
        private DataContext _context;
        public AuthRepo(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Register(User user, string passwrod)
        {
            byte[] PassHash, PassSalt;
            CreateHashedPass(passwrod, out PassHash, out PassSalt);
            user.PassHash = PassHash;
            user.PassSalt = PassSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        // Hashing and salting password to make the password secure  
        public void CreateHashedPass(string password, out byte[] PassHash,out  byte[] PassSalt)
        {

            using (var hmacsh = new System.Security.Cryptography.HMACSHA512())
            {
                PassSalt = hmacsh.Key; // This key will be used to unlock pass 
                PassHash = hmacsh.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }


        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
                return null;

            if (!VerifyPass(password, user.PassHash, user.PassSalt))
                return null;

            return user;

        }
        public bool VerifyPass(string password, byte[] PassHash, byte[] PassSalt)
        {

            using (var hmacsh = new System.Security.Cryptography.HMACSHA512(PassSalt))
            {

                var ComputedHash = hmacsh.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != PassHash[i]) return false;
                }
            }

            return true;
        }
        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;

            return false;
        }
    }
}
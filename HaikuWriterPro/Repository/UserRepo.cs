using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Repository
{
    public class UserRepo
    {
        private readonly HaikuDbContext _dbContext;

         public UserRepo(HaikuDbContext context){
            this._dbContext = context;
        }

        public User Register(User newUser)
        {
            
            var newUser1 = _dbContext.Users.Add(newUser);// addd the new user to the Db
            _dbContext.SaveChanges();// save the change.
            return _dbContext.Users.FirstOrDefault(u => u.Username == newUser.Username);// default is null
        }

        public bool UserExists(string username)
        {
            if (_dbContext.Users.Where(p => p.Username == username).FirstOrDefault() != null)
            {
                Console.WriteLine(username);
                return true;
            }
            else
            {

                return false;
            }
        }

        public User GetUserByUsername(string username)
        {
            User foundUser = _dbContext.Users.FirstOrDefault(p => p.Username == username);
            return foundUser;
        }

        public User UpdateUserInfo(RawUser user){

            User updatedUser = _dbContext.Users.FirstOrDefault(p => p.Username == user.Username);

            if(updatedUser == null){
                return null;
            }
            updatedUser.Email = user.Email;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.FaceBookName = user.FaceBookName;
            updatedUser.TwitterName = user.TwitterName;
            updatedUser.AdminStatus = user.AdminStatus;

            _dbContext.SaveChanges();
            
            return updatedUser;
        }

        public void UpdatePassword(){

           _dbContext.SaveChanges();
        }
        
        public List<User> GetAllUser(){

            List<User> userlist = _dbContext.Users.ToList();
            return userlist;
        }

    }
}
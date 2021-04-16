using Repository;
using Models;
using System.Collections.Generic;
using System;

namespace BusinessLogic
{

    public class UserMethods{

        private readonly UserRepo _userRepo;
        private readonly Hasher hasher = new Hasher();
        public UserMethods( UserRepo userrepo)
        {
            this._userRepo = userrepo;
        }


        public User UserRegister(RawUser user){

            if(_userRepo.GetUserByUsername(user.Username) != null){   
                Console.WriteLine("user exists");
                User temp = new User();
                return temp;
            }
            User newUser = hasher.hashPassword(user.Password);
            newUser.AdminStatus = user.AdminStatus;
            newUser.Username = user.Username;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.FaceBookName = user.FaceBookName;
            newUser.TwitterName = user.TwitterName;
            

            User registerUser = _userRepo.Register(newUser);
            if(registerUser == null){
                Console.WriteLine("DB error");
                return null;
            }

            registerUser.PasswordHash = null;
            registerUser.PasswordSalt = null;
            return registerUser;
        }

        public User UserLogin(string username, string password){
            if (!_userRepo.UserExists(username))
            {   Console.WriteLine("user doesn't exists " + username);
                return null;
            }
            else{
                
                User foundUser = _userRepo.GetUserByUsername(username);

                // hash the provided password with the key from the found user
                byte[] hash = hasher.HashTheUsername(password, foundUser.PasswordSalt);

                if (CompareTwoHashes(foundUser.PasswordHash, hash))
                {
                    foundUser.PasswordHash = null;
                    foundUser.PasswordSalt = null;
                    return foundUser;
                }
                else return null;

            }
        }

        /// <summary>
        /// gets two arrays and compire them and return true or false
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        private bool CompareTwoHashes(byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            //compare the hash of the inputted password and the found user
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public User GetUser(string username){

            User newuser = _userRepo.GetUserByUsername(username);

            return newuser;

        }

        public User UpdateUserInfo(RawUser user){

            User newUser = _userRepo.UpdateUserInfo(user);
            return newUser;
        }

        public bool UpdatePassword(string username, string password, string newPassword){
            
            User newUser = _userRepo.GetUserByUsername(username);

            if(newUser == null){
                return false;
            }

            byte[] hash = hasher.HashTheUsername(password, newUser.PasswordSalt);

                if (CompareTwoHashes(newUser.PasswordHash, hash))
                {
                    User anotherUser =  hasher.hashPassword(newPassword);
                    newUser.PasswordHash = anotherUser.PasswordHash;
                    newUser.PasswordSalt = anotherUser.PasswordSalt;

                    _userRepo.UpdatePassword();
                    Console.WriteLine("pw updated");
                    return true;
                }
                else{
                    return false;
                }
        }

        public List<User> GetAllUser(){
            
            List<User> userlist = _userRepo.GetAllUser();

            return userlist;
        }
    }

}
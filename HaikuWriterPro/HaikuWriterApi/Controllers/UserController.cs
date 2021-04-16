using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using BusinessLogic;

namespace HaikuWriterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserMethods _userMethods;

        public UserController(UserMethods userMethods){
            this._userMethods = userMethods;
        }
        
       [HttpPost("signup")]
        public ActionResult<User> signup([FromBody] RawUser user){

            Console.WriteLine("user");
            Console.WriteLine(user.Email);

            User newUser = _userMethods.UserRegister(user);
            if(newUser.Username == null){
                return StatusCode(422, "Sorry, This User Already Exists");
            }
            if(newUser == null){
                return StatusCode(409, "Sorry, Sign-up Was Unsuccessful");
            }
            return newUser;
        } 

        [HttpGet("login/{username}/{password}")]
        public ActionResult<User> login(string username, string password){


            User newUser = _userMethods.UserLogin(username, password);
            if(newUser == null){
                return StatusCode(409, "Username or Password Did Not Match Our Record");
            }
            return newUser;
        } 

        [HttpGet("getuserbyusername/{username}")]
        public ActionResult<User> Getuser(string username){

            User newUser = _userMethods.GetUser(username);
            return newUser;
        } 

        [HttpPost("updateuser")]
        public ActionResult<User> UpdateUserInfo(RawUser user){


            User newUser = _userMethods.UpdateUserInfo(user);
            
            return newUser;
        } 

        /// <summary>
        /// For changing/updating user's password 
        /// take username, password, and newpassword as input and returns bool true if updated succesfully
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpGet("updatepassword/{username}/{password}/{newPassword}")]
        public ActionResult<bool> UpdatePassword(string username, string password, string newPassword){

            bool updated  = _userMethods.UpdatePassword(username, password, newPassword);
            
            return updated;
        } 


        /// <summary>
        /// returns list of all user 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllUser")]
        public ActionResult<List<User>> GellAllUser(){

            return _userMethods.GetAllUser();
        }

    }
}

//ZmGDxcwSFCGveQkIp1via/nULRSdr
//KnGKFPU/AEZoM2hUjfJf
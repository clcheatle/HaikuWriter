using System.Collections.Generic;
using System.Linq;
using System;
using Models;

namespace Repository
{
    public class HaikuRepo
    {
        private readonly HaikuDbContext _dbContext;

        public HaikuRepo(HaikuDbContext context)
        {
            this._dbContext = context;
        }

        /// <summary>
        /// This method will add a haiku line to the database and then return the
        /// newly create haiku line back
        /// </summary>
        /// <param name="haikuline"></param>
        /// <returns></returns>
        public HaikuLine SaveLine(HaikuLine haikuline)
        {
            var newLine = _dbContext.HaikuLines.Add(haikuline);//add the new line to the database
            _dbContext.SaveChanges();
            return _dbContext.HaikuLines.FirstOrDefault(h => h.HaikuLineId == haikuline.HaikuLineId);
        }

        /// <summary>
        /// This method will add a haiku to the database and return the saved haiku back
        /// </summary>
        /// <param name="haiku"></param>
        /// <returns></returns>
        public Haiku SaveHaiku(Haiku haiku)
        {
            var newHaiku = _dbContext.Haikus.Add(haiku);
            _dbContext.SaveChanges();
            return _dbContext.Haikus.FirstOrDefault(h => h.HaikuId == haiku.HaikuId);
        }

        /// <summary>
        /// This method will return a haiku line that has five syllables
        /// </summary>
        /// <returns></returns>
        public HaikuLine GetHaiku5()
        {
            Random random = new Random();
            int numrows = _dbContext.HaikuLines.Where(h => h.Syllable == 5).Select(h => h.HaikuLineId).Count();
            var rand = random.Next(numrows);
            var line = _dbContext.HaikuLines.Where(h => h.Syllable == 5).OrderBy(h => h.HaikuLineId).Skip(rand).Take(1).First();
            if (line.Syllable == 5)
            {
                return line;
            }
            return null;
        }

        /// <summary>
        /// This method will return a haiku line that has 7 syllables
        /// </summary>
        /// <returns></returns>
        public HaikuLine GetHaiku7()
        {
            Random random = new Random();
            int numrows = _dbContext.HaikuLines.Where(h => h.Syllable == 7).Select(h => h.HaikuLineId).Count();
            var rand = random.Next(numrows);
            var line = _dbContext.HaikuLines.Where(h => h.Syllable == 7).OrderBy(h => h.HaikuLineId).Skip(rand).Take(1).First();
            if (line.Syllable == 7)
            {
                return line;
            }
            return null;
        }

        /// <summary>
        /// This method will check to see if the haiku line that is retrieved from the database is different from the haiku line
        /// that was give, if it is the same, it will query the db for another line and check again. If it is different it will return the line
        /// </summary>
        /// <param name="alreadyUsed"></param>
        /// <returns></returns>
        public HaikuLine GetHaiku5(HaikuLine alreadyUsed)
        {
            //get haiku line from DB
            HaikuLine newHaikuLine = GetHaiku5();

            //If the haikuline is not the same as the first line, return
            if (newHaikuLine != alreadyUsed)
            {
                return newHaikuLine; //Passes the new haiku line to the caller
            }
            else
            {
                //If the lines are the same, recursively call the method again
                // It will check if the new line recieved is the same or not
                // It will call the method again if they are the same again
                // It will return here, if they are not the same.
                return GetHaiku5(alreadyUsed);
            }
        }

        /// <summary>
        /// method that queries the database for a list of unapproved haikulines
        /// </summary>
        /// <returns></returns>
        public List<HaikuLine> GetUnapprovedHaikuLines()
        {
            List<HaikuLine> haikuLines = _dbContext.HaikuLines
            .Where(hl => hl.Approved == false).ToList();
            return haikuLines;
        }

        /// <summary>
        /// This method will query the db for a list of approved haikus
        /// </summary>
        /// <returns></returns>
        public List<Haiku> GetHaikus(){

            List<Haiku> haikus = _dbContext.Haikus
            .Where(h => h.Approved == true).ToList();
            return haikus;
        }
        
        /// <summary>
        /// Method that quieries the database for a list of unapporved haikus
        /// </summary>
        /// <returns></returns>
        public List<Haiku> GetUnapprovedHaikus()
        {
            List<Haiku> haikus = _dbContext.Haikus
            .Where(h => h.Approved == false).ToList();
            return haikus;
        }

        /// <summary>
        /// Method that will query the database for the haiku line that matches the id sent
        /// Then will change the approval status to true and send back the haiku's approval status
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        public bool ApproveHaikuLine(int hlid)
        {
            HaikuLine haikuLine = _dbContext.HaikuLines.Where(hl => hl.HaikuLineId == hlid).FirstOrDefault();
            haikuLine.Approved = true;
            _dbContext.SaveChanges();
            bool haikuLineApproval = _dbContext.HaikuLines.Where(h1 => h1.HaikuLineId == hlid).FirstOrDefault().Approved;

            return haikuLineApproval;
        }

        /// <summary>
        /// Method will query the database for a haiku and then set the approval status
        /// to true then return the approval vaule
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        public bool ApproveHaiku(int hlid)
        {
            Haiku haiku = _dbContext.Haikus.Where(h => h.HaikuId == hlid).FirstOrDefault();
            haiku.Approved = true;
            _dbContext.SaveChanges();
            bool haikuLineApproval = _dbContext.Haikus.Where(h => h.HaikuId == hlid).FirstOrDefault().Approved;

            return haikuLineApproval;
        }

        /// <summary>
        /// This method will query the database for the haiku line by given id and then delete it from the 
        /// database. If the operation was successful, return true
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        public bool DeleteHaikuLine(int hlid)
        {
            bool deletionSuccessful = false;

            HaikuLine haikuLine = _dbContext.HaikuLines.Where(hl => hl.HaikuLineId == hlid).FirstOrDefault();
            _dbContext.HaikuLines.Remove(haikuLine);
            _dbContext.SaveChanges();
            haikuLine = _dbContext.HaikuLines.Where(hl => hl.HaikuLineId == hlid).FirstOrDefault();

            if (haikuLine == null)
            {
                deletionSuccessful = true;
            }

            return deletionSuccessful;
        }

        /// <summary>
        /// This method will query the database for a haiku by given haikuid and then delete it from the database
        /// /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        public bool DeleteHaiku(int hid)
        {
            System.Console.Write("made it to repo");

            bool deletionSuccessful = false;

            Haiku haiku = _dbContext.Haikus.Where(h => h.HaikuId == hid).FirstOrDefault();
            _dbContext.Haikus.Remove(haiku);
            _dbContext.SaveChanges();
            haiku = _dbContext.Haikus.Where(h => h.HaikuId == hid).FirstOrDefault();

            if (haiku == null)
            {
                deletionSuccessful = true;
            }

            return deletionSuccessful;
        }

        /// <summary>
        /// Will query the database for a haiku line based on given haiku id
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        public HaikuLine GetHaikuLine(int hlid)
        {
            HaikuLine hl = _dbContext.HaikuLines.Where(h => h.HaikuLineId == hlid).FirstOrDefault();
            return hl;
        }

        /// <summary>
        /// This method will query the database for a list of user favorites and return that list
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Haiku> GetUserFavorites(string username)
        {
            List<Haiku> favs = _dbContext.UserFavs.Where(uf => uf.User.Username == username).Select(uf => uf.Haiku).ToList();
            return favs;
        }

        /// <summary>
        /// Method will save a haiku to db under user's favorites
        /// </summary>
        /// <param name="username"></param>
        /// <param name="haiku"></param>
        /// <returns></returns>
        public bool AddToUserFavorites(string username, Haiku haiku)
        {
            bool addSuccesful = false;

            //add haiku to db
            Haiku savedHaiku = SaveHaiku(haiku);
            UserFav userFav = new UserFav();
            userFav.Username = username;
            userFav.Haiku = haiku;
            _dbContext.UserFavs.Add(userFav);
            _dbContext.SaveChanges();

            userFav = _dbContext.UserFavs.Where(uf => uf.Username == username && uf.Haiku.HaikuId == savedHaiku.HaikuId).FirstOrDefault();
            if(userFav != null)
            {
                addSuccesful = true;
            }
            
            return addSuccesful;
        }
    }
}
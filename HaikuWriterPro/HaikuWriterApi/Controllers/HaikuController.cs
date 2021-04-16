using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BusinessLogic;
using Models;

namespace HaikuWriterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HaikuController : ControllerBase
    {
        private readonly HaikuGenerator _haukuGenerator;
        private readonly HaikuMethods _haikuMethod;

        public HaikuController(HaikuGenerator haukuGenerator, HaikuMethods haikuMethod)
        {
            _haukuGenerator = haukuGenerator;
            _haikuMethod = haikuMethod;
        }

        /// <summary>
        /// Haiku Controller Route that will retrieve a random haiku to display
        /// </summary>
        /// <returns></returns>
        [HttpGet("generateHaiku")]
        public ActionResult<HaikuDTO> GenerateHaiku()
        {
            HaikuDTO haiku = _haukuGenerator.GenerateHaiku();
            return haiku;
        }

        /// <summary>
        /// Haiku Controller Route to retrieve a list of unapproved haiku lines
        /// </summary>
        /// <returns></returns>
        [HttpGet("unapprovedHaikuLine")]
        public ActionResult<List<HaikuLine>> GetUnapprovedHaikuLines()
        {
            List<HaikuLine> haikuLinesList = _haikuMethod.GetUnapprovedHaikuLines();
            return haikuLinesList;
        }

        /// <summary>
        /// Haiku Controller route that retrieves a list of unapproved haikus
        /// </summary>
        /// <returns></returns>
        [HttpGet("unapprovedHaiku")]
        public ActionResult<List<Haiku>> GetUnapprovedHaikus()
        {
            List<Haiku> haikuList = _haikuMethod.GetUnapprovedHaikus();
            return haikuList;
        }

        /// <summary>
        /// Returns all the approved haikus 
        /// </summary>
        /// <returns></returns>
        [HttpGet("allhaikus")]
        public ActionResult<List<Haiku>> GetAllHaikus()
        {

            List<Haiku> haikuList = _haikuMethod.GetHaikus();
            return haikuList;
        }

        /// Haiku Controller Route that will take in an haiku line id
        /// will send back approval status
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        [HttpPost("approveHaikuLine")]
        public ActionResult<bool> ApproveHaikuLine([FromBody] int hlid)
        {
            bool haikuLineApproval = _haikuMethod.ApproveHaikuLine(hlid);
            return haikuLineApproval;
        }

        [HttpPost("approveHaiku")]
        public ActionResult<bool> ApproveHaiku([FromBody] int hlid)
        {
            bool haikuLineApproval = _haikuMethod.ApproveHaiku(hlid);
            return haikuLineApproval;
        }

        /// <summary>
        /// Haiku Controller Route that will take in a haiku line id and
        /// pass a request to delete from the database
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        [HttpPost("deleteHaikuLine")]
        public ActionResult<bool> DeleteHaikuLine([FromBody] int hlid)
        {
            bool deletionSuccessful = _haikuMethod.DeleteHaikuLine(hlid);
            return deletionSuccessful;
        }

        /// <summary>
        /// Haiku Controller that will pass a request along to delete a haiku
        /// </summary>
        /// <param name="hlid"></param>
        /// <returns></returns>
        [HttpPost("deleteHaiku")]
        public ActionResult<bool> DeleteHaiku([FromBody] int hlid)
        {
            bool deletionSuccessful = _haikuMethod.DeleteHaiku(hlid);
            return deletionSuccessful;
        }

        /// <summary>
        /// Haiku Controller route that will pass along a haiku to save to the database
        /// to later be reviewed by an admin user
        /// </summary>
        /// <param name="haikuLine"></param>
        /// <returns></returns>
        [HttpPost("submitHaikuLine")]
        public ActionResult<HaikuLine> SubmitHaikuLine([FromBody] RawHaikuLine haikuLine)
        {

            HaikuLine hl = new HaikuLine
            {
                Line = haikuLine.line,
                Tags = haikuLine.tags,
                Syllable = haikuLine.syllable,
                Approved = false,
                Username = haikuLine.username
            };

            HaikuLine newline = _haikuMethod.SubmitHaikuLine(hl);

            return newline;
        }

        /// <summary>
        /// Haiku Controller Route that will pass a haiku along to be saved to the database
        /// to then be later reviewed by admin
        /// </summary>
        /// <param name="haiku"></param>
        /// <returns></returns>
        [HttpPost("submitHaiku")]
        public ActionResult<Haiku> SubmitHaiku([FromBody] HaikuDTO haiku)
        {

            Haiku h = new Haiku
            {
                HaikuLine1 = haiku.haikuLine1,
                HaikuLine2 = haiku.haikuLine2,
                HaikuLine3 = haiku.haikuLine2,
                Tags = haiku.tags,
                Approved = false,
                Username = haiku.username
            };

            Haiku newhaiku = _haikuMethod.SubmitHaiku(h);

            return newhaiku;
        }

        /// <summary>
        /// Haiku Controller Route that will pass a haiku down to repolayer to save it to 
        /// a user's favorites
        /// </summary>
        /// <param name="haiku"></param>
        /// <returns></returns>
        [HttpPost("saveHaiku")]
        public ActionResult<Boolean> SaveHaikuToFavorites([FromBody] SaveHaikuDTO haiku)
        {
            Haiku newhaiku = new Haiku{
                HaikuLine1 = haiku.haikuLine1,
                HaikuLine2 = haiku.haikuLine2,
                HaikuLine3 = haiku.haikuLine3,
                Tags = haiku.tags,
                Approved = true,
                Username = haiku.username
            };

            string currentusername = haiku.currentuser;

            bool saveSuccessful = _haikuMethod.SaveHaikuToFavorites(currentusername, newhaiku);

            return saveSuccessful;
        }

    }
}

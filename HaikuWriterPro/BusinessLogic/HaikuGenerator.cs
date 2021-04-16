using Repository;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class HaikuGenerator
    {
        private readonly HaikuRepo _repo;

        public HaikuGenerator(HaikuRepo repo)
        {
            _repo = repo;
        }

        public HaikuLine Line1 { get; set; }
        public HaikuLine Line2 { get; set; }
        public HaikuLine Line3 { get; set; }

        public void MakeHaiku()
        {
            Line1 = _repo.GetHaiku5();
            Line2 = _repo.GetHaiku7();
            Line3 = _repo.GetHaiku5(Line1);
        }

        public HaikuDTO GenerateHaiku()
        {
            //Get lines for the haiku
            MakeHaiku();
            //Get tags
            string tags = GetTags(Line1, Line2, Line3);
            //get usernames
            string username = GetUsername(Line1, Line2, Line3);

            // set haiku lines for haiku DTO
            HaikuDTO generatedHaiku = new HaikuDTO
            {
                haikuLine1 = Line1.Line,
                haikuLine2 = Line2.Line,
                haikuLine3 = Line3.Line,
                tags = tags,
                username = username
            };

            return generatedHaiku;
        }

        /// <summary>
        /// This method will get tags from three lines and combine them into one
        /// ta string
        /// </summary>
        /// <param name="hl"></param>
        /// <param name="hl2"></param>
        /// <param name="hl3"></param>
        /// <returns></returns>
        public string GetTags(HaikuLine hl, HaikuLine hl2, HaikuLine hl3)
        {
            List<string> tagholder = CombineTags(hl, hl2, hl3);
            List<string> uniquetagholder = tagholder.Distinct().ToList();
            string tags = "";

            //compare tags 
            foreach (string tag in uniquetagholder)
            {
                tags += tag + " ";
            }
            tags = tags.Trim();
            return tags;
        }

        /// <summary>
        /// This method will take a haikuline and then split the tags by space
        /// It will return a string array of tags
        /// </summary>
        /// <param name="hl"></param>
        /// <returns></returns>
        public static string[] SplitTags(HaikuLine hl)
        {
            string[] tags = hl.Tags.Split(' ');
            return tags;
        }

        /// <summary>
        /// This method will take string arrays of haiku tags and combine them
        /// into one array
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <returns></returns>
        public List<string> CombineTags(HaikuLine hl, HaikuLine hl2, HaikuLine hl3)
        {
            // New List to hold tags
            List<string> tags = new List<string>();
            // Split tag string from eahc haiku
            string[] t1 = SplitTags(hl);
            string[] t2 = SplitTags(hl2);
            string[] t3 = SplitTags(hl3);

            foreach (string tag in t1)
            {
                tags.Add(tag);
            }
            foreach (string tag in t2)
            {
                tags.Add(tag);
            }
            foreach (string tag in t3)
            {
                tags.Add(tag);
            }
            return tags;
        }

        /// <summary>
        /// This method will compare the usernames of each line and return a username string
        /// of combines usernames
        /// </summary>
        /// <param name="hl"></param>
        /// <param name="hl2"></param>
        /// <param name="hl3"></param>
        /// <returns></returns>
        public static string GetUsername(HaikuLine hl, HaikuLine hl2, HaikuLine hl3)
        {
            string username = "";
            string username1 = hl.Username;
            string username2 = hl2.Username;
            string username3 = hl3.Username;

            username = username1;
            if (!username1.Equals(username2))
            {
                username += " " + username2;
            }
            if (!username3.Equals(username1) && !username3.Equals(username2))
            {
                username += " " + username3;
            }

            return username;
        }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HaikuLine
    {   
        [Key]
        public int HaikuLineId { get; set; }
        public string Line { get; set; }
        public string Tags { get; set; }
        public int Syllable { get; set; }
        public bool Approved { get; set; }

        //ef relationship with user
        public string Username { get; set; }
        public User User { get; set; }
    }
}
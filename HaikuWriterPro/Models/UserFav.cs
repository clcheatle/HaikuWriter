using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class UserFav
    {
        //ef relationship with user
        public string Username { get; set; }
        public User User { get; set; }
        //ef relationship with haiku
        public int HaikuId { get; set; }
        public Haiku Haiku { get; set; }
    }
}
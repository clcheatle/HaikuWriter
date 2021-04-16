using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Thread
    {
        [Key]
        public int ThreadId { get; set; }
        public string Description { get; set; }

        //ef relationship with user
        public string Username { get; set; }
        public User User { get; set; }

        //ef relationship with message
        public ICollection<Message> Messages { get; set; }
    }
}
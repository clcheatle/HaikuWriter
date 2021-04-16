using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RawUser
    {
        
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public bool AdminStatus { get; set; }
        public string FaceBookName { get; set; }
        public string TwitterName { get; set; }

        public string Password { get; set; }
    
       
    }
}

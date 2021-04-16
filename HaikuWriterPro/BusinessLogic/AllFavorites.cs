using System.Collections.Generic;
using System.Linq;
using Models;

namespace BusinessLogic
{
    public class AllFavorites
    {
        public User User { get; set; }
        public ICollection<UserFav> Favorites { get; set; }
        public AllFavorites(User user, ICollection<UserFav> favorites)
        {
            User = user;
            Favorites = new List<UserFav>();
            foreach(UserFav fav in favorites) 
            {
                if(fav.User.Username == user.Username)
                {
                    Favorites.Add(fav);
                }
                
            }
        }
    }
}

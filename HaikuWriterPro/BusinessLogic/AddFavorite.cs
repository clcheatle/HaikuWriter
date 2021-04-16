using Models;

namespace BusinessLogic
{
    public class AddFavorite
    {
        public UserFav FavoriteHaiku { get; set; }

        public AddFavorite(Haiku haiku, User user)
        {
            FavoriteHaiku= new UserFav(); 
            FavoriteHaiku.User = user;
            FavoriteHaiku.Haiku = haiku;
        }
    }
}
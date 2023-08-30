using Model.EF;

namespace Model
{
    public class UserDAO
    {
        private UserDBContext dbContext;
        public UserDAO()
        {
            dbContext = new UserDBContext();
        }
        public int Login(string userName, string password)
        {
            var result = dbContext.Users.SingleOrDefault(c => c.Name.Equals(userName) && c.Password.Equals(password));
            if (result == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
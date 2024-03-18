using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface IUsersRepository
    {
        User GetById(int id);
        User GetByUsername(string username);
        User GetByUsernameAndPassword(string username, string password);
        IEnumerable<User> GetAllUsers();
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}

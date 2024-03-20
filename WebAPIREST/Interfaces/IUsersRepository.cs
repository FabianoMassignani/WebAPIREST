using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        User GetByUsername(string username);
        User GetByUsernameAndPassword(string username, string password);
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}

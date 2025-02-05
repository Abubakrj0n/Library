using library.Entities;

namespace library.Interfaces;

public interface ILiberyService
{
    bool Registration(string username, string password);
    void Login(string username, string password);
    List<User> GetUsers();
    bool IsAdmin();
    bool DeleteUser(string usernameTodalete);
    void BorrowBook(string title);
    void CheckDueDate(string title);
    void ReturnBook(string title);
    void GetBooks();
    



}
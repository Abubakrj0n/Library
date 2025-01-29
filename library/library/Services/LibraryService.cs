using library.Entities;

namespace library.Services;

public class LibraryService
{
    public List<User> RegisteredUsers { get; set; } = new List<User>();
    public List<Book> Books { get; set; } = new List<Book>();
    public User CurrentUser { get; set; } = null;

    public bool Registration(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Имя пользователя обизатеьно");

        }

        foreach (var user in RegisteredUsers)
        {
            if (user.UserName == username)
            {
                Console.WriteLine("Ползоватеь уже заригистирован!");
                return false;
            }
        }

        var newUser = new User()
        {
            UserName = username,
            Password = password
        };
        RegisteredUsers.Add(newUser);
        Console.WriteLine("Ползователь успешно зарегистирован!");
        return true;

    }

    public void Login(string username, string password)
    {

        foreach (var registeredUser in RegisteredUsers)
        {
            if (registeredUser == null)
            {
                Console.WriteLine("Поле не должно быть пустым");
            }

            if (registeredUser.UserName == username && registeredUser.Password == password)
            {
                Console.WriteLine("Вы успешно зарегистрировались");
            }
        }
    }

    public List<User> GetUsers()
    {
        Console.WriteLine("Список зарегистированых ползователи");
        foreach (var user in RegisteredUsers)
        {
            Console.WriteLine($"Имя ползователей {user.UserName} Роль {user.UserRole}");
        }

        return RegisteredUsers;
    }

    public bool IsAdmin()
    {
        return CurrentUser != null && CurrentUser.UserRole == Role.Admin;
    }

    public bool DeleteUser(string usernameTodalete)
    {
        if (!IsAdmin())
        {
            Console.WriteLine("Только админ может далят ползователи");
            return false;
        }

        bool userDeleted = false;
        for (int i = 0; i < RegisteredUsers.Count; i++)
        {
            if (RegisteredUsers[i].UserName == usernameTodalete)
            {
                RegisteredUsers.RemoveAt(i);
                Console.WriteLine("Удалён пользователь с таким именем");
                userDeleted = true;
                break;
            }
        }
        return false;

    }
} 
        
        
    
    
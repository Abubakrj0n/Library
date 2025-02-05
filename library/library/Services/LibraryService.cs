using library.Entities;
using library.Interfaces;

namespace library.Services;

public class LibraryService :ILiberyService
{
    public List<User> RegisteredUsers { get; set; } = new List<User>();y
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
            if (registeredUser.UserName == username && registeredUser.Password == password)
            {
                CurrentUser = registeredUser;  
                Console.WriteLine($"Вы успешно вошли! Роль: {CurrentUser.UserRole}");
                return;
            }
        }
        Console.WriteLine("Неверное имя пользователя или пароль.");
    }


    public List<User> GetUsers()
    {
        Console.WriteLine("Список зарегистированых ползователи");
        foreach (var user in RegisteredUsers)
        {
            Console.WriteLine($"Имя ползователей: {user.UserName} Роль: {user.UserRole}");
        }

        return RegisteredUsers;
    }

    public bool IsAdmin()
    {
        return CurrentUser != null && CurrentUser.UserRole == Role.Admin;
    }
    public void GetBooks()
    {
        if (!IsAdmin()) 
        {
            Console.WriteLine("Доступ запрещен. Только администратор может просматривать список книг.");
            return;
        }

        if (Books.Count == 0)
        {
            Console.WriteLine("В Библотеки нет книги");
        }

        foreach (var book in Books)
        {
            Console.WriteLine($"Имя книги: {book.Title},Имя Автора: {book.Author}, Имя жанр: {book.Genre},Год издания:{book.Year},Доступ: {book.IsAvailable} ");
        }
        
    }

    public void BorrowBook(string title)
    {
        foreach (var book in Books)
        {
            if (book.Title.Equals(title))
            {
                if (!book.IsAvailable)
                {
                    Console.WriteLine("Книга уже выдана.");
                    return;
                }
                if (CurrentUser == null)
                {
                    Console.WriteLine("Сначала войдите в систему.");
                    return;
                }
                book.IsAvailable = false;
                book.DueDate = DateTime.Now.AddDays(1); 
                book.Borrower = CurrentUser;
                CurrentUser.BorrowedBooks.Add(book);
                Console.WriteLine($"Книга '{title}' выдана пользователю {CurrentUser.UserName}.");
                return;
            }
        }
        Console.WriteLine("Книга не найдена.");
    }

    public void CheckDueDate(string title)
    {
        foreach (var book in Books)
        {
            if (book.Title.Equals(title) && book.Borrower == CurrentUser)
            {
                if (!book.IsAvailable)
                {
                    Console.WriteLine($"Книга '{title}' должна быть возвращена до {book.DueDate.Value:dd.MM.yyyy}.");
                    return;
                }
            }
        }
        Console.WriteLine("Вы не брали эту книгу.");
    }

    public void ReturnBook(string title)
    {
        foreach (var book in Books)
        {
            if (book.Title.Equals(title) && book.Borrower == CurrentUser)
            {
                book.IsAvailable = true;
                book.DueDate = null;
                book.Borrower = null;
                CurrentUser.BorrowedBooks.Remove(book);
                Console.WriteLine($"Книга '{title}' успешно возвращена.");
                return;
            }
        }
        Console.WriteLine("Вы не брали эту книгу.");
    }

    public bool DeleteBook(string bookTodelete)
    {
        if (!IsAdmin())
        {
            Console.WriteLine("Только админ может удолять книгу");
            return false;
        }

        bool bookDelete = false;
        for (int i = 0; i < Books.Count; i++)
        {
            if (Books[i].Title == bookTodelete)
            {
                Books.RemoveAt(i);
                Console.WriteLine("Удалён книга с таким именем");
                bookDelete = true;
                break;


            }
        }

        return false;
    }


    public bool DeleteUser(string usernameTodalete)
    {
        if (!IsAdmin())
        {
            Console.WriteLine("Только админ может удалят ползователи");
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
        
        
    
    
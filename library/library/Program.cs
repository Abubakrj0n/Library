// See https://aka.ms/new-console-template for more information

using System;
using library.Entities;
using library.Services;

class Program
{
    static void Main()
    {
        LibraryService libraryService = new LibraryService();
        
        
        libraryService.RegisteredUsers.Add(new User("Abubakr", "abu123", Role.Admin));
        
        while (true)
        {
            Console.WriteLine("Добро пожаловать в библиотеку!");
            Console.WriteLine("1. Регистрация");
            Console.WriteLine("2. Вход");
            Console.WriteLine("3. Список пользователей (только для админов)");
            Console.WriteLine("4. Управление книгами (только для админов)");
            Console.WriteLine("5. Выдача и возврат книг");
            Console.WriteLine("6. Удаление пользователя (только для админов)");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите действие: ");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя пользователя: ");
                    string username = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    string password = Console.ReadLine();
                    libraryService.Registration(username, password);
                    break;
                
                case "2":
                    Console.Write("Введите имя пользователя: ");
                    string loginUsername = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    string loginPassword = Console.ReadLine();
                    libraryService.Login(loginUsername, loginPassword);
                    break;
                
                case "3":
                    if (libraryService.CurrentUser != null && libraryService.CurrentUser.UserRole == Role.Admin)
                    {
                        libraryService.GetUsers();
                    }
                    else
                    {
                        Console.WriteLine("Доступ запрещен. Только администраторы могут просматривать список пользователей.");
                    }
                    break;
                
                case "4":
                    if (libraryService.CurrentUser == null || libraryService.CurrentUser.UserRole != Role.Admin)
                    {
                        Console.WriteLine("Доступ запрещен. Только администраторы могут управлять книгами.");
                        break;
                    }
                    Console.WriteLine("Управление книгами:");
                    Console.WriteLine("1. Добавить книгу");
                    Console.WriteLine("2. Посмотрет списка книги");
                    Console.WriteLine("3. Удаление книги");
                    Console.Write("Выберите действие: ");
                    string bookChoice = Console.ReadLine();
                    
                    switch (bookChoice)
                    {
                        case "1":
                            Console.Write("Введите название книги: ");
                            string title = Console.ReadLine();
                            Console.Write("Введите автора: ");
                            string author = Console.ReadLine();
                            Console.Write("Введите жанр: ");
                            string genre = Console.ReadLine();
                            Console.Write("Введите год издания: ");
                            int year = int.Parse(Console.ReadLine());
                            libraryService.Books.Add(new Book { Title = title, Author = author, Genre = genre, Year = year, IsAvailable = true });
                            Console.WriteLine("Книга успешно добавлена.");
                            break;
                        case "2":
                            if (libraryService.CurrentUser != null && libraryService.CurrentUser.UserRole == Role.Admin)
                            {
                                libraryService.GetBooks();
                            }
                            else
                            {
                                Console.WriteLine("Только админ можеть видет списки книгу");
                            }
                            break;
                        case "3":
                            if (libraryService.CurrentUser != null && libraryService.CurrentUser.UserRole == Role.Admin)
                            {
                                Console.Write("Введите имя книга для удаления: ");
                                string bookDelete = Console.ReadLine();
                                libraryService.DeleteBook(bookDelete);
                            }
                            else
                            {
                                Console.WriteLine("Только Админ может удалить пользователь");
                            } 
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;
                case "5":
                    Console.WriteLine("Выдача и возврат книг:");
                    Console.WriteLine("1. Выдать книгу");
                    Console.WriteLine("2. Посмотреть срок возврата");
                    Console.WriteLine("3. Вернуть книгу");
                    Console.Write("Выберите действие:");
                    string borrowChoice = Console.ReadLine();
                    switch (borrowChoice)
                    {
                        case "1":
                            Console.Write("Введите название книги: ");
                            string borrowTitle = Console.ReadLine();
                            libraryService.BorrowBook(borrowTitle);
                            break;
                        case "2":
                            Console.Write("Введите название автор: ");
                            string returnAuthor = Console.ReadLine();
                            libraryService.CheckDueDate(returnAuthor);
                            break;
                        case "3":
                            Console.Write("Введите название жанр: ");
                            string returnBookGenre = Console.ReadLine();
                            libraryService.ReturnBook(returnBookGenre);
                            break;
                    }
                    break;
                    case "6":
                    if (libraryService.CurrentUser != null && libraryService.CurrentUser.UserRole == Role.Admin)
                    {
                        Console.Write("Введите имя пользователя для удаления: ");
                        string userToDelete = Console.ReadLine();
                        libraryService.DeleteUser(userToDelete);
                    }
                    else
                    {
                        Console.WriteLine("Только Админ может удалить пользователь");
                    }    
                    break;
                    case "7":
                    Console.WriteLine("Выход из программы...");
                    return;
                    default:
                        Console.WriteLine("Ошибка ");
                        break;
                        
                        
                
            }
        }
    }
}
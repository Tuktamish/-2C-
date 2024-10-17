public class Author
{
    private char firstInitial;
    private char lastInitial;

    // Конструктор
    public Author(char firstInitial, char lastInitial)
    {
        this.firstInitial = firstInitial;
        this.lastInitial = lastInitial;
    }

    // Конструктор копирования
    public Author(Author other)
    {
        this.firstInitial = other.firstInitial;
        this.lastInitial = other.lastInitial;
    }

    // Метод для получения полного имени автора
    public string GetFullName()
    {
        return $"{firstInitial}. {lastInitial}.";
    }

    // Переопределение метода ToString()
    public override string ToString()
    {
        return $"Author: {GetFullName()}";
    }
}

public class Book : Author
{
    private string title;
    private string genre;

    // Конструктор
    public Book(char firstInitial, char lastInitial, string title, string genre)
        : base(firstInitial, lastInitial)
    {
        this.title = title;
        this.genre = genre;
    }

    // Конструктор копирования
    public Book(Book other)
        : base(other)
    {
        this.title = other.title;
        this.genre = other.genre;
    }

    // Метод для получения информации о книге
    public string GetBookInfo()
    {
        return $"{title} - Genre: {genre}, Author: {GetFullName()}";
    }

    // Переопределение метода ToString()
    public override string ToString()
    {
        return $"Book: {title}, Genre: {genre}, Author: {GetFullName()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Ввод данных для автора
        char authorFirstInitial = GetCharInput("Введите первую букву имени автора: ");
        char authorLastInitial = GetCharInput("Введите первую букву фамилии автора: ");

        // Ввод данных для книги
        string bookTitle = GetInput("Введите название книги: ");
        string bookGenre = GetInput("Введите жанр книги: ");

        // Создание объекта автора
        Author author = new Author(authorFirstInitial, authorLastInitial);
        Console.WriteLine(author.ToString()); // Печать информации об авторе

        // Создание объекта книги
        Book book = new Book(authorFirstInitial, authorLastInitial, bookTitle, bookGenre);
        Console.WriteLine(book.ToString()); // Печать информации о книге
        Console.WriteLine(book.GetBookInfo()); // Дополнительная информация о книге

        // Создание копии книги
        Book bookCopy = new Book(book);
        Console.WriteLine(bookCopy.ToString()); // Печать копии книги
    }

    static char GetCharInput(string message)
    {
        char result;
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (input.Length == 1)
            {
                result = input[0];
                break;
            }
            Console.WriteLine("Ошибка: введите только один символ.");
        }
        return result;
    }

    static string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}

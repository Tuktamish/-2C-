public class Money
{
    private uint rubles;
    private byte kopeks;

    // Конструктор
    public Money(uint rubles, byte kopeks)
    {
        if (kopeks >= 100)
        {
            rubles += (uint)(kopeks / 100);
            kopeks %= 100;
        }
        this.rubles = rubles;
        this.kopeks = kopeks;
    }

    // Конструктор копирования
    public Money(Money other)
    {
        this.rubles = other.rubles;
        this.kopeks = other.kopeks;
    }

    // Свойства
    public uint Rubles => rubles;
    public byte Kopeks => kopeks;

    // Метод для вычитания другой денежной величины
    public Money Subtract(Money other)
    {
        uint totalKopeksThis = rubles * 100 + kopeks;
        uint totalKopeksOther = other.rubles * 100 + other.kopeks;

        if (totalKopeksThis < totalKopeksOther)
            throw new InvalidOperationException("Результат не может быть отрицательным.");

        uint resultKopeks = totalKopeksThis - totalKopeksOther;
        return new Money(resultKopeks / 100, (byte)(resultKopeks % 100));
    }

    // Унарная операция ++
    public static Money operator ++(Money money)
    {
        money.kopeks++;
        if (money.kopeks >= 100)
        {
            money.kopeks = 0;
            money.rubles++;
        }
        return money;
    }

    // Унарная операция --
    public static Money operator --(Money money)
    {
        if (money.rubles == 0 && money.kopeks == 0)
            throw new InvalidOperationException("Нельзя вычесть копейку из нуля.");

        if (money.kopeks == 0)
        {
            money.kopeks = 99;
            money.rubles--;
        }
        else
        {
            money.kopeks--;
        }
        return money;
    }

    // Операция приведения к uint
    public static implicit operator uint(Money money)
    {
        return money.rubles;
    }

    // Операция приведения к double
    public static explicit operator double(Money money)
    {
        return money.kopeks / 100.0;
    }

    // Бинарная операция - Money и uint
    public static Money operator -(Money money, uint amount)
    {
        uint totalKopeks = money.rubles * 100 + money.kopeks;
        totalKopeks -= amount;
        if (totalKopeks < 0)
            throw new InvalidOperationException("Результат не может быть отрицательным.");

        return new Money(totalKopeks / 100, (byte)(totalKopeks % 100));
    }

    // Бинарная операция - Money и Money
    public static Money operator -(Money money1, Money money2)
    {
        return money1.Subtract(money2);
    }

    // Переопределение метода ToString()
    public override string ToString()
    {
        return $"{rubles} руб. {kopeks} коп.";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Ввод данных для первой денежной величины
        Money money1 = GetMoneyInput("Введите первую сумму (рубли, копейки): ");
        Console.WriteLine($"Первая сумма: {money1}");

        // Ввод данных для второй денежной величины
        Money money2 = GetMoneyInput("Введите вторую сумму (рубли, копейки): ");
        Console.WriteLine($"Вторая сумма: {money2}");

        // Вычитание
        try
        {
            Money result = money1 - money2;
            Console.WriteLine($"Результат вычитания: {result}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Тестирование унарных операций
        Console.WriteLine($"Сумма после добавления копейки: {++money1}");
        Console.WriteLine($"Сумма после вычитания копейки: {--money1}");

        // Тестирование приведения типов
        uint rubles = money1;
        double kopeksInRubles = (double)money1;
        Console.WriteLine($"Рубли: {rubles}, Копейки в рублях: {kopeksInRubles}");
    }

    static Money GetMoneyInput(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            var parts = input.Split(',');

            if (parts.Length == 2 &&
                IsValidRubles(parts[0].Trim(), out uint rubles) &&
                IsValidKopeks(parts[1].Trim(), out byte kopeks))
            {
                return new Money(rubles, kopeks);
            }

            Console.WriteLine("Ошибка: Неверный формат ввода. Используйте: <рубли>, <копейки>");
        }
    }

    static bool IsValidRubles(string input, out uint rubles)
    {
        return uint.TryParse(input, out rubles);
    }

    static bool IsValidKopeks(string input, out byte kopeks)
    {
        return byte.TryParse(input, out kopeks) && kopeks < 100;
    }
}

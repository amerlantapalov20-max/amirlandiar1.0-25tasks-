using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

public sealed class Car
{
    public Car(string brand, int year, decimal price)
    {
        Brand = brand;
        Year = year;
        Price = price;
    }

    public string Brand { get; }
    public int Year { get; }
    public decimal Price { get; }

    public string ShowInfo() => $"Марка: {Brand}";
    public string ShowInfo(int year) => $"{Brand}, год выпуска: {year}";
    public string ShowInfo(int year, decimal price) =>
        $"{Brand}, год выпуска: {year}, цена: {price:C}";
}

public static class Student
{
    public static double CalculateGrade(double score) => score;
    public static double CalculateGrade(double firstScore, double secondScore) =>
        (firstScore + secondScore) / 2;
    public static double CalculateGrade(double firstScore, double secondScore, double thirdScore) =>
        (firstScore + secondScore + thirdScore) / 3;
}

public static class DiscountSystem
{
    public static decimal CalculateDiscount(decimal price, double percent)
    {
        if (percent < 0 || percent > 100)
            throw new ArgumentOutOfRangeException(nameof(percent), "Процент скидки должен быть от 0 до 100.");
        ValidateNonNegative(price, nameof(price));
        return price * (decimal)percent / 100;
    }

    public static decimal CalculateDiscount(decimal price, decimal fixedAmount)
    {
        ValidateNonNegative(price, nameof(price));
        ValidateNonNegative(fixedAmount, nameof(fixedAmount));
        return Math.Min(price, fixedAmount);
    }

    private static void ValidateNonNegative(decimal value, string name)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(name, "Значение не может быть отрицательным.");
    }
}

public static class DeliveryService
{
    public static decimal CalculateDelivery(decimal distanceKm) =>
        CalculateDelivery(distanceKm, 5m);

    public static decimal CalculateDelivery(decimal distanceKm, bool express)
    {
        ValidateDistance(distanceKm);
        return 5m + distanceKm * (express ? 3m : 1m);
    }

    public static decimal CalculateDelivery(decimal distanceKm, decimal weightKg)
    {
        ValidateDistance(distanceKm);
        if (weightKg < 0)
            throw new ArgumentOutOfRangeException(nameof(weightKg), "Вес не может быть отрицательным.");
        return 5m + distanceKm + weightKg * 0.5m;
    }

    private static void ValidateDistance(decimal distanceKm)
    {
        if (distanceKm < 0)
            throw new ArgumentOutOfRangeException(nameof(distanceKm), "Расстояние не может быть отрицательным.");
    }
}

public sealed class Book
{
    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public string Title { get; }
    public string Author { get; }
    public int Year { get; }

    public string DisplayBook() => Title;
    public string DisplayBook(string author) => $"{Title} — {author}";
    public string DisplayBook(string author, int year) => $"{Title} — {author}, {year}";
}

public static class GameScore
{
    public static int AddScore(int score, int points) => checked(score + points);
    public static int AddScore(int score, int points, int bonusPoints) =>
        checked(score + points + bonusPoints);
    public static int AddScore(int score, int points, double multiplier) =>
        checked(score + (int)Math.Round(points * multiplier, MidpointRounding.AwayFromZero));
}

public static class FileStorage
{
    public static void SaveFile(string path, string text) =>
        File.WriteAllText(path, text);

    public static void SaveFile(string path, string[] lines) =>
        File.WriteAllLines(path, lines);

    public static void SaveFile(string path, string text, Encoding encoding) =>
        File.WriteAllText(path, text, encoding);
}

public static class Geometry
{
    public static double CalculateVolume(double cubeSide)
    {
        ValidatePositive(cubeSide, nameof(cubeSide));
        return Math.Pow(cubeSide, 3);
    }

    public static double CalculateVolume(double length, double width, double height)
    {
        ValidatePositive(length, nameof(length));
        ValidatePositive(width, nameof(width));
        ValidatePositive(height, nameof(height));
        return length * width * height;
    }

    public static double CalculateVolume(double radius, double height)
    {
        ValidatePositive(radius, nameof(radius));
        ValidatePositive(height, nameof(height));
        return Math.PI * radius * radius * height;
    }

    private static void ValidatePositive(double value, string name)
    {
        if (!double.IsFinite(value) || value <= 0)
            throw new ArgumentOutOfRangeException(name, "Размер должен быть конечным и больше нуля.");
    }
}

public static class TaxiService
{
    public static decimal CalculateFare(decimal distanceKm) =>
        CalculateFare(distanceKm, 0m);

    public static decimal CalculateFare(decimal distanceKm, decimal waitingMinutes)
    {
        ValidateNonNegative(distanceKm, nameof(distanceKm));
        ValidateNonNegative(waitingMinutes, nameof(waitingMinutes));
        return 3m + distanceKm * 2m + waitingMinutes * 0.5m;
    }

    public static decimal CalculateFare(decimal distanceKm, string carClass)
    {
        ValidateNonNegative(distanceKm, nameof(distanceKm));
        decimal rate = carClass.ToLowerInvariant() switch
        {
            "economy" => 1.5m,
            "comfort" => 2.5m,
            "business" => 4m,
            _ => throw new ArgumentException("Класс: economy, comfort или business.", nameof(carClass))
        };
        return 3m + distanceKm * rate;
    }

    private static void ValidateNonNegative(decimal value, string name)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(name, "Значение не может быть отрицательным.");
    }
}

public static class Library
{
    private static readonly Book[] Books =
    {
        new("Война и мир", "Лев Толстой", 1869),
        new("Преступление и наказание", "Федор Достоевский", 1866),
        new("Мастер и Маргарита", "Михаил Булгаков", 1967)
    };

    public static Book? SearchBook(string title) =>
        Books.FirstOrDefault(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

    public static Book? SearchBook(string query, bool searchByAuthor) =>
        searchByAuthor
            ? Books.FirstOrDefault(book => book.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
            : SearchBook(query);

    public static Book? SearchBook(string title, string author) =>
        Books.FirstOrDefault(book =>
            book.Title.Contains(title, StringComparison.OrdinalIgnoreCase) &&
            book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
}

public static class ArraySorter
{
    public static int[] Sort(int[] values) => values.OrderBy(value => value).ToArray();
    public static double[] Sort(double[] values) => values.OrderBy(value => value).ToArray();
    public static string[] Sort(string[] values) =>
        values.OrderBy(value => value, StringComparer.Ordinal).ToArray();
}

public readonly record struct ArrayStatistics(double Minimum, double Maximum, double Average);

public static class ArrayAnalyzer
{
    public static ArrayStatistics Analyze(int[] values)
    {
        if (values.Length == 0)
            throw new ArgumentException("Массив не должен быть пустым.", nameof(values));
        return new ArrayStatistics(values.Min(), values.Max(), values.Average());
    }

    public static ArrayStatistics Analyze(double[] values)
    {
        if (values.Length == 0)
            throw new ArgumentException("Массив не должен быть пустым.", nameof(values));
        if (values.Any(value => !double.IsFinite(value)))
            throw new ArgumentException("Значения должны быть конечными числами.", nameof(values));
        return new ArrayStatistics(values.Min(), values.Max(), values.Average());
    }

    public static ArrayStatistics Analyze(double first, double second) =>
        Analyze(new[] { first, second });
}

public static class PasswordValidator
{
    public static bool ValidatePassword(string password) =>
        password.Length >= 8;

    public static bool ValidatePassword(string password, int minimumLength) =>
        password.Length >= minimumLength && password.Any(char.IsDigit);

    public static bool ValidatePassword(string password, int minimumLength, bool requireSpecialCharacter) =>
        password.Length >= minimumLength &&
        password.Any(char.IsDigit) &&
        (!requireSpecialCharacter || password.Any(character => !char.IsLetterOrDigit(character)));
}

public static class CurrencyConverter
{
    private const decimal FixedExchangeRate = 0.92m;

    public static decimal ConvertCurrency(decimal amount)
    {
        ValidateAmount(amount);
        return amount * FixedExchangeRate;
    }

    public static decimal ConvertCurrency(decimal amount, decimal exchangeRate, decimal commissionPercent)
    {
        ValidateAmount(amount);
        if (exchangeRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(exchangeRate), "Курс должен быть больше нуля.");
        if (commissionPercent < 0 || commissionPercent > 100)
            throw new ArgumentOutOfRangeException(nameof(commissionPercent), "Комиссия должна быть от 0 до 100 процентов.");

        decimal convertedAmount = amount * exchangeRate;
        return convertedAmount - convertedAmount * commissionPercent / 100;
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма не может быть отрицательной.");
    }
}

public static class DateFormatter
{
    public static string FormatDate(DateTime date) =>
        date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);

    public static string FormatDate(string date) =>
        DateTime.Parse(date, CultureInfo.InvariantCulture).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);

    public static string FormatDate(DateOnly date, TimeOnly time) =>
        $"{date:dd.MM.yyyy} {time:HH:mm}";
}

public static class NotificationService
{
    public static void SendNotification(string message) =>
        Console.WriteLine($"Уведомление: {message}");

    public static void SendNotification(string title, string message) =>
        Console.WriteLine($"[{title}] {message}");

    public static void SendNotification(string title, string message, string recipient) =>
        Console.WriteLine($"Для {recipient} — [{title}] {message}");
}

public sealed class Employee
{
    public Employee(string name, string position, decimal salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public string Name { get; }
    public string Position { get; }
    public decimal Salary { get; }

    public override string ToString() => $"{Name}, {Position}, зарплата: {Salary:C}";
}

public static class EmployeeFactory
{
    public static Employee CreateEmployee(string name) =>
        new(name, "Не указана", 0m);

    public static Employee CreateEmployee(string name, string position) =>
        new(name, position, 0m);

    public static Employee CreateEmployee(string name, string position, decimal salary)
    {
        if (salary < 0)
            throw new ArgumentOutOfRangeException(nameof(salary), "Зарплата не может быть отрицательной.");
        return new Employee(name, position, salary);
    }
}

public static class Warehouse
{
    public static int UpdateStock(int currentQuantity, int amount)
    {
        ValidateQuantities(currentQuantity, amount);
        return checked(currentQuantity + amount);
    }

    public static int UpdateStock(int currentQuantity, int amount, bool decrease)
    {
        ValidateQuantities(currentQuantity, amount);
        if (!decrease)
            return UpdateStock(currentQuantity, amount);
        if (amount > currentQuantity)
            throw new ArgumentOutOfRangeException(nameof(amount), "Нельзя списать больше товара, чем есть на складе.");
        return currentQuantity - amount;
    }

    public static int UpdateStock(int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(newQuantity), "Количество не может быть отрицательным.");
        return newQuantity;
    }

    private static void ValidateQuantities(int currentQuantity, int amount)
    {
        if (currentQuantity < 0 || amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Количество не может быть отрицательным.");
    }
}

public static class MathLibrary
{
    public static double Calculate(double first, double second, char operation) =>
        CalculateCore(first, second, operation);

    public static decimal Calculate(decimal first, decimal second, char operation) =>
        operation switch
        {
            '+' => first + second,
            '-' => first - second,
            '*' => first * second,
            '/' when second != 0 => first / second,
            '/' => throw new DivideByZeroException("Деление на ноль невозможно."),
            _ => throw new ArgumentException("Поддерживаются операции +, -, * и /.", nameof(operation))
        };

    private static double CalculateCore(double first, double second, char operation) =>
        operation switch
        {
            '+' => first + second,
            '-' => first - second,
            '*' => first * second,
            '/' when second != 0 => first / second,
            '/' => throw new DivideByZeroException("Деление на ноль невозможно."),
            _ => throw new ArgumentException("Поддерживаются операции +, -, * и /.", nameof(operation))
        };
}

public static class AuthorizationSystem
{
    private const string DemoLogin = "student";
    private const string DemoEmail = "student@example.com";
    private const string DemoPassword = "Pass123!";
    private const string DemoCode = "123456";

    public static bool Login(string login, string password) =>
        login == DemoLogin && password == DemoPassword;

    public static bool Login(string email, string password, bool useEmail) =>
        useEmail && email == DemoEmail && password == DemoPassword;

    public static bool Login(string login, string password, string verificationCode) =>
        login == DemoLogin && password == DemoPassword && verificationCode == DemoCode;
}

public readonly record struct ImageDimensions(int Width, int Height);

public static class ImageProcessor
{
    public static ImageDimensions ResizeImage(int originalWidth, int originalHeight, int width, int height)
    {
        ValidateOriginal(originalWidth, originalHeight);
        ValidatePositive(width, nameof(width));
        ValidatePositive(height, nameof(height));
        return new ImageDimensions(width, height);
    }

    public static ImageDimensions ResizeImage(int originalWidth, int originalHeight, double scale)
    {
        ValidateOriginal(originalWidth, originalHeight);
        if (!double.IsFinite(scale) || scale <= 0)
            throw new ArgumentOutOfRangeException(nameof(scale), "Масштаб должен быть конечным и больше нуля.");
        return new ImageDimensions(
            Math.Max(1, (int)Math.Round(originalWidth * scale)),
            Math.Max(1, (int)Math.Round(originalHeight * scale)));
    }

    public static ImageDimensions ResizeImage(
        int originalWidth,
        int originalHeight,
        int maxWidth,
        int maxHeight,
        bool preserveAspectRatio)
    {
        ValidateOriginal(originalWidth, originalHeight);
        ValidatePositive(maxWidth, nameof(maxWidth));
        ValidatePositive(maxHeight, nameof(maxHeight));

        if (!preserveAspectRatio)
            return new ImageDimensions(maxWidth, maxHeight);

        double scale = Math.Min((double)maxWidth / originalWidth, (double)maxHeight / originalHeight);
        return new ImageDimensions(
            Math.Max(1, (int)Math.Round(originalWidth * scale)),
            Math.Max(1, (int)Math.Round(originalHeight * scale)));
    }

    private static void ValidateOriginal(int width, int height)
    {
        ValidatePositive(width, nameof(width));
        ValidatePositive(height, nameof(height));
    }

    private static void ValidatePositive(int value, string name)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(name, "Размер должен быть больше нуля.");
    }
}

public static class FuelCalculator
{
    public static decimal CalculateFuelCost(decimal distanceKm, decimal consumptionPer100Km) =>
        CalculateFuelCost(distanceKm, consumptionPer100Km, 1m);

    public static decimal CalculateFuelCost(
        decimal distanceKm,
        decimal consumptionPer100Km,
        decimal fuelPricePerLiter)
    {
        if (distanceKm < 0 || consumptionPer100Km < 0 || fuelPricePerLiter < 0)
            throw new ArgumentOutOfRangeException(nameof(distanceKm), "Параметры не могут быть отрицательными.");
        return distanceKm * consumptionPer100Km / 100 * fuelPricePerLiter;
    }
}

public static class ElectronicsStore
{
    public static decimal CalculateFinalPrice(decimal price, decimal discountPercent) =>
        CalculateFinalPriceCore(price, discountPercent, 0m, 0m);

    public static decimal CalculateFinalPrice(decimal price, decimal discountPercent, decimal taxPercent) =>
        CalculateFinalPriceCore(price, discountPercent, taxPercent, 0m);

    public static decimal CalculateFinalPrice(
        decimal price,
        decimal discountPercent,
        decimal taxPercent,
        decimal deliveryCost) =>
        CalculateFinalPriceCore(price, discountPercent, taxPercent, deliveryCost);

    private static decimal CalculateFinalPriceCore(
        decimal price,
        decimal discountPercent,
        decimal taxPercent,
        decimal deliveryCost)
    {
        if (price < 0 || deliveryCost < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Цена и доставка не могут быть отрицательными.");
        if (discountPercent < 0 || discountPercent > 100 || taxPercent < 0)
            throw new ArgumentOutOfRangeException(nameof(discountPercent), "Скидка должна быть 0–100%, налог — неотрицательным.");
        return price * (1 - discountPercent / 100) * (1 + taxPercent / 100) + deliveryCost;
    }
}

public static class StudentEvaluation
{
    public static string EvaluateStudent(double examScore) => GetResult(examScore);

    public static string EvaluateStudent(double examScore, double testScore) =>
        GetResult((examScore + testScore) / 2);

    public static string EvaluateStudent(double[] examScores)
    {
        if (examScores.Length == 0)
            throw new ArgumentException("Нужен хотя бы один результат экзамена.", nameof(examScores));
        return GetResult(examScores.Average());
    }

    private static string GetResult(double score)
    {
        if (!double.IsFinite(score) || score < 0 || score > 100)
            throw new ArgumentOutOfRangeException(nameof(score), "Оценка должна быть от 0 до 100.");
        return score >= 90 ? "Отлично" :
            score >= 75 ? "Хорошо" :
            score >= 60 ? "Удовлетворительно" : "Неудовлетворительно";
    }
}

public static class Helper
{
    public static double Calculate(double first, double second, char operation) =>
        MathLibrary.Calculate(first, second, operation);

    public static decimal Calculate(decimal first, decimal second, char operation) =>
        MathLibrary.Calculate(first, second, operation);

    public static string Format(double number) =>
        number.ToString("F2", CultureInfo.InvariantCulture);

    public static string Format(string text) => text.Trim();

    public static string Format(DateTime date) =>
        date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    public static int Find(int[] values, int target) => Array.IndexOf(values, target);

    public static int Find(string[] values, string target) =>
        Array.FindIndex(values, value => string.Equals(value, target, StringComparison.OrdinalIgnoreCase));

    public static int Find(string text, string substring) =>
        text.IndexOf(substring, StringComparison.OrdinalIgnoreCase);

    public static void Display(string text) => Console.WriteLine(text);
    public static void Display(Employee employee) => Console.WriteLine(employee);
    public static void Display(Book book) => Console.WriteLine(book.DisplayBook(book.Author, book.Year));
    public static void Display(Car car) => Console.WriteLine(car.ShowInfo(car.Year, car.Price));
}

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("=== Демонстрация всех заданий ===");

        var car = new Car("Toyota", 2022, 2_500_000m);
        Console.WriteLine("\n1. Автомобиль:");
        Console.WriteLine(car.ShowInfo());
        Console.WriteLine(car.ShowInfo(car.Year));
        Console.WriteLine(car.ShowInfo(car.Year, car.Price));

        Console.WriteLine("\n2. Оценки:");
        Console.WriteLine($"{Student.CalculateGrade(5)}; {Student.CalculateGrade(4, 5)}; {Student.CalculateGrade(5, 4, 5):F2}");

        Console.WriteLine("\n3. Скидки:");
        Console.WriteLine($"{DiscountSystem.CalculateDiscount(1000m, 10.0)}; {DiscountSystem.CalculateDiscount(1000m, 100m)}");

        Console.WriteLine("\n4. Доставка:");
        Console.WriteLine($"{DeliveryService.CalculateDelivery(10m)}; {DeliveryService.CalculateDelivery(10m, true)}; {DeliveryService.CalculateDelivery(10m, 4m)}");

        var book = new Book("Война и мир", "Лев Толстой", 1869);
        Console.WriteLine("\n5. Книга:");
        Console.WriteLine(book.DisplayBook());
        Console.WriteLine(book.DisplayBook(book.Author));
        Console.WriteLine(book.DisplayBook(book.Author, book.Year));

        Console.WriteLine("\n6. Игровые очки:");
        Console.WriteLine($"{GameScore.AddScore(100, 10)}; {GameScore.AddScore(100, 10, 5)}; {GameScore.AddScore(100, 10, 2.0)}");

        Console.WriteLine("\n7. Сохранение файлов:");
        string outputDirectory = Path.Combine(AppContext.BaseDirectory, "output");
        Directory.CreateDirectory(outputDirectory);
        FileStorage.SaveFile(Path.Combine(outputDirectory, "message.txt"), "Текстовый файл");
        FileStorage.SaveFile(Path.Combine(outputDirectory, "lines.txt"), new[] { "Первая строка", "Вторая строка" });
        FileStorage.SaveFile(Path.Combine(outputDirectory, "utf8.txt"), "Файл в UTF-8", Encoding.UTF8);
        Console.WriteLine($"Три файла сохранены в: {outputDirectory}");

        Console.WriteLine("\n8. Объёмы:");
        Console.WriteLine($"{Geometry.CalculateVolume(3)}; {Geometry.CalculateVolume(2, 3, 4)}; {Geometry.CalculateVolume(2, 5):F2}");

        Console.WriteLine("\n9. Такси:");
        Console.WriteLine($"{TaxiService.CalculateFare(10m):F2}; {TaxiService.CalculateFare(10m, 5m):F2}; {TaxiService.CalculateFare(10m, "comfort"):F2}");

        Console.WriteLine("\n10. Поиск книги:");
        Console.WriteLine(Library.SearchBook("Война и мир") is Book titleMatch ? titleMatch.DisplayBook(titleMatch.Author, titleMatch.Year) : "Не найдена");
        Console.WriteLine(Library.SearchBook("Достоевский", true) is Book authorMatch ? authorMatch.DisplayBook(authorMatch.Author, authorMatch.Year) : "Не найдена");
        Console.WriteLine(Library.SearchBook("Мастер", "Булгаков") is Book bothMatch ? bothMatch.DisplayBook(bothMatch.Author, bothMatch.Year) : "Не найдена");

        Console.WriteLine("\n11. Сортировка:");
        Console.WriteLine(string.Join(", ", ArraySorter.Sort(new[] { 3, 1, 2 })));
        Console.WriteLine(string.Join(", ", ArraySorter.Sort(new[] { 3.5, 1.2, 2.4 })));
        Console.WriteLine(string.Join(", ", ArraySorter.Sort(new[] { "груша", "абрикос", "яблоко" })));

        Console.WriteLine("\n12. Статистика:");
        Console.WriteLine(ArrayAnalyzer.Analyze(new[] { 2, 8, 5 }));
        Console.WriteLine(ArrayAnalyzer.Analyze(new[] { 1.5, 4.5, 3.0 }));
        Console.WriteLine(ArrayAnalyzer.Analyze(2.0, 6.0));

        Console.WriteLine("\n13. Проверка пароля:");
        Console.WriteLine($"{PasswordValidator.ValidatePassword("пароль123")}; {PasswordValidator.ValidatePassword("пароль123", 8)}; {PasswordValidator.ValidatePassword("Пароль123!", 8, true)}");

        Console.WriteLine("\n14. Конвертация валюты:");
        Console.WriteLine($"{CurrencyConverter.ConvertCurrency(100m):F2}; {CurrencyConverter.ConvertCurrency(100m, 90m, 1.5m):F2}");

        Console.WriteLine("\n15. Форматирование даты:");
        Console.WriteLine(DateFormatter.FormatDate(new DateTime(2025, 5, 10)));
        Console.WriteLine(DateFormatter.FormatDate("2025-05-10"));
        Console.WriteLine(DateFormatter.FormatDate(new DateOnly(2025, 5, 10), new TimeOnly(14, 30)));

        Console.WriteLine("\n16. Уведомления:");
        NotificationService.SendNotification("Сообщение");
        NotificationService.SendNotification("Заголовок", "Текст сообщения");
        NotificationService.SendNotification("Заголовок", "Текст сообщения", "user@example.com");

        Console.WriteLine("\n17. Сотрудники:");
        Helper.Display(EmployeeFactory.CreateEmployee("Анна"));
        Helper.Display(EmployeeFactory.CreateEmployee("Иван", "Разработчик"));
        Helper.Display(EmployeeFactory.CreateEmployee("Мария", "Менеджер", 120_000m));

        Console.WriteLine("\n18. Склад:");
        Console.WriteLine($"{Warehouse.UpdateStock(10, 5)}; {Warehouse.UpdateStock(10, 3, true)}; {Warehouse.UpdateStock(20)}");

        Console.WriteLine("\n19. Математические операции:");
        Console.WriteLine($"{MathLibrary.Calculate(10.0, 3.0, '+')}; {MathLibrary.Calculate(10m, 3m, '/')}");

        Console.WriteLine("\n20. Авторизация:");
        Console.WriteLine($"{AuthorizationSystem.Login("student", "Pass123!")}; {AuthorizationSystem.Login("student@example.com", "Pass123!", true)}; {AuthorizationSystem.Login("student", "Pass123!", "123456")}");

        Console.WriteLine("\n21. Изменение размера:");
        Console.WriteLine(ImageProcessor.ResizeImage(800, 600, 400, 300));
        Console.WriteLine(ImageProcessor.ResizeImage(800, 600, 0.5));
        Console.WriteLine(ImageProcessor.ResizeImage(800, 600, 500, 500, true));

        Console.WriteLine("\n22. Стоимость топлива:");
        Console.WriteLine($"{FuelCalculator.CalculateFuelCost(100m, 8m):F2}; {FuelCalculator.CalculateFuelCost(100m, 8m, 60m):F2}");

        Console.WriteLine("\n23. Итоговая цена:");
        Console.WriteLine($"{ElectronicsStore.CalculateFinalPrice(1000m, 10m):F2}; {ElectronicsStore.CalculateFinalPrice(1000m, 10m, 20m):F2}; {ElectronicsStore.CalculateFinalPrice(1000m, 10m, 20m, 50m):F2}");

        Console.WriteLine("\n24. Оценивание студента:");
        Console.WriteLine($"{StudentEvaluation.EvaluateStudent(95)}; {StudentEvaluation.EvaluateStudent(80, 90)}; {StudentEvaluation.EvaluateStudent(new[] { 70.0, 80.0, 90.0 })}");

        Console.WriteLine("\n25. Универсальный помощник:");
        Console.WriteLine($"Calculate: {Helper.Calculate(2.5, 3.0, '*')}; {Helper.Calculate(10m, 4m, '-')}");
        Console.WriteLine($"Format: {Helper.Format(12.345)}; {Helper.Format("  текст  ")}; {Helper.Format(new DateTime(2025, 5, 10))}");
        Console.WriteLine($"Find: {Helper.Find(new[] { 4, 8, 12 }, 8)}; {Helper.Find(new[] { "кот", "пёс" }, "ПЁС")}; {Helper.Find("Привет, мир", "мир")}");
        Helper.Display("Текстовый объект");
        Helper.Display(book);
        Helper.Display(car);
        Helper.Display(EmployeeFactory.CreateEmployee("Олег", "Инженер", 95_000m));
    }
}

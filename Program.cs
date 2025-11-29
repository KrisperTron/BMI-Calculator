using System;

class Program
{
    static BmiAnalyzer analyzer = new BmiAnalyzer();

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        bool menu = true;

        while (menu)
        {
            Console.Clear();
            Console.WriteLine("=== Анализатор ИМТ (ООП) ===");
            Console.WriteLine("1. Новый замер");
            Console.WriteLine("2. История измерений");
            Console.WriteLine("3. Анализ динамики");
            Console.WriteLine("4. Сравнить два замера");
            Console.WriteLine("5. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    NewMeasurement();
                    break;
                case "2":
                    Console.Clear();
                    analyzer.ShowHistory();
                    Wait();
                    break;
                case "3":
                    Console.Clear();
                    analyzer.AnalyzeTrends();
                    Wait();
                    break;
                case "4":
                    CompareMenu();
                    break;
                case "5":
                    menu = false;
                    break;
                default:
                    Console.WriteLine("Ошибка выбора!");
                    Wait();
                    break;
            }
        }
    }



    static void NewMeasurement()
    {
        Console.Clear();
        Console.WriteLine("=== Новый замер ===");

        try
        {
            double height = InputHeight();
            double weight = InputWeight();
            int age = InputAge();
            string gender = InputGender();

            var measurement = new BmiMeasurement(weight, height, gender, age);

            Console.WriteLine("\nРезультат:");
            measurement.PrintReport();

            Console.WriteLine("\nСохранить этот замер? (д/н): ");
            if (Console.ReadLine().Trim().ToLower() == "д")
            {
                analyzer.AddMeasurement(measurement);
                Console.WriteLine("Замер сохранён!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Wait();
    }



    static void CompareMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Сравнение замеров ===");

        Console.Write("Введите номер первого замера: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите номер второго замера: ");
        int b = int.Parse(Console.ReadLine());

        Console.Clear();
        analyzer.CompareMeasurements(a, b);

        Wait();
    }


    static double InputHeight()
    {
        while (true)
        {
            Console.Write("Введите рост (в метрах или см): ");
            string input = Console.ReadLine().Trim();

            if (double.TryParse(input, out double h))
            {
                if (h > 50 && h < 250) h /= 100;
                if (h >= 1.0 && h <= 2.5) return h;
            }

            Console.WriteLine("Ошибка: рост должен быть 1.0–2.5 м.");
        }
    }

    static double InputWeight()
    {
        while (true)
        {
            Console.Write("Введите вес (кг): ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out double w) && w >= 30 && w <= 300)
                return w;

            Console.WriteLine("Ошибка: вес 30–300 кг.");
        }
    }

    static int InputAge()
    {
        while (true)
        {
            Console.Write("Введите возраст: ");
            if (int.TryParse(Console.ReadLine(), out int age) && age >= 1 && age <= 120)
                return age;

            Console.WriteLine("Ошибка: возраст 1–120.");
        }
    }

    static string InputGender()
    {
        while (true)
        {
            Console.Write("Введите пол (м/ж): ");
            string g = Console.ReadLine().Trim().ToLower();

            if (g == "м" || g == "ж")
                return g;

            Console.WriteLine("Ошибка: введите м/ж.");
        }
    }

    // ───────────────────────────────────────────────

    static void Wait()
    {
        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }
}

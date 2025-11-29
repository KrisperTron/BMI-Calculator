using System;
using System.Linq;

public class BmiAnalyzer
{
    private const int MaxRecords = 10;
    private BmiMeasurement[] measurements = new BmiMeasurement[MaxRecords];
    private int currentIndex = 0;

    public void AddMeasurement(BmiMeasurement m)
    {
        measurements[currentIndex] = m;
        currentIndex = (currentIndex + 1) % MaxRecords;
    }

    public void ShowHistory()
    {
        Console.WriteLine("=== История замеров ===");

        var list = measurements.Where(m => m != null).ToList();
        if (list.Count == 0)
        {
            Console.WriteLine("История пуста.");
            return;
        }

        foreach (var m in list)
        {
            Console.WriteLine();
            m.PrintReport();
        }
    }

    public void AnalyzeTrends()
    {
        var list = measurements.Where(m => m != null).ToList();
        if (list.Count < 1)
        {
            Console.WriteLine("Недостаточно данных.");
            return;
        }

        var first = list.First();
        var last = list.Last();
        double delta = last.BmiValue - first.BmiValue;

        Console.WriteLine("=== Динамика ===");
        Console.WriteLine($"Период: {first.MeasurementDate:dd.MM.yyyy} — {last.MeasurementDate:dd.MM.yyyy}");
        Console.WriteLine($"Изменение ИМТ: {(delta >= 0 ? "+" : "")}{delta:F2}  ({first.BmiValue:F1} → {last.BmiValue:F1})");

        DrawGraph(list);
    }

    public void CompareMeasurements(int a, int b)
    {
        var list = measurements.Where(m => m != null).ToList();
        if (a < 1 || b < 1 || a > list.Count || b > list.Count)
        {
            Console.WriteLine("Неверные номера измерений.");
            return;
        }

        var m1 = list[a - 1];
        var m2 = list[b - 1];

        Console.WriteLine("=== Сравнение ===");
        m1.PrintReport();
        Console.WriteLine();
        m2.PrintReport();

        Console.WriteLine($"\nРазница ИМТ: {(m2.BmiValue - m1.BmiValue):F2}");
    }

    // “График” — ASCII визуализация
    private void DrawGraph(System.Collections.Generic.List<BmiMeasurement> list)
    {
        Console.WriteLine("\nГрафик изменения ИМТ:");
        double min = list.Min(m => m.BmiValue);
        double max = list.Max(m => m.BmiValue);

        foreach (var m in list)
        {
            int len = (int)((m.BmiValue - min) / (max - min + 0.1) * 30);
            Console.WriteLine($"{m.MeasurementDate:dd.MM}: {new string('*', len)} {m.BmiValue:F1}");
        }
    }
}

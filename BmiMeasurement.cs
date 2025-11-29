using System;

public class BmiMeasurement
{
    public double Weight { get; set; }          // кг
    public double Height { get; set; }          // м
    public string Gender { get; set; }          // "м"/"ж"
    public int Age { get; set; }                // 1–120
    public double BmiValue { get; private set; }
    public string Category { get; private set; }
    public DateTime MeasurementDate { get; set; }

    public BmiMeasurement(double weight, double height, string gender, int age)
    {
        Weight = weight;
        Height = height;
        Gender = gender;
        Age = age;
        MeasurementDate = DateTime.Now;

        CalculateBmi();
        DetermineCategory();
    }

    public void CalculateBmi()
    {
        BmiValue = Weight / (Height * Height);
    }

    public void DetermineCategory()
    {
        if (BmiValue < 16) Category = "Выраженный дефицит";
        else if (BmiValue < 18.5) Category = "Недостаточный вес";
        else if (BmiValue < 25) Category = "Норма";
        else if (BmiValue < 30) Category = "Избыточный вес";
        else if (BmiValue < 35) Category = "Ожирение 1 степени";
        else if (BmiValue < 40) Category = "Ожирение 2 степени";
        else Category = "Ожирение 3 степени";
    }

    public void PrintReport()
    {
        Console.WriteLine($"Дата: {MeasurementDate:dd.MM.yyyy HH:mm}");
        Console.WriteLine($"Вес: {Weight} кг, Рост: {Height:F2} м");
        Console.WriteLine($"Возраст: {Age}, Пол: {Gender}");
        Console.WriteLine($"ИМТ: {BmiValue:F2} — {Category}");
    }
}

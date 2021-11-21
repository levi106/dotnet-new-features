using System.Text;

Point p1 = new(5, 6);
Point p2 = p1 with { X = 10 };
Console.WriteLine($"{p2}");

Person person1 = new("Nancy", "Davolio");
Person person2 = new("Nancy", "Davolio");
Console.WriteLine(person1);
Console.WriteLine($"{person1 == person2}");

PersonClass person3 = new("Nancy", "Davolio");
Console.WriteLine(person3.ToString());

PersonRecordDerived derived = new("Nancy", "Davolio", "US");
Console.WriteLine(derived.ToString());

DailyTemperature[] data = new DailyTemperature[] {
    new DailyTemperature(HighTemp: 57, LowTemp: 30),
    new DailyTemperature(60, 35),
    new DailyTemperature(63, 33),
    new DailyTemperature(68, 29),
    new DailyTemperature(72, 47),
    new DailyTemperature(75, 55),
    new DailyTemperature(77, 55),
    new DailyTemperature(72, 58),
    new DailyTemperature(70, 47),
    new DailyTemperature(77, 59),
    new DailyTemperature(85, 65),
    new DailyTemperature(87, 65),
    new DailyTemperature(85, 72),
    new DailyTemperature(83, 68),
    new DailyTemperature(77, 65),
    new DailyTemperature(72, 58),
    new DailyTemperature(77, 55),
    new DailyTemperature(76, 53),
    new DailyTemperature(80, 60),
    new DailyTemperature(85, 66)
};

foreach (var item in data)
    Console.WriteLine(item);

var temp1 = new DailyTemperature(10, 10);
temp1.HighTemp = 11;
var temp2 = new DailyTemperatureReadonly(10, 10);
// temp2.HighTemp = 11; // ERROR

public record Point(int X, int Y);

public record Person(string FirstName, string LastName);

public class PersonClass
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public PersonClass(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public record PersonRecordBase(string FirstName, string LastName)
{
    public sealed override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("BaseRecord");
        builder.Append(" { ");
        if (PrintMembers(builder))
        {
            builder.Append(" ");
        }
        builder.Append("}");
        return builder.ToString();
    }
}

public record PersonRecordDerived(string FirstName, string LastName, string Locale)
    : PersonRecordBase(FirstName, LastName)
{
    // ERROR
    //public override string ToString()
    //{
    //    StringBuilder builder = new();
    //    builder.Append("DerivedRecord");
    //    builder.Append(" { ");
    //    if (PrintMembers(builder))
    //    {
    //        builder.Append(" ");
    //    }
    //    builder.Append("}");
    //    return builder.ToString();
    //}
}

public readonly record struct DailyTemperatureReadonly(double HighTemp, double LowTemp)
{
    public double Mean => (HighTemp - LowTemp) / 2.0;
}
public record struct DailyTemperature(double HighTemp, double LowTemp)
{
    public double Mean => (HighTemp - LowTemp) / 2.0;
}

public struct DailyTemperatureStruct
{
    public double HighTemp { get; init; }
    public double LowTemp { get; init; }

    public DailyTemperatureStruct(int highTemp, int lowTemp)
    {
        HighTemp = highTemp;
        LowTemp = lowTemp;
    }
}

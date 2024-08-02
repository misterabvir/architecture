using L03.Models.Base;
using L03.Models.Enums;

namespace L03.Models;

public class Honda(Color color) : Car("Honda", color, 4, CarType.Sedan, FuelType.Gasoline, TransmissionType.Automatic)
{
    public override void Drive()
    {
        Console.WriteLine($"Honda drives down the street");
    }
}

using L03.Models.Base;
using L03.Models.Enums;

namespace L03.Models;

public class Bmw(Color color) : Car("BMW", color, 4, CarType.Coupe, FuelType.Diesel, TransmissionType.Manual)
{
    public override void Drive()
    {
        Console.WriteLine($"BMW drives down the street");
    }
}
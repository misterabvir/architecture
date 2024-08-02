using L03.Models.Base;
using L03.Models.Enums;

namespace L03.Models;

public class Station : IStation
{
    public string Name { get; } = "Station#" + Random.Shared.Next(10, 99);
    
    private bool IsFuelAvailable(FuelType fuelType) => Random.Shared.Next(0, 4) != 1;
    public bool IsServiceWindShieldCleaningAvailable { get; set; } = Random.Shared.Next(0, 4) != 1;
    public bool IsServiceHeadlightCleaningAvailable { get; set; } = Random.Shared.Next(0, 4) != 1;
    public bool IsServiceMirrorCleaningAvailable { get; set; } = Random.Shared.Next(0, 4) != 1;

    public decimal Refuel(Car car)
    {
        if (IsFuelAvailable(car.FuelType))
        {
            Console.WriteLine($"{car.Brand} was refueled");
            return (decimal)Random.Shared.NextDouble() * 100;
        }
        else
        {
            Console.WriteLine($"{car.Brand} was not refueled");
            return 0;
        }
    }

    public decimal WindshieldCleaning(Car car)
    {
        if (IsServiceWindShieldCleaningAvailable)
        {
            Console.WriteLine($"{car.Brand} windshield was cleaned");
            return (decimal)Random.Shared.NextDouble() * 100;
        }
        else
        {
            Console.WriteLine("Windshield cleaning not provided");
            return 0;
        }
    }

    public decimal HeadlightCleaning(Car car)
    {
        if (IsServiceHeadlightCleaningAvailable)
        {
            Console.WriteLine($"{car.Brand} headlight was cleaned");
            return (decimal)Random.Shared.NextDouble() * 100;
        }
        else
        {
            Console.WriteLine("Headlight cleaning not provided");
            return 0;
        }
    }

    public decimal MirrorCleaning(Car car)
    {
        if (IsServiceMirrorCleaningAvailable)
        {
            Console.WriteLine($"{car.Brand} mirror was cleaned");
            return (decimal)Random.Shared.NextDouble() * 100;
        }
        else
        {
            Console.WriteLine("Mirror cleaning not provided");
            return 0;
        }
    }
}
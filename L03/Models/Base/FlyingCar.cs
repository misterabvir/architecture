using L03.Models.Enums;

namespace L03.Models.Base;

public abstract class FlyingCar(string brand, Color color, int countOfWheel, CarType carType, FuelType fuelType, TransmissionType transmissionType) : Car(brand, color, countOfWheel, carType, fuelType, transmissionType)
{
    public override void Drive()
    {
        System.Console.WriteLine($"{Brand} is flying");
    }
}

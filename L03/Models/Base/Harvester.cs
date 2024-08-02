using L03.Models.Enums;

namespace L03.Models.Base;

public abstract class Harvester(string brand,
                                Color color,
                                int countOfWheel,
                                CarType carType,
                                FuelType fuelType,
                                TransmissionType transmissionType) :
                                Car(
                                    brand,
                                    color,
                                    countOfWheel,
                                    carType,
                                    fuelType,
                                    transmissionType)
{
    public void SweepStreet()
    {
        System.Console.WriteLine($"{Brand} is sweeping streets");
    }
}

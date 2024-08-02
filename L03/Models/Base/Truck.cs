using L03.Models.Base;
using L03.Models.Enums;

namespace L03.Models.Base;

public abstract class Truck(string brand,
                                CarType carType,
                                Color color,
                                int countOfWheel,
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
    public bool IsCargoLoaded { get; set; } = false;
    
    public void LoadCargo()
    {
        IsCargoLoaded = true;
        Console.WriteLine("Cargo loaded");
    }
    public void UnloadCargo()
    {
        IsCargoLoaded = false;
        Console.WriteLine("Cargo unloaded");
    }
}
using L03.Models.Enums;

namespace L03.Models.Base;

public abstract class Car(string brand, Color color, int countOfWheel, CarType carType,  FuelType fuelType, TransmissionType transmissionType)
{
    public string Brand => brand;
    public Color Color => color;
    public FuelType FuelType => fuelType;
    public TransmissionType TransmissionType => transmissionType;
    public CarType CarType => carType;
    public int CountOfWheel => countOfWheel;
    public bool IsFogLightsOn { get; private set; } = false;
    public bool IsWipersOn { get; private set; } = false;

    override public string ToString()
    {
        return $"""
                {Brand}: 
                    Color: {Color}, 
                    Fuel Type: {FuelType}, 
                    Transmission Type: {TransmissionType}, 
                    Car Type: {CarType}
                """;
    }

    public abstract void Drive();
    
    public virtual void ShiftingGear()
    {
        System.Console.WriteLine(TransmissionType switch
        {
            TransmissionType.Automatic => $"Shifting gear in automatic transmission",
            _ => $"Shifting gear in manual transmission"
        });
    }

    public bool SwitchingWipers() => IsWipersOn = !IsWipersOn;
    public bool SwitchFogLights() => IsFogLightsOn = !IsFogLightsOn;

    public decimal Fuel(IStation station)
    {
        System.Console.WriteLine($"{Brand} is trying refuel on the '{station.Name}'");
        return station.Refuel(this);
    }

    public virtual decimal Maintenance(IStation station)
    {
        System.Console.WriteLine($"{Brand} is trying get services on the '{station.Name}'");
        return 
            station.HeadlightCleaning(this) + 
            station.MirrorCleaning(this) + 
            station.WindshieldCleaning(this);
    }
}
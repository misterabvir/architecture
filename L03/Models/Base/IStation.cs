using L03.Models.Enums;

namespace L03.Models.Base;

public interface IStation
{
    string Name { get; }
    decimal Refuel(Car car);
    decimal WindshieldCleaning(Car car);
    decimal HeadlightCleaning(Car car);
    decimal MirrorCleaning(Car car);
}
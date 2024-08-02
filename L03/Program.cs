using L03.Models;
using L03.Models.Base;
using L03.Models.Enums;

Honda honda = new (Color.White);
Bmw bmw = new (Color.Black);

List<Car> cars = [honda, bmw];

foreach (var car in cars)
{
    var price = car.Fuel(new Station());
    Console.WriteLine($"{car.Brand} {price:C0}");
}

/*OUTPUT
Honda is trying refuel on the 'Station#65'
Honda was not refueled
Honda $0
BMW is trying refuel on the 'Station#56'
BMW was refueled
BMW $10
*/

foreach (var car in cars)
{
    var price = car.Maintenance(new Station());
    Console.WriteLine($"{car.Brand} {price:C0}");
}

/*OUTPUT
Honda is trying get services on the 'Station#55'
Headlight cleaning not provided
Honda mirror was cleaned
Honda windshield was cleaned
Honda $171
BMW is trying get services on the 'Station#30'
BMW headlight was cleaned
BMW mirror was cleaned
BMW windshield was cleaned
BMW $262
*/


foreach (var car in cars)
{
    car.Drive();
}

/*OUTPUT
Honda drives down the street
BMW drives down the street
*/

foreach (var car in cars)
{
    System.Console.WriteLine(car);
}
/*OUTPUT
Honda:
    Color: White,
    Fuel Type: Gasoline,
    Transmission Type: Automatic,
    Car Type: Sedan
BMW:
    Color: Black,
    Fuel Type: Diesel,
    Transmission Type: Manual,
    Car Type: Coupe
*/

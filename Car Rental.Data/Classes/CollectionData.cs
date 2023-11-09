using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;
using System.Reflection;
namespace Car_Rental.Data.Classes;
public class CollectionData : IData
{
	// Listor som man hämtar och skickar data till av de olika interfacen.
	readonly List<IPerson> _persons = new();
	readonly List<IVehicle> _vehicles = new();
	readonly List<IBooking> _bookings = new();
	/* Properties vars syfte är att ge unika idn till objektet
	   man lägger till någon av listorna. */
	public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(x => x.Id) + 1;
	public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(x => x.Id) + 1;
	public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(x => x.Id) + 1;
	/* Skapar man en instans av CollectionData
	   anropas metoden SeedData. */
	public CollectionData() => SeedData();
	/* Metoden SeedData lägger till data till 
	 tidigare nämnda listor. */
	void SeedData()
	{
		_persons.Add(new Customer(NextPersonId, 12345, "Doe", "John"));
		_persons.Add(new Customer(NextPersonId, 98765, "Doe", "Jane"));
		_vehicles.Add(new Car(NextVehicleId, "ABC123", "Volvo", 10000, 1, VehicleTypes.Combi, 200));
		_vehicles.Add(new Car(NextVehicleId, "DEF456", "Saab", 20000, 1, VehicleTypes.Sedan, 100));
		_vehicles.Add(new Car(NextVehicleId, "GHI789", "Tesla", 1000, 3, VehicleTypes.Sedan, 100));
		_vehicles.Add(new Car(NextVehicleId, "JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van, 300));
		_vehicles.Add(new Motorcycle(NextVehicleId, "MNO234", "Yamaha", 30000, 0.5, VehicleTypes.Motorcycle, 50));
		_bookings.Add(new Booking(NextBookingId, _vehicles[2].RegNo, (Customer)_persons[0],
		_vehicles[2].OdoMeter, new DateTime(2023, 11, 1)));
		_bookings[0].ReturnVehicle(_vehicles[2]).ReturnVehicleStatus(_vehicles[2]);
		_bookings.Add(new Booking(NextBookingId, _vehicles[3].RegNo,
		(Customer)_persons[1], _vehicles[3].OdoMeter,
		new DateTime(2023, 11, 1), new DateTime(2023, 11, 3), 5000));
		_bookings[1].ReturnVehicle(_vehicles[3])
	   .ReturnVehicleStatus(_vehicles[3], _bookings[1], (double)_bookings[1].KmReturned!);
	}
	/* En generisk metod som använder refelections för att
	   returnera en lista som överenstämmer med datatypen samt det 
	   man filtrerar. */
	public List<T> Get<T>(Expression<Func<T, bool>>? expression) where T : class
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
	   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
		?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = ((List<T>)value).AsQueryable();
		if (expression is null) return collection.ToList();
		return collection.Where(expression).ToList();
	}
	/* En generisk metod som använder refelections för att
	   returnera ett objekt som överenstämmer med datatypen samt det 
	   man filtrerar. */
	public T? Single<T>(Expression<Func<T, bool>>? expression) where T : class
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
	   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
		?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = ((List<T>)value).AsQueryable();
		if (expression is null) return null;
		var item = collection.SingleOrDefault(expression);
		return item ?? throw new ArgumentNullException();
	}
	/* En generisk metod som använder refelections för att
	   lägga till ett objekt till en lista som överenstämmer med datatypen. */
	public void Add<T>(T item) where T : class
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
	   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
		?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = (List<T>)value;
		collection.Add(item);
	}
	/* En generisk metod som använder refelections för att
	   ta bort ett objekt ifrån en lista som överenstämmer med datatypen. */
	public void Remove<T>(T item) where T : class
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
	   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
		?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = (List<T>)value;
		collection.Remove(item);
	}
	/* En asynkron metod som används för att hyra ett fordon.
	   För att därmed kunna lägga till en ny bokning. */
	public async Task<List<IBooking>> RentVehicle(IVehicle vehicle, IPerson person)
	{
		var _vehicle = _vehicles.FirstOrDefault(v => v.Equals(vehicle)) ?? throw new ArgumentNullException();
		var _person = _persons.FirstOrDefault(p => p.Equals(person)) ?? throw new ArgumentNullException();
		var booking = new Booking(NextBookingId, _vehicle.RegNo, (Customer)_person,
			_vehicle.OdoMeter, _vehicle.VehicleLastReneted);
		await Task.Delay(5000);
		booking.SetBookingStatus().
			ReturnVehicleStatus(_vehicle);
		_bookings.Add(booking);
		return _bookings;
	}
	// Metod som används för att återlämna ett fordon.
	public IBooking ReturnVehicle(IVehicle vehicle, double distance, int days)
	{
		var _vehicle = _vehicles.FirstOrDefault(v => v.Equals(vehicle)) ?? throw new ArgumentNullException();
		var booking = _bookings.LastOrDefault(b => b.RegNo.Equals(_vehicle.RegNo)
		&& b.KmReneted.Equals(vehicle.OdoMeter)) ?? throw new ArgumentNullException();
		var kmReturned = _vehicle.OdoMeter + distance;
		var km = kmReturned - booking.KmReneted;
		booking.Reneted.Duration(days, booking, _vehicle)
			.CalculateCost(_vehicle, km)
			.SetBookingValues(booking, kmReturned).
			ReturnVehicleStatus(_vehicle, booking, kmReturned);
		return booking;
	}
}
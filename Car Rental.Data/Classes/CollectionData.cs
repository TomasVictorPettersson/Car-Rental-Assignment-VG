using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Car_Rental.Data.Classes;
public class CollectionData : IData
{
	// Listor som sparar data av de olika interfacen.
	readonly List<IPerson> _persons = new();
	readonly List<IVehicle> _vehicles = new();
	readonly List<IBooking> _bookings = new();
	/* Skapar man en instans av CollectionData
	 anropas metoden SeedData. */
	public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(x => x.Id) + 1;
	public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(x => x.Id) + 1;
	public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(x => x.Id) + 1;
	public string? RegNoInput { get; set; } = null;
	public string? MakeInput { get; set; } = null;
	public int OdoMeterInput { get; set; }
	public double CostPerKmInput { get; set; }
	public VehicleTypes VehicleType { get; set; }
	public int? SSNInput { get; set; } = null;
	public string? FirstNameInput { get; set; } = null;
	public string? LastNameInput { get; set; } = null;
	public CollectionData() => SeedData();
	/* Metoden SeedData lägger till data till 
	 tidigare nämnda listor. */
	void SeedData()
	{
		var customerOneId = NextPersonId;
		var customerOne = new Customer(customerOneId, 12345, "Doe", "John");
		_persons.Add(customerOne);
		var customerTwoId = NextPersonId;
		var customerTwo = new Customer(customerTwoId, 98765, "Doe", "Jane");
		_persons.Add(customerTwo);
		var vehicleOneId = NextVehicleId;
		var vehicleOne = new Car(vehicleOneId, "ABC123", "Volvo", 10000, 1, VehicleTypes.Combi,
			200);
		_vehicles.Add(vehicleOne);
		var vehicleTwoId = NextVehicleId;
		var vehicleTwo = new Car(vehicleTwoId, "DEF456", "Saab", 20000, 1, VehicleTypes.Sedan,
			100);
		_vehicles.Add(vehicleTwo);
		var vehicleThreeId = NextVehicleId;
		var vehicleThree = new Car(vehicleThreeId, "GHI789", "Tesla", 1000, 3, VehicleTypes.Sedan,
			100);
		_vehicles.Add(vehicleThree);
		var vehicleFourId = NextVehicleId;
		var vehicleFour = new Car(vehicleFourId, "JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van,
			300);
		_vehicles.Add(vehicleFour);
		var vehicleFiveId = NextVehicleId;
		var vehicleFive = new Motorcycle(vehicleFiveId, "MNO234", "Yamaha",
			30000, 0.5, VehicleTypes.Motorcycle, 50);
		_vehicles.Add(vehicleFive);
		var bookingOneId = NextBookingId;
		var bookingOne = new Booking(bookingOneId, vehicleThree.RegNo, customerOne, vehicleThree.OdoMeter,
			new DateTime(2023, 9, 20));
		bookingOne.ReturnVehicle(vehicleThree);
		_bookings.Add(bookingOne);
		var bookingTwoId = NextBookingId;
		var bookingTwo = new Booking(bookingTwoId, vehicleFour.RegNo, customerTwo, vehicleFour.OdoMeter,
			new DateTime(2023, 9, 20), new DateTime(2023, 9, 20), 5000);
		bookingTwo.ReturnVehicle(vehicleFour);
		_bookings.Add(bookingTwo);
	}
	public List<T> Get<T>(Expression<Func<T, bool>>? expression) 
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
			   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
			   ?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = ((List<T>)value).AsQueryable();
		if (expression is null) return collection.ToList();
		return collection.Where(expression).ToList();
	}
	public T? Single<T>(Expression<Func<T, bool>>? expression)
	{
		throw new NotImplementedException();
	}
	public void Add<T>(T item)
	{
		if (item is Customer)
		{
			_persons.Add((IPerson)item);
		}
		else if (item is Vehicle)
		{
			_vehicles.Add((IVehicle)item);
		}
	}
	public IBooking RentVehicle(int vehicleId, int customerId)
	{
		throw new NotImplementedException();
	}
	public IBooking ReturnVehicle(int vehicleId)
	{
		throw new NotImplementedException();
	}
	public IEnumerable<IPerson> GetPersons() => _persons;
	public IEnumerable<IBooking> GetBookings() => _bookings;
	public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) => _vehicles;
}
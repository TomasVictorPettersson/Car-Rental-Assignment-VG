﻿using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
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
		var _personsCopy = _persons.DistinctBy(p => p.SSN).ToList();
		_persons.Clear();
		_persons.AddRange(_personsCopy);
		var vehicleOneId = NextVehicleId;
		var vehicleOne = new Car(vehicleOneId, "ABC123", "Volvo", 10000, 1, VehicleTypes.Combi,
			200); ;
		_vehicles.Add(vehicleOne);
		var vehicleTwoId = NextVehicleId;
		var vehicleTwo = new Car(vehicleTwoId, "DEF456", "Saab", 20000, 1, VehicleTypes.Sedan, 100);
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
		var _vehiclesCopy = _vehicles.DistinctBy(v => v.RegNo).ToList();
		_vehicles.Clear();
		_vehicles.AddRange(_vehiclesCopy);
		var bookingOneId = NextBookingId;
		var bookingOne = new Booking(bookingOneId, vehicleThree.RegNo, customerOne, vehicleThree.OdoMeter,
			new DateTime(2023, 11, 3));
		bookingOne.ReturnVehicle(vehicleThree)
			.ReturnVehicleStatus(vehicleThree);
		_bookings.Add(bookingOne);
		var bookingTwoId = NextBookingId;
		var bookingTwo = new Booking(bookingTwoId, vehicleFour.RegNo, customerTwo, vehicleFour.OdoMeter,
			new DateTime(2023, 11, 3), new DateTime(2023, 11, 3), 5000);
		bookingTwo.ReturnVehicle(vehicleFour)
			.ReturnVehicleStatus(vehicleFour, bookingTwo, (double)bookingTwo.KmReturned!);
		_bookings.Add(bookingTwo);
		var bookingsCopy = _bookings.DistinctBy(b => b.RegNo).ToList();
		_bookings.Clear();
		_bookings.AddRange(bookingsCopy);
	}
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
	public void Add<T>(T item) where T : class
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
	   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
		?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = (List<T>)value;
		collection.Add(item);
	}
	public void Remove<T>(T item) where T : class
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
	   .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
		?? throw new InvalidOperationException("Unsupported type");
		var value = collections.GetValue(this) ?? throw new InvalidDataException();
		var collection = (List<T>)value;
		collection.Remove(item);
	}
	public async Task<List<IBooking>> RentVehicle(IVehicle vehicle, IPerson person)
	{
		var bookingId = NextBookingId;
		var _vehicle = _vehicles.FirstOrDefault(v => v.Equals(vehicle)) ?? throw new ArgumentNullException();
		var _person = _persons.FirstOrDefault(p => p.Equals(person)) ?? throw new ArgumentNullException();
		var booking = new Booking(bookingId, _vehicle.RegNo, (Customer)_person,
			_vehicle.OdoMeter, _vehicle.VehicleLastReneted);
		await Task.Delay(5000);
		booking.SetBookingStatus().
			ReturnVehicleStatus(_vehicle);
		_bookings.Add(booking);
		return _bookings;
	}
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
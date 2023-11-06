﻿using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
namespace Car_Rental.Business.Classes;
/* BookingProcessor klassens syfte är att hämta data som 
   skickas vidare till gränsnittet i Car Rental G. */
public class BookingProcessor
{
	/* För att göra det möjligt krävs först en injektion av IData
	i BookingProcessors konstruktor. */
	private readonly IData _db;
	public BookingProcessor(IData db) => _db = db;
	/* Anropar sedan metoder som ligger i 
	   CollectionData klassen i Data projektet. */
	public string? RegNo { get; set; }
	public string? Make { get; set; }
	public double OdoMeter { get; set; }
	public double CostPerKm { get; set; }
	public double CostPerDay { get; set; }
	public int? SSN { get; set; }
	public string? LastName { get; set; }
	public string? FirstName { get; set; }
	public string Message { get; set; } = string.Empty;
	public string? CustomerSSN { get; set; }
	public double? Distance { get; set; } = null;
	public int? Days { get; set; } = null;
	public bool IsProcessing { get; set; }
	public string? VehicleType { get; set; }
	public string[] BookingStatusNames => _db.BookingStatusNames();
	public string[] VehicleStatusNames => _db.VehicleStatusNames();
	public string[] VehicleTypeNames => _db.VehicleTypeNames();
	public BookingStatuses GetBookingStatus(string name) => _db.GetBookingStatus(name);
	public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);
	public VehicleStatuses GetVehicleStatus(string name) => _db.GetVehicleStatus(name);
	public IEnumerable<IPerson> GetPersons() => _db.Get<IPerson>(p => p.Equals(p)).OrderBy(p => p.SSN);
	public IEnumerable<IBooking> GetBookings() => _db.Get<IBooking>(b => b.Equals(b));
	public IEnumerable<IVehicle> GetVehicles()
		=> _db.Get<IVehicle>(v => v.Equals(v)).OrderBy(v => v.RegNo);
	public IBooking? GetBooking(int bookingId) => _db.Single<IBooking>(b => b.Id.Equals(bookingId));
	public IPerson? GetPerson(string ssn) => _db.Single<IPerson>(p => p.SSN.ToString().Equals(ssn));
	public IVehicle? GetVehicle(int vehicleId) => _db.Single<IVehicle>(v => v.Id.Equals(vehicleId));
	public IVehicle? GetVehicle(string regNo) => _db.Single<IVehicle>(v => v.RegNo.Equals(regNo));
	public void AddCustomer(int? ssn, string lastName, string firstName)
	{
		Message = string.Empty;
		try
		{
			if (ssn is null || lastName is null || firstName is null)
			{
				throw new ArgumentException("Could not add customer.");
			}
			bool isSSNTaken = default;
			if (isSSNTaken = GetPersons().Any(p => p.SSN.Equals(ssn)))
			{
				throw new ArgumentException($"A customer with SSN {ssn} already exists.");
			}
			else if (ssn?.ToString().Length < 5)
			{
				throw new ArgumentException("SSN must contain 5 numbers.");
			}
			var customerId = _db.NextPersonId;
			var customer = new Customer(customerId, (int)ssn!, lastName.FirstCharSubstring(),
				firstName.FirstCharSubstring());
			_db.Add<IPerson>(customer);
		}
		catch (ArgumentException ex)
		{
			Message = ex.Message;
		}
		SSN = null;
		LastName = null;
		FirstName = null;
	}
	public void AddVehicle(string regNo, string make, double odoMeter,
		double costPerKm, VehicleTypes vehicleType, double costPerDay)
	{
		Message = string.Empty;
		try
		{
			if (regNo is null || make is null ||
				odoMeter < 0 || costPerKm < 0 || costPerDay < 0)
			{
				throw new ArgumentException("Could not add vehicle.");
			}
			bool isRegNoTaken = default;
			if (isRegNoTaken = GetVehicles().Any(v => v.RegNo.ToLower().Equals(regNo.ToLower())))
			{
				throw new ArgumentException($"A vehicle with RegNo {regNo.ToUpper()} already exists.");
			}
			else if (isRegNoTaken = GetBookings().Any(b => b.RegNo.ToLower().Equals(regNo.ToLower())))
			{
				throw new ArgumentException($"A booking with vehicle RegNo {regNo.ToUpper()} already exists.");
			}
			else if (regNo.Length < 6)
			{
				throw new ArgumentException("RegNo must be 6 characters long.");
			}
			var vehicleId = _db.NextVehicleId;
			var vehicle = new Vehicle(vehicleId, regNo.ToUpper(), make.FirstCharSubstring(),
				odoMeter, costPerKm, vehicleType, costPerDay);
			_db.Add<IVehicle>(vehicle);
		}
		catch (ArgumentException ex)
		{
			Message = ex.Message;
		}
		catch (Exception ex)
		{
			Message = ex.Message;
		}
		RegNo = null;
		Make = null;
		OdoMeter = default;
		CostPerKm = default;
		VehicleType = default;
		CostPerDay = default;
	}
	public void RemoveBooking(IBooking booking)
	{
		Message = string.Empty;
		try
		{
			var vehicle = GetVehicles().SingleOrDefault(v => v.RegNo.Equals(booking.RegNo))
				?? throw new ArgumentNullException();
			vehicle.VehicleStatus = VehicleStatuses.Available;
			vehicle.VehicleLastReneted = booking.Reneted;
			_db.Remove(booking);
		}
		catch (ArgumentNullException ex)
		{
			Message = ex.Message;
		}
		catch (Exception ex)
		{
			Message = ex.Message;
		}
	}
	public async Task<List<IBooking>> RentVehicle(IVehicle vehicle, IPerson person)
	{
		List<IBooking> booking = new();
		Message = string.Empty;
		try
		{
			if (person is null)
			{
				throw new ArgumentException("Must select a customer to be able to rent a car.");
			}
			IsProcessing = true;
			booking = await _db.RentVehicle(vehicle, person);
			IsProcessing = false;
		}
		catch (ArgumentException ex)
		{
			Message = ex.Message;
		}
		catch (Exception ex)
		{
			Message = ex.Message;
		}
		CustomerSSN = null;
		return (List<IBooking>)(booking.ToList() ?? Enumerable.Empty<IBooking>());
	}
	public IBooking? ReturnVehicle(IVehicle vehicle, double? distance, int? days)
	{
		Message = string.Empty;
		try
		{
			if (distance is null || days is null)
			{
				throw new ArgumentException("Distance or Days cannot have an empty value.");
			}
			else if (distance < 0 || days < 0)
			{
				throw new ArgumentException("Distance or Days cannot have a value less than zero.");
			}
			var booking = _db.ReturnVehicle(vehicle, (double)distance, (int)days);
			Distance = null;
			Days = null;
			return booking;
		}
		catch (ArgumentException ex)
		{
			Message = ex.Message;
			return null;
		}
		catch (Exception ex)
		{
			Message = ex.Message;
			return null;
		}
	}
}
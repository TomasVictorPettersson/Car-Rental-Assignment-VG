using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Linq.Expressions;

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
	public string? RegNoInput
	{
		get => _db.RegNoInput;
		set => _db.RegNoInput = value;
	}
	public string? MakeInput
	{
		get => _db.MakeInput;
		set => _db.MakeInput = value;
	}
	public double OdoMeterInput
	{
		get => _db.OdoMeterInput;
		set => _db.OdoMeterInput = value;
	}
	public double CostPerKmInput
	{
		get => _db.CostPerKmInput;
		set => _db.CostPerKmInput = value;
	}
	public VehicleTypes VehicleTypeInput
	{
		get => _db.VehicleTypeInput;
		set => _db.VehicleTypeInput = value;
	}
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public int? SSNInput
	{
		get => _db.SSNInput;
		set => _db.SSNInput = value;
	}
	public string? LastNameInput
	{
		get => _db.LastNameInput;
		set => _db.LastNameInput = value;
	}
	public string? FirstNameInput
	{
		get => _db.FirstNameInput;
		set => _db.FirstNameInput = value;
	}
	public string Message
	{
		get => _db.Message;
		set => _db.Message = value;
	}
	public void AddCustomer(int? sSN, string lastName, string firstName)
	{
		Message = string.Empty;
		try
		{
			if (sSN is null || lastName is null || firstName is null)
			{
				throw new ArgumentException("Could not add customer.");
			}
			var customerId = _db.NextPersonId;
			var customer = new Customer(customerId, (int)sSN, lastName, firstName);
			_db.Add<IPerson>(customer);
		}
		catch (ArgumentException ex)
		{
			Message = ex.Message;
		}
		SSNInput = null;
		LastNameInput = null;
		FirstNameInput = null;
	}
	public void AddVehicle(string regNo, string make, double odoMeter, double costPerKm, VehicleTypes vehicleType)
	{
		Message = string.Empty;
		try
		{
			if (regNo is null || make is null)
			{
				throw new ArgumentException("Could not add vehicle.");
			}
			var vehicleId = _db.NextVehicleId;
			var vehicle = new Vehicle(vehicleId, regNo.ToUpper(), make, odoMeter, costPerKm, vehicleType);
			_db.Add<IVehicle>(vehicle);

		}
		catch (ArgumentException ex)
		{
			Message = ex.Message;
		}
		RegNoInput = null;
		MakeInput = null;
		OdoMeterInput = default;
		CostPerKmInput = default;
		VehicleTypeInput = default;
	}
	public IEnumerable<IPerson> GetPersons() => _db.Get<IPerson>(p => p.Equals(p));
	public IEnumerable<IBooking> GetBookings() => _db.Get<IBooking>(b => b.Equals(b));
	public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
		=> _db.Get<IVehicle>(v => v.Equals(v));
	/* public IVehicle? GetVehicle(int vehicleId) { }
	public IVehicle? GetVehicle(string regNo) { }
	public lägg till asynkron returdata typ RentVehicle(int vehicleId, int
	customerId)
	{
	// Använd Task.Delay för att simulera tiden det tar
	// att hämta data från ett API.
	}
	public IBooking ReturnVehicle(int vehicleId, double ditance) { }	
	// Calling Default Interface Methods
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name) */
}
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
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
	public VehicleTypes VehicleType { get; set; }
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public int? SSN { get; set; }
	public string? LastName { get; set; }
	public string? FirstName { get; set; }
	public string Message { get; set; } = string.Empty;
	public Customer Customer { get; set; }
	public double? Distance { get; set; } = null;
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
		SSN = null;
		LastName = null;
		FirstName = null;
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
		RegNo = null;
		Make = null;
		OdoMeter = default;
		CostPerKm = default;
		VehicleType = default;
	}
	public IEnumerable<IPerson> GetPersons() => _db.Get<IPerson>(p => p.Equals(p));
	public IEnumerable<IBooking> GetBookings() => _db.Get<IBooking>(b => b.Equals(b));
	public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
		=> _db.Get<IVehicle>(v => v.Equals(v));
	public async Task<List<IBooking>> RentVehicle(int vehicleId, int customerId)
	{
		return await _db.RentVehicle(vehicleId, customerId);
	}
	public IBooking ReturnVehicle(int vehicleId, string vehicleRegNo, double? distance) => _db.ReturnVehicle(vehicleId, vehicleRegNo, (double)distance);
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
	public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name) */
}
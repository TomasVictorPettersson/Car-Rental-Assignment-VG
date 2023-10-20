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
	public IEnumerable<IPerson> GetPersons() => _db.GetPersons();
	public IEnumerable<IBooking> GetBookings() => _db.GetBookings().OrderBy(x => x.RegNo);
	public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
		=> _db.GetVehicles(status).OrderBy(v => v.RegNo);
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public int SSNInput
	{
		get => _db.SSNInput;
		set
		{
			_db.SSNInput = value;
		}
	}	
	public string? FirstNameInput
	{
		get => _db.FirstNameInput;
		set
		{
			_db.FirstNameInput = value;
		}
	}
	public string? LastNameInput
	{
		get => _db.LastNameInput;
		set
		{
			_db.LastNameInput = value;
		}
	}
	public void AddPerson() => _db.AddPerson(SSNInput,LastNameInput,FirstNameInput);
	

	
	/* public IVehicle? GetVehicle(int vehicleId) { }
	public IVehicle? GetVehicle(string regNo) { }
	public lägg till asynkron returdata typ RentVehicle(int vehicleId, int
	customerId)
	{
	// Använd Task.Delay för att simulera tiden det tar
	// att hämta data från ett API.
	}
	public IBooking ReturnVehicle(int vehicleId, double ditance) { }
	public void AddVehicle(string make, string registrationNumber, double
	odometer, double costKm, VehicleStatuses status, VehicleTypes type) { }
	public void AddCustomer(string socialSecurityNumber, string firstName, string
	lastName) { }
	// Calling Default Interface Methods
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name) */
}
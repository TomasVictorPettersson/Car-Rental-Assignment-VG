using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Linq.Expressions;
namespace Car_Rental.Data.Interfaces;
public interface IData
{
	List<T> Get<T>(Expression<Func<T, bool>>? expression);
	T? Single<T>(Expression<Func<T, bool>>? expression);
	void Add<T>(T item);
	int NextVehicleId { get; }
	int NextPersonId { get; }
	int NextBookingId { get; }
	string? RegNoInput { get; set; }
	string? MakeInput { get; set; }
	int OdoMeterInput { get; set; }
	double CostPerKmInput { get; set; }
	string[] VehicleTypeNames => (string[])Enum.GetValues(typeof(VehicleTypes));
	int? SSNInput { get; set; }
	string? FirstNameInput { get; set; }
	string? LastNameInput { get; set; }
	void AddPerson(int? sSN, string? lasttName, string? firstName);
	IBooking RentVehicle(int vehicleId, int customerId);
	IBooking ReturnVehicle(int vehicleId);
	// Default Interface Methods
	string[] VehicleStatusNames => (string[])Enum.GetValues(typeof(VehicleStatuses));
	
	/*
	public VehicleTypes GetVehicleType(string name) => 
	*/
	IEnumerable<IPerson> GetPersons();
    IEnumerable<IBooking> GetBookings();
    IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default);	
}
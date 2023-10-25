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
	double OdoMeterInput { get; set; }
	double CostPerKmInput { get; set; }
	VehicleTypes VehicleTypeInput { get; set; }
	string[] VehicleTypeNames => (string[])Enum.GetValues(typeof(VehicleTypes));
	int? SSNInput { get; set; }
	string? LastNameInput { get; set; }
	string? FirstNameInput { get; set; }
	string Message { get; set; }
	/*
	IBooking RentVehicle(int vehicleId, int customerId);
	*/
	IBooking ReturnVehicle(int vehicleId);
	// Default Interface Methods
	string[] VehicleStatusNames => (string[])Enum.GetValues(typeof(VehicleStatuses));
	/*
	public VehicleTypes GetVehicleType(string name) => 
	*/
}
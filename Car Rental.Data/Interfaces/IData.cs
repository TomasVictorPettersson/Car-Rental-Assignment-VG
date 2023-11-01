using Car_Rental.Common.Classes;
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
	string[] VehicleTypeNames => (string[])Enum.GetValues(typeof(VehicleTypes));
	public Task<List<IBooking>> RentVehicle(int vehicleId, int customerId);
	public IBooking ReturnVehicle(int vehicleId,double distance);
	// Default Interface Methods
	string[] VehicleStatusNames => (string[])Enum.GetValues(typeof(VehicleStatuses));
	/*
	public VehicleTypes GetVehicleType(string name) => 
	*/
}
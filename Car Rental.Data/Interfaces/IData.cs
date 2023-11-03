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
	string[] BookingStatusNames => Enum.GetNames(typeof(BookingStatuses));
	string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
	string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
	Task<List<IBooking>> RentVehicle(int vehicleId, int customerId);
	IBooking ReturnVehicle(int vehicleId,double distance, int days);
	/*
	public VehicleTypes GetVehicleType(string name) => 
	*/
}
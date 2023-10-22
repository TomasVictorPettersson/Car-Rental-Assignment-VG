using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Interfaces;
public interface IVehicle
{
	int Id { get; init; }
	string RegNo { get; init;}
	string Make { get; init; }
	double OdoMeter { get; init; }
	double CostPerKm { get; init; }
	VehicleTypes VehicleType { get; init; }
	double? CostPerDay { get; init; }
	VehicleStatuses VehicleStatus { get; }
	void ReturnVehicleStatus(BookingStatuses bookingStatus);
}
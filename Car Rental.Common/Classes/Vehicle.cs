using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;
public class Vehicle : IVehicle
{
	public int Id { get; init; }
	public string RegNo { get; init; }
	public string Make { get; init; }
	public double Odometer { get; init; }
	public double CostPerKm { get; init; }
	public VehicleTypes VehicleType { get; init; }
	public double CostPerDay { get; init; }
	public VehicleStatuses VehicleStatus { get; private set;}
	public void ReturnVehicleStatus(BookingStatuses bookingStatus)
	{
		if (bookingStatus.Equals(BookingStatuses.Closed))
		{
			VehicleStatus = VehicleStatuses.Available;
		}
		else if (bookingStatus.Equals(BookingStatuses.Open))
		{
			VehicleStatus = VehicleStatuses.Booked;
		}
		else if (bookingStatus.Equals(BookingStatuses.None))
		{
			VehicleStatus = VehicleStatuses.Unknown;
		}
	}	
	public Vehicle(int id, string regNo, string make, int odometer, double costPerKm,
		VehicleTypes vehicleType, double costPerDay) =>
		(Id, RegNo, Make, Odometer, CostPerKm, VehicleType, CostPerDay) =
		(id, regNo, make, odometer, costPerKm, vehicleType, costPerDay);
}
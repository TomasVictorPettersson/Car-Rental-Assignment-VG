using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;
public class Vehicle : IVehicle
{
	public int Id { get; init; }
	public string RegNo { get; init; }
	public string Make { get; init; }
	public double OdoMeter { get; set; }
	public double CostPerKm { get; init; }
	public VehicleTypes VehicleType { get; init; }
	public double CostPerDay { get; init; }
	public VehicleStatuses VehicleStatus { get; set; }	
	public DateTime VehicleLastReneted { get; set; } = DateTime.Now;
	public Vehicle(int id, string regNo, string make, double odoMeter, double costPerKm,
		VehicleTypes vehicleType, double costPerDay) =>
		(Id, RegNo, Make, OdoMeter, CostPerKm, VehicleType, CostPerDay) =
		(id, regNo, make, odoMeter, costPerKm, vehicleType, costPerDay);
}
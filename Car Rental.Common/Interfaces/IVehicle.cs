using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Interfaces;
public interface IVehicle
{
	int Id { get; }
	public string RegNo { get;}
	public string Make { get;}
	public double OdoMeter { get; set; }
	public double CostPerKm { get; }	
	double CostPerDay { get; }
	VehicleTypes VehicleType { get; }
	VehicleStatuses VehicleStatus { get; set; }
	DateTime VehicleLastReneted { get; set; }
}
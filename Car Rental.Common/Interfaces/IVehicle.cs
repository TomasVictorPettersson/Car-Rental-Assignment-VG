namespace Car_Rental.Common.Interfaces;
public interface IVehicle
{
	int Id { get; }
	public string RegNo { get;}
	public string Make { get;}
	public double OdoMeter { get; set; }
	public double CostPerKm { get; }
	string VehicleType { get;}
	double CostPerDay { get; }
	string VehicleStatus { get; set; }
	DateTime VehicleLastReneted { get; set; }
}
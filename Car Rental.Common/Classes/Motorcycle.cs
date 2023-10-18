using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Classes;
public class Motorcycle : Vehicle
{
	public Motorcycle(int id, string regNo, string make, int odometer, double costPerKm,
		VehicleTypes vehicleType, double costPerDay) :
		base(id, regNo, make, odometer, costPerKm, vehicleType, costPerDay)
	=> (Id, RegNo, Make, Odometer, CostPerKm, VehicleType, CostPerDay) =
		(id, regNo, make, odometer, costPerKm, vehicleType, costPerDay);
}
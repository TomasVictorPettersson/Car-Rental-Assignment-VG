namespace Car_Rental.Common.Classes;
public class Car : Vehicle
{    
    public Car(int id, string regNo, string make, int odoMeter, double costPerKm,
		string vehicleType, double costPerDay) :
		base(id, regNo, make, odoMeter, costPerKm, vehicleType, costPerDay)
	{ }	
}
﻿using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Classes;
public class Motorcycle : Vehicle
{
	public Motorcycle(int id, string regNo, string make, int odoMeter, double costPerKm,
		VehicleTypes vehicleType, double costPerDay) :
		base(id, regNo, make, odoMeter, costPerKm, vehicleType, costPerDay)
	{ }
}
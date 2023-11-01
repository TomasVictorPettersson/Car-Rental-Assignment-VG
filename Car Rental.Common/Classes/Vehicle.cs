﻿using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;
public class Vehicle : IVehicle
{
	public int Id { get; init; }
	public string RegNo { get; init; }
	public string Make { get; init; }
	public double OdoMeter { get; private set; }
	public double CostPerKm { get; init; }
	public VehicleTypes VehicleType { get; init; }
	public double CostPerDay { get; init; }
	public VehicleStatuses VehicleStatus { get; private set; }
	public void ReturnVehicleStatus(BookingStatuses bookingStatus, double kmReturned = 0)
	{
		if (bookingStatus.Equals(BookingStatuses.Closed))
		{
			OdoMeter = kmReturned;
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
	public Vehicle(int id, string regNo, string make, double odoMeter, double costPerKm,
		VehicleTypes vehicleType, double costPerDay) =>
		(Id, RegNo, Make, OdoMeter, CostPerKm, VehicleType, CostPerDay) =
		(id, regNo, make, odoMeter, costPerKm, vehicleType, costPerDay);
}
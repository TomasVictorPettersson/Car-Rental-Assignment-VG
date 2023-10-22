﻿using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Interfaces;
public interface IVehicle
{
	int Id { get; }
	public string RegNo { get;}
	public string Make { get;}
	public double OdoMeter { get; }
	public double CostPerKm { get; }
	VehicleTypes VehicleType { get;}
	double? CostPerDay { get; }
	VehicleStatuses VehicleStatus { get; }
	void ReturnVehicleStatus(BookingStatuses bookingStatus);
}
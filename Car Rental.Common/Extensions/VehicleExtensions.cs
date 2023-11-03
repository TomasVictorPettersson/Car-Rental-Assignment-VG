﻿using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Extensions;
public static class VehicleExtensions
{
	public static BookingStatuses SetBookingStatus(this IBooking booking)
	{
		booking.BookingStatus = BookingStatuses.Open;
		return booking.BookingStatus;
	}
	public static int Duration(this DateTime startDate, int days,
		IBooking booking, IVehicle vehicle)
	{
		var endDate = vehicle.VehicleLastReneted;
		if (startDate < DateTime.Now || startDate > DateTime.Now)
		{
			endDate = startDate;
		}
		double duration = default;
		if (days.Equals(0))
		{
			duration = (endDate - startDate).TotalDays + 1;
		}
		else if (days > 0)
		{
			duration = (endDate - startDate).TotalDays + 1 + days;
		}
		booking.Returned = endDate.AddDays(duration - 1);
		vehicle.VehicleLastReneted = booking.Returned;
		return (int)duration;
	}
	public static double CalculateCost(this int duration, IVehicle vehicle, double km)
	{
		var cost = duration * vehicle.CostPerDay + km * vehicle.CostPerKm;
		return cost;
	}
	public static BookingStatuses SetBookingValues(this double cost, IBooking booking,
		double kmReturned)
	{
		booking.KmReturned = kmReturned;
		booking.Cost = cost;
		booking.BookingStatus = BookingStatuses.Closed;
		return booking.BookingStatus;
	}
	public static void ReturnVehicleStatus(this BookingStatuses bookingStatus,
		IVehicle vehicle, double kmReturned = 0)
	{
		if (bookingStatus.Equals(BookingStatuses.Closed))
		{
			vehicle.OdoMeter = kmReturned;
			vehicle.VehicleStatus = VehicleStatuses.Available;
		}
		else if (bookingStatus.Equals(BookingStatuses.Open))
		{
			vehicle.VehicleStatus = VehicleStatuses.Booked;
		}
		else if (bookingStatus.Equals(BookingStatuses.None))
		{
			vehicle.VehicleStatus = VehicleStatuses.Unknown;
		}
	}
}
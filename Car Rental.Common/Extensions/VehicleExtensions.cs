using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Extensions;
public static class VehicleExtensions
{
	public static BookingStatuses SetBookingStatus(this IBooking booking)
	{
		booking.BookingStatus = BookingStatuses.Open;
		return booking.BookingStatus;
	}
	public static int Duration(this DateTime startDate, DateTime endDate)
	{
		var days = (endDate - startDate).TotalDays + 1;
		return (int)days;
	}
	public static double CalculateCost(this int days, IVehicle vehicle, double km)
	{
		var cost = days * vehicle.CostPerDay + km * vehicle.CostPerKm;
		return (double)cost;
	}
	public static BookingStatuses SetBookingValues(this double cost, IBooking booking,
		double kmReturned, DateTime returned)
	{
		booking.KmReturned = kmReturned;
		booking.Returned = returned;
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
	}
}
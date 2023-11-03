using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Extensions;
public static class VehicleExtensions
{
	public static string SetBookingStatus(this IBooking booking, string[] bookingStatusNames)
	{
		booking.BookingStatus = bookingStatusNames[2];
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
	public static string SetBookingValues(this double cost, IBooking booking,
		double kmReturned, string[] bookingStatusNames)
	{
		booking.KmReturned = kmReturned;
		booking.Cost = cost;
		booking.BookingStatus = bookingStatusNames[0];
		return booking.BookingStatus;
	}
	public static void ReturnVehicleStatus(this string bookingStatus,
		IVehicle vehicle, string[] bookingStatusNames, 
		string[] vehicleStatusNames, double kmReturned = 0)
	{
		if (bookingStatus.Equals(bookingStatusNames[0]))
		{
			vehicle.OdoMeter = kmReturned;
			vehicle.VehicleStatus = vehicleStatusNames[0];
		}
		else if (bookingStatus.Equals(bookingStatusNames[1]))
		{
			vehicle.VehicleStatus = vehicleStatusNames[2];
		}
		else if (bookingStatus.Equals(bookingStatusNames[2]))
		{
			vehicle.VehicleStatus = vehicleStatusNames[1];
		}	
	}
}
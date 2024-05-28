using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Extensions;

public static class VehicleExtensions
{
    /* Extension metod som används till att sätta property hos
	   ett objekt av en klass som implementerar interfacet iBooking
	   till BookingStatuses.Open. */

    public static BookingStatuses SetBookingStatus(this IBooking booking)
    {
        booking.BookingStatus = BookingStatuses.Open;
        return booking.BookingStatus;
    }

    /* Extension metod som används till att räkna ut varaktigheten
	   emellan när man hyr ett fordon och sedan återlämnar det. */

    public static int Duration(this DateTime startDate, int days,
        IBooking booking, IVehicle vehicle)
    {
        var endDate = vehicle.VehicleLastReneted;
        if (startDate < DateTime.Now || startDate > DateTime.Now)
        {
            endDate = startDate;
        }
        var duration = (endDate - startDate).TotalDays + 1 + days;
        booking.Returned = endDate.AddDays(duration - 1);
        return (int)duration;
    }

    // Extension metod som används till att beräkna kostnad av biluthyrning.
    public static double CalculateCost(this int duration, IVehicle vehicle, double km)
    {
        var cost = duration * vehicle.CostPerDay + km * vehicle.CostPerKm;
        return cost;
    }

    /* Extension metod som används till att sätta värden på olika properties
	   på ett objekt av en klass som implementerar interfacet iBooking. */

    public static BookingStatuses SetBookingValues(this double cost, IBooking booking,
        double kmReturned)
    {
        booking.KmReturned = kmReturned;
        booking.Cost = cost;
        booking.BookingStatus = BookingStatuses.Closed;
        return booking.BookingStatus;
    }

    /* Extension metod som används till att sätta värden på olika properties
	   på ett objekt av en klass som implementerar interfacet iVehicle. */

    public static void ReturnVehicleStatus(this BookingStatuses bookingStatus,
        IVehicle vehicle, IBooking? booking = null, double kmReturned = 0)
    {
        if (bookingStatus.Equals(BookingStatuses.Closed))
        {
            vehicle.VehicleLastReneted = booking!.Returned;
            vehicle.OdoMeter = kmReturned;
            vehicle.VehicleStatus = VehicleStatuses.Available;
        }
        else if (bookingStatus.Equals(BookingStatuses.Open))
        {
            vehicle.VehicleStatus = VehicleStatuses.Booked;
        }
        else
        {
            vehicle.VehicleStatus = VehicleStatuses.Unknown;
        }
    }
}
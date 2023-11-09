using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Extensions;
public static class BookingExtensions
{	
	public static BookingStatuses ReturnVehicle(this IBooking booking, 
		IVehicle vehicle)
	{
		/* Variablerna days och km nyttjas för att beräkna bokningskostnad.
		   Bokningskostnad tilldelas sedan till propertyn Cost. */
		var days = (booking.Returned - booking.Reneted).TotalDays + 1;
		var km = booking.KmReturned - booking.KmReneted;
		try
		{
			/* OM km är större eller lika med 0 OCH days är större eller
			 lika med ett så tilldelas Cost ett värde.
			 BookingStatus tilldelas värdet BookingStatuses.Closed. */
			if (km >= 0 && days >= 1)
			{			
				booking.Cost = days * vehicle.CostPerDay + km * vehicle.CostPerKm;
				booking.BookingStatus = BookingStatuses.Closed;
				booking.KmReturned = vehicle.OdoMeter + km;
				return booking.BookingStatus;
			}
			/* ANNARS OM KmReturned är null OCH Returned har defaultvärde så			
			   tilldelas BookingStatus värdet Open.  */
			else if (booking.KmReturned is null && booking.Returned.Equals(default))
			{
				booking.BookingStatus = BookingStatuses.Open;
				return booking.BookingStatus;
			}
			/* Uppfylls inte någon av ovanstående selektioner så hamnar
			   man i andra selektioner. 
			   Beroende på värdena på variablerna och properties.
			   Gemensamt för dessa selektioner är att om man hamnar i en av dem
			   så kastas ett nytt ArgumentException med ett specifikt
			   felmeddelande för just den selektionen. */
			else if (booking.Returned != default && days <= 0 && booking.KmReturned < booking.KmReneted)
			{
				throw new ArgumentException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Km Returned and Returned cannot be less than Km Reneted and Reneted.");
			}
			else if (booking.Returned != default && days <= 0 && booking.KmReturned >= booking.KmReneted)
			{
				throw new ArgumentException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Returned cannot be less than Reneted.");
			}
			else if (booking.Returned != default && days <= 0 && booking.KmReturned is null)
			{
				throw new ArgumentException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Returned cannot be less than Reneted and Km Returned has no value.");
			}
			else if (booking.Returned != default && days >= 1 && booking.KmReturned is null)
			{
				throw new ArgumentException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Km Returned has no value.");
			}
			else
			{
				throw new ArgumentException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Km Returned cannot be less than Km Reneted.");
			}
		}
		/* Kastas ett nytt ArgumentException hamnar man i catch-blocket.
		   Här tilldelas BookingStatus värdet BookingStatuses.None.	       		   		   
		   Och slutligen tilldelas Message propertyn
		   det felmeddelande från selektionen där
		   ArgumentException kastades. */
		catch (ArgumentException ex)
		{
			booking.BookingStatus = BookingStatuses.None;
			booking.Message = ex.Message;
			return booking.BookingStatus;
		}
	}
}
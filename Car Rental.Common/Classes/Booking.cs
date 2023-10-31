using Car_Rental.Common.Enums;
using Car_Rental.Common.Exceptions;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;
public class Booking : IBooking
{
	public int Id { get; init; }
	public string RegNo { get; init; }
	public Customer Customer { get; init; }
	public double KmReneted { get; init; }
	public double? KmReturned { get; private set; }
	public DateTime Reneted { get; init; }
	public DateTime Returned { get; set; }
	public double? Cost { get; private set; }
	public BookingStatuses BookingStatus { get; private set; }
	public string Message { get; private set; } = string.Empty;
	public void SetBookingStatus(BookingStatuses bookingStatus)
	{
		BookingStatus = bookingStatus;
	}
	public void SetBookingValues(double kmReturned, double cost, BookingStatuses bookingStatus)
	{
		KmReturned = kmReturned;
		Cost = cost;
		BookingStatus = bookingStatus;
	}
	public void ReturnVehicle(IVehicle vehicle)
	{
		/* Variablerna days och km nyttjas för beräkna bokningskostnad.
		   Bokningskostnad tilldelas till propertyn Cost. */
		var days = (Returned - Reneted).TotalDays + 1;
		var km = KmReturned - KmReneted;
		try
		{
			/* OM km är större eller lika med 0 OCH days är större eller
			 lika med ett så tilldelas Cost ett värde.
			 BookingStatus tilldelas värdet Closed.
			 Anropar sedan metoden ReturnVehicleStatus med BookingStatus som
			 argument för att ändra VehicleStatus till Available. */
			if (km >= 0 && days >= 1)
			{
				Cost = days * vehicle.CostPerDay + km * vehicle.CostPerKm;
				BookingStatus = BookingStatuses.Closed;
				vehicle.ReturnVehicleStatus(BookingStatus);
			}
			/* ANNARS OM KmReturned är null OCH Returned har defaultvärde så			
			   tilldelas BookingStatus värdet Open.
			   Anropar sedan metoden ReturnVehicleStatus med BookingStatus som
			   argument för att ändra VehicleStatus till Booked. */
			else if (KmReturned is null && Returned.Equals(default))
			{
				BookingStatus = BookingStatuses.Open;
				vehicle.ReturnVehicleStatus(BookingStatus);
			}
			/* Uppfylls inte någon av ovanstående if-satserna så hamnar
			   man i andra if-satser. Beroende på värdena på variablerna och properties.
			   Gemensamt för dessa if-satser är att om man hamnar i ett av dem
			   så kastas ett nytt BookingException med ett specifikt
			   felmeddelande för just den if-satsen. */
			else if (Returned != default && days <= 0 && KmReturned < KmReneted)
			{
				throw new BookingException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Km Returned and Returned cannot be less than Km Reneted and Reneted.");
			}
			else if (Returned != default && days <= 0 && KmReturned >= KmReneted)
			{
				throw new BookingException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Returned cannot be less than Reneted.");
			}
			else if (Returned != default && days <= 0 && KmReturned is null)
			{
				throw new BookingException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Returned cannot be less than Reneted and Km Returned has no value.");
			}
			else if (Returned != default && days >= 1 && KmReturned is null)
			{
				throw new BookingException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Km Returned has no value.");
			}
			else if (Returned != default && days >= 1 && KmReturned < KmReneted)
			{
				throw new BookingException($"For booking with vehicle RegNo {vehicle.RegNo}. " +
					$"Km Returned cannot be less than Km Reneted.");
			}
		}
		/* Kastas ett nytt BookingException hamnar man i catch-blocket.
		   Här tilldelas BookingStatus värdet None.
	       Anropar sedan metoden ReturnVehicleStatus med BookingStatus som
		   argument för att ändra VehicleStatus till Unknown. 		   		   
		   Och slutligen tilldela Message propertyn
		   det felmeddelande från if-blocket där
		   BookingException kastades. */
		catch (BookingException ex)
		{
			BookingStatus = BookingStatuses.None;
			vehicle.ReturnVehicleStatus(BookingStatus);
			Message = ex.Message;
		}
	}
    public Booking(int id, string regNo, Customer customer, double kmReneted,
		DateTime reneted, DateTime returned = default, double? kmReturned = null) =>
		(Id, RegNo, Customer, KmReneted, KmReturned, Reneted, Returned) =
		(id, regNo, customer, kmReneted, kmReturned, reneted, returned);
}
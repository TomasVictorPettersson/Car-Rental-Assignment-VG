using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Interfaces;
public interface IBooking
{
	int Id { get; }
	string RegNo { get; }
	Customer Customer { get; }
	double KmReneted { get; }
	double? KmReturned { get;}
	DateTime Reneted { get; }
	DateTime Returned { get; set; }
	double? Cost { get; }
	BookingStatuses BookingStatus { get;}
	string Message { get; }
	public void SetBookingStatus(BookingStatuses bookingStatus);
	void SetBookingValues(double kmReturned, double cost, BookingStatuses bookingStatus);
	void ReturnVehicle(IVehicle vehicle);
}
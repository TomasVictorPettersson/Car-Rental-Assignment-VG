using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Interfaces;
public interface IBooking
{
	int Id { get; }
	string RegNo { get; }
	Customer Customer { get; }
	double KmReneted { get; }
	double? KmReturned { get; set; }
	DateTime Reneted { get; }
	DateTime Returned { get; set; }
	double? Cost { get; set; }
	BookingStatuses BookingStatus { get; }
	string Message { get; }
	void ReturnVehicle(IVehicle vehicle);
}
using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
namespace Car_Rental.Common.Interfaces;
public interface IBooking
{
	int Id { get; init; }
	string RegNo { get; init; }
	Customer Customer { get; init; }
	double KmReneted { get; init; }
	double? KmReturned { get; init; }
	DateTime Reneted { get; init; }
	DateTime Returned { get; init; }
	double? Cost { get; }
	BookingStatuses BookingStatus { get; }
	string Message { get; }
	void ReturnVehicle(IVehicle vehicle);
}
using Car_Rental.Common.Classes;
namespace Car_Rental.Common.Interfaces;
public interface IBooking
{
	int Id { get; }
	string RegNo { get; }
	Customer Customer { get; }
	double KmReneted { get; }
	double? KmReturned { get; set; }
	DateTime Reneted { get; set; }
	DateTime Returned { get; set; }
	double? Cost { get; set; }
	string? BookingStatus { get; set; }
	string Message { get; set; }
}
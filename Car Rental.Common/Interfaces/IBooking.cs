using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    int Id { get; }
    string RegNo { get; }
    Customer Customer { get; }
    double KmRented { get; }
    double? KmReturned { get; set; }
    DateTime Rented { get; set; }
    DateTime Returned { get; set; }
    double? Cost { get; set; }
    BookingStatuses BookingStatus { get; set; }
    string Message { get; set; }
}
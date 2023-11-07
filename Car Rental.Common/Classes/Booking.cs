using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;
public class Booking : IBooking
{
	public int Id { get; init; }
	public string RegNo { get; init; }
	public Customer Customer { get; init; }
	public double KmReneted { get; init; }
	public double? KmReturned { get; set; }
	public DateTime Reneted { get; set; }
	public DateTime Returned { get; set; }
	public double? Cost { get; set; }
	public BookingStatuses BookingStatus { get; set; }
	public string Message { get; set; } = string.Empty;
	public Booking(int id, string regNo, Customer customer, double kmReneted,
		DateTime reneted, DateTime returned = default, double? kmReturned = null) =>
		(Id, RegNo, Customer, KmReneted, KmReturned, Reneted, Returned) =
		(id, regNo, customer, kmReneted, kmReturned, reneted, returned);
}
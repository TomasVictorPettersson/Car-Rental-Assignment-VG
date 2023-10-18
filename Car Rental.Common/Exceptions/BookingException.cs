namespace Car_Rental.Common.Exceptions;
public class BookingException : ArgumentException
{
	public BookingException(string message) : base(message)
	{
	}
}
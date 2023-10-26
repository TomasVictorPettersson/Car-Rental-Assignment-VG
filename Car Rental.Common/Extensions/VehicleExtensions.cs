namespace Car_Rental.Common.Extensions;
public static class VehicleExtensions
{
	public static int Duration(this DateTime startDate, DateTime endDate)
	{				
		var days = (startDate - endDate).TotalDays + 1;		
		return (int)days;
	}
}
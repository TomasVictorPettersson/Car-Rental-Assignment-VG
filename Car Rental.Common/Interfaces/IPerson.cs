namespace Car_Rental.Common.Interfaces;
public interface IPerson
{
	int Id { get; init; }
	public int SSN { get; init; }
	string LastName { get; init; }
	string FirstName { get; init; }
}
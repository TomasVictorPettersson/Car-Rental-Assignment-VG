namespace Car_Rental.Common.Interfaces;
public interface IPerson
{
	int Id { get; }
	public int SSN { get; }
	public string LastName { get; }
	public string FirstName { get; }
}
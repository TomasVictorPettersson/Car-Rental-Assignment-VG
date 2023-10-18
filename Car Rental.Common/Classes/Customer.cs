using Car_Rental.Common.Interfaces;
namespace Car_Rental.Common.Classes;
public class Customer : IPerson
{
	public int Id { get; init; }
	public int SSN { get; init; }
    public string LastName { get; init; }
    public string FirstName { get; init; }
    public Customer(int id,int sSN, string lastName, string firstName)
        => (Id, SSN, LastName, FirstName) = (id, sSN, lastName, firstName);
}
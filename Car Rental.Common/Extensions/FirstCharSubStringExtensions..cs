namespace Car_Rental.Common.Extensions;
public static class FirstCharSubStringExtensions
{
	public static string FirstCharSubstring(this string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return string.Empty;
		}
		return $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
	}
}
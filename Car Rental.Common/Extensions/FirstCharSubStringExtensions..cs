namespace Car_Rental.Common.Extensions;
public static class FirstCharSubStringExtensions
{
	/* Extensionmetod som omvandlar en sträng så att den första
	   bokstaven blir en versal och resten av bokstäverna 
	   i strängen blir gemener. */
	public static string FirstCharSubstring(this string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return string.Empty;
		}
		return $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
	}
}
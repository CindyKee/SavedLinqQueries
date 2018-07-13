<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Configuration</Namespace>
</Query>

void Main()
{
	const string prefix = "Alice.N.Wonderland";

	Console.WriteLine($"Using regex string validator: {UseRegexStringValidator(prefix)}");

	Console.WriteLine($"Using regex matching: {UseRegexMatching(prefix)}");

}

// Define other methods and classes here
public static bool UseRegexStringValidator(string test)
{
	const string regex = @"^(?=.{6,})[A-Za-z0-9_-]+(?:\.[A-Za-z0-9_-]+)*$";
	var myRegexValidator = new RegexStringValidator(regex);

	Console.WriteLine($"Can validate this object? {myRegexValidator.CanValidate(test.GetType())}");
	try
	{
		myRegexValidator.Validate(test);
		return true;
	}
	catch
	{
		return false;
	}
}

public static bool UseRegexMatching(string test)
{
	var regex = new Regex(@"^(?=.{6,})[A-Za-z0-9_-]+(?:\.[A-Za-z0-9_-]+)*$");
	
	return regex.IsMatch(test);
}
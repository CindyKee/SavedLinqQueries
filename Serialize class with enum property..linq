<Query Kind="Program">
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Domain.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var display = new Details { MultipleChoiceDisplayType = DisplayType.MultipleChoice };
	display.Dump();
	
	var test = new Test
	{
		CustomFieldDetailsJson = @"{ multipleChoiceDisplayType: 1 }"
	};
	
	test.Dump();
	
	var choice = test.GetDetails<Details>();
	choice.Dump();

	dynamic request = JsonConvert.DeserializeObject<dynamic>(test.CustomFieldDetailsJson);
	Console.WriteLine($"request type: {request.GetType().Name}");
	
	var details = (Details) request?.ToObject<Details>();
	details.Dump();
	
	// Null Json test
	test.CustomFieldDetailsJson = null;
	test.GetDetails<Details>().Dump();
	
	default(Details).Dump();
}

// Define other methods and classes here
public class Test 
{
	public string CustomFieldDetailsJson { get; set; }

	public T GetDetails<T>() => CustomFieldDetailsJson == null ? default(T) : JsonConvert.DeserializeObject<T>(CustomFieldDetailsJson);

	public void SetDetails<T>(T details) => CustomFieldDetailsJson = JsonConvert.SerializeObject(details);
}

public class Details
{
	public DisplayType MultipleChoiceDisplayType { get; set; }
}

public enum DisplayType
{
	DropDown = 0,
	MultipleChoice = 1
}
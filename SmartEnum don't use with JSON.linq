<Query Kind="Program">
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Domain.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var display = SmartEnum.MultipleChoice;
	display.Dump();
	
	var test = new Test
	{
		CustomFieldDetailsJson = @"{ DisplayType: { Value: 0 } }"
	};
	
	test.Dump();
	
	var fromValue = SmartEnum.FromValue(1);
	fromValue.Dump();
	
	var fromString = SmartEnum.FromString("Dropdown");
	fromString.Dump();
	
	var choice = test.GetDetails<Details>();
	
	choice.Dump();
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
	public SmartEnum DisplayType { get; set; }
}

public class SmartEnum
{
	public SmartEnum() {}
	
	private SmartEnum(int value, string name)
	{
		Value = value;
		Name = name;
	}

	public string Name { get; private set; }
	public int Value { get; private set; }

	public static SmartEnum DropDown { get; } = new SmartEnum(0, "DropDown");
	public static SmartEnum MultipleChoice { get; } = new SmartEnum(1, "Multiple Choice");

	public static IEnumerable<SmartEnum> List()
	{
		return new[] { DropDown, MultipleChoice };
	}

	public static SmartEnum FromString(string displayString)
	{
		return List().Single(d => string.Equals(d.Name, displayString, StringComparison.OrdinalIgnoreCase));
	}

	public static SmartEnum FromValue(int value)
	{
		return List().Single(d => d.Value == value);
	}
}
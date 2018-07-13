<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var json = @"{
		'Title': 'My Button Title',
		'Label': 'Button text',
		'Link': 'http://some.link.com'
	}";

	var x = CustomActionButtonDetailsFactory.NewFromType<ActionButtonLinkDetails>();
	x.GetType().Name.Dump();

	var abl = CustomActionButtonDetailsFactory.NewFromJson<ActionButtonLinkDetails>(json);

	Console.WriteLine(abl.GetType().Name);
	abl.Dump();
}

// Define other methods and classes here
public abstract class CustomActionButtonDetails
{
}

public class ActionButtonLinkDetails : CustomActionButtonDetails
{
	public string Title { get; set; }
	public string Label { get; set; }
	public Uri Link { get; set; }
}

public static class CustomActionButtonDetailsFactory
{
	public static T NewFromType<T>() where T: CustomActionButtonDetails, new ()
	{
		return new T();
	}
	
	public static T NewFromJson<T>(string json) where T: CustomActionButtonDetails, new ()
	{
		return JsonConvert.DeserializeObject<T>(json);
	}
}

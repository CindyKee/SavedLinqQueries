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
	
	var x = CustomActionButtonDetails<ActionButtonLinkDetails>.NewFromType();
	x.GetType().Name.Dump();
	
	var abl = CustomActionButtonDetails<ActionButtonLinkDetails>.NewFromJson(json);

	Console.WriteLine(abl.GetType().Name);
	abl.Dump();
}

// Define other methods and classes here
public abstract class CustomActionButtonDetails<T> 
	where T: CustomActionButtonDetails<T>, new ()
{
	public static T NewFromType()
	{
		return new T();
	}
	
	public static T NewFromJson(string json)
	{
		return JsonConvert.DeserializeObject<T>(json);
	}
}

public class ActionButtonLinkDetails : CustomActionButtonDetails<ActionButtonLinkDetails>
{
	public string Title { get; set; }
	public string Label { get; set; }
	public Uri Link { get; set; }
}
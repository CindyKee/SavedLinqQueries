<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
</Query>

void Main()
{
//	var x = JsonConvert.SerializeObject(null);
//	x.Dump();
//	JsonConvert.DeserializeObject(x).Dump();

	var field = new CustomField
	{
		CustomFieldType = CustomFieldType.ActionButton,
		CustomActionButtonType = CustomActionButtonType.LinkToPrimafact
	};

	var details = new ActionButtonLinkDetails
	{
		Title = "My Button Title",
		Icon = "Button Icon text",
		Link = new Uri("mailto:cindy@filevine.com")
	};
	field.SetDetails(details);
	field.Dump("Must always set details using SetDetails() method (except for EF).");
	field.CustomFieldDetailsJson.Dump("Using the built-in Get from the Json property.");

//	dynamic x = JsonConvert.SerializeObject(details).ToString();
//	//dynamic x = "{ 'Name': 'Cindy'}";
//	Console.WriteLine($"{x.GetType().Name}");
//	Console.WriteLine(x);
//	//field.CustomFieldDetailsJson = x;
//	field.SetDetails(x);
//	field.Dump();
	
//	dynamic y = JsonConvert.DeserializeObject<dynamic>(x);
//	Console.WriteLine($"y is: {y}\ny.GetType(): {y.GetType().Name}");
//	field.SetDetails<ActionButtonLinkDetails>(y);
//	field.CustomFieldDetailsJson.Dump("Simulate details from dynamic request object");


	// Access the static-typed property using GetDetails<T>
	var getDetails = field.GetDetails<ActionButtonLinkDetails>();
	getDetails.Icon = "Using property name of static typed object";
	getDetails.Dump("Details property using GetDetails<T> and changing subproperty");
	field.Dump("Show that original field's property has not changed because they are not the same reference");
	
	// Set the property using SetDetails<T>
	field.SetDetails(new ActionButtonLinkDetails { Icon = "Testing Property Set", Title = "Not empty" });
	field.Dump("Created a new details object and used SetDetails<T>");
	field.GetDetails<ActionButtonLinkDetails>().Dump("Show strongly-typed result after setting.");

	// Work with the dynamic property

	// Get using dynamic
	dynamic getDyn = JsonConvert.DeserializeObject<dynamic>(field.CustomFieldDetailsJson);
	Console.WriteLine($"Dynamic Icon: {getDyn.Icon}");
	Console.WriteLine($"Dynamic Title: {getDyn.Title}\n");
	
	dynamic dynamicDetails = field.CustomFieldDetails;
	Console.WriteLine($"Get by dynamic property\n{dynamicDetails}");
	Console.WriteLine($"Underlying type of dynamic property: {dynamicDetails.GetType().Name}");
	Console.WriteLine($"Dynamic Property.Icon: {dynamicDetails.Icon}");
	Console.WriteLine($"Dynamic Property.Icon from object: {field.CustomFieldDetails.Icon}");
	Console.WriteLine($"Dynamic Property.Title: {dynamicDetails.Title}");
	
	// Access the Json property
	var json = field.CustomFieldDetailsJson;
	json.Dump("Details represented as a JSON object using Property Get");
	json = json.Replace("empty", "****");

	// Test dynamic property with setting and getting null
	//field.SetDetails(default(ActionButtonLinkDetails));
	Console.WriteLine($"Dynamic property is still null: {field.CustomFieldDetails == null}");
	dynamic nullProperty = field.CustomFieldDetails;
	Console.WriteLine($"Dynamic property Get returns null: {nullProperty == null}");
	ActionButtonLinkDetails abl = field.GetDetails<ActionButtonLinkDetails>();
	Console.WriteLine($"ActionButtonLinkDetails should be null: {abl == null}");
	field.Dump();
}

// Define other methods and classes here
public class CustomField
{
	//private CustomFieldDetailsBase _customFieldDetails = null;

	public CustomFieldType CustomFieldType { get; set; }
	public CustomActionButtonType CustomActionButtonType { get; set; }

	public string CustomFieldDetailsJson { get; protected set; }

	[NotMapped]
	public dynamic CustomFieldDetails
	{
		get => JsonConvert.DeserializeObject<dynamic>(CustomFieldDetailsJson);
		protected set => CustomFieldDetailsJson = JsonConvert.SerializeObject(value);
	}
	
	public T GetDetails<T>() where T: CustomFieldDetailsBase => JsonConvert.DeserializeObject<T>(CustomFieldDetailsJson);
	public void SetDetails<T>(T details) where T: CustomFieldDetailsBase => CustomFieldDetailsJson = JsonConvert.SerializeObject(details);
}

public abstract class CustomFieldDetailsBase
{
}
	
public class ActionButtonLinkDetails : CustomFieldDetailsBase
{
	public string Title { get; set; }
	public string Icon { get; set; }
	public Uri Link { get; set; }
	public bool OpenInNewTab { get; set; }
	public Dictionary<string, string> Placeholders { get; set; } = new Dictionary<string, string>();
}

public enum CustomFieldType
{
	Integer = 1,
	Currency = 2,
	String = 3,
	Date = 4,
	Boolean = 5,
	Text = 6,
	PersonLink = 7,
	Doc = 8,
	Dropdown = 9,
	Header = 10,
	DocList = 11,
	DocGen = 12,
	PlainDecimal = 13,
	Percent = 14,
	Deadline = 15,
	Pro = 16,
	TextLarge = 17,

	//vv these are short term hacks 
	IncidentDate = 18,
	ProjectNumber = 19,
	//^^ these are short term hacks 

	ActionButton = 20,
	Instructions = 21,
	Url = 22
}

public enum CustomActionButtonType
{
	None = 0,
	SnagRecordsRequest = 1,
	SendToQuickbooks = 2,
	LinkToPrimafact = 3
}
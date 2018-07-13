<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
</Query>

void Main()
{
	var field = new CustomField
	{
		CustomFieldType = CustomFieldType.ActionButton,
		CustomActionButtonType = CustomActionButtonType.LinkToPrimafact,
		CustomFieldDetailsJson = @"{
			'Title': 'My Button Title',
			'Label': 'Button text',
			'Link': 'http://some.link.com'
		}"
	};
	
	field.Dump("Original CustomField using JSON string to initialize Json property");
	
	// Access the static-typed property
	var details = field.CustomFieldDetails as ActionButtonLinkDetails;
	details.Label = "Using property name of static typed object";
	details.Dump("Details property using Get and changing subproperty");
	field.Dump("Show that original field's property has also been changed - reference types");
	
	((ActionButtonLinkDetails)field.CustomFieldDetails).Label = "Add label with property name";
	field.Dump("Change the label again, but using casting to get to the correct field");
	
	// Set the strongly-typed property to null to remove it
	field.CustomFieldDetails = null;
	field.CustomFieldDetails.Dump("Nullified property, but setting should have created a new empty object and not null");
	
	field.CustomFieldDetails = new ActionButtonLinkDetails { Label = "Testing Property Set", Title = "Not empty" };
	field.Dump("Created a new details object and used setter");
	
	// Access the Json property
	var json = field.CustomFieldDetailsJson;
	json.Dump("Details represented as a JSON object using Property Get");
	json = json.Replace("empty", "****");
	field.CustomFieldDetailsJson = json;
	field.CustomFieldDetails.Dump("Loaded from a Json string using the Property Set");
}

// Define other methods and classes here
public class CustomField
{
	private CustomFieldDetailsBase _customFieldDetails = null;

	public CustomFieldType CustomFieldType { get; set; }
	public CustomActionButtonType CustomActionButtonType { get; set; }

	[NotMapped]
	public CustomFieldDetailsBase CustomFieldDetails
	{
		get { return _customFieldDetails ?? (_customFieldDetails = CustomFieldDetailsBase.NewCustomDetailsInstance(CustomFieldType, CustomActionButtonType)); }
		set { _customFieldDetails = (value ?? CustomFieldDetailsBase.NewCustomDetailsInstance(CustomFieldType, CustomActionButtonType)); }
	}

	public string CustomFieldDetailsJson
	{
		get { return JsonConvert.SerializeObject(CustomFieldDetails); }
		set
		{
			CustomFieldDetails = (value == null)
				? CustomFieldDetailsBase.NewCustomDetailsInstance(CustomFieldType, CustomActionButtonType)
				: CustomFieldDetailsBase.FromTypeAndJson(CustomFieldType, CustomActionButtonType, value);
		}
	}
}

public abstract class CustomFieldDetailsBase
{
	public static CustomFieldDetailsBase NewCustomDetailsInstance(CustomFieldType fieldType, CustomActionButtonType actionButtonType)
	{
		switch (fieldType)
		{
			case CustomFieldType.ActionButton:
				switch (actionButtonType)
				{
					case CustomActionButtonType.LinkToPrimafact:
						return new ActionButtonLinkDetails();
					default:
						throw new NotSupportedException();
				}
			default:
				throw new NotSupportedException();
		}
	}

	public static CustomFieldDetailsBase FromTypeAndJson(CustomFieldType fieldType, CustomActionButtonType actionButtonType, string value)
	{
		switch (fieldType)
		{
			case CustomFieldType.ActionButton:
				switch (actionButtonType)
				{
					case CustomActionButtonType.LinkToPrimafact:
						return JsonConvert.DeserializeObject<ActionButtonLinkDetails>(value);
					default:
						throw new NotSupportedException();
				}
			default:
				throw new NotSupportedException();
		}
	}
}
	
public class ActionButtonLinkDetails : CustomFieldDetailsBase
{
	public string Title { get; set; }
	public string Label { get; set; }
	public Uri Link { get; set; }
}

// Other notes
/*
CustomFieldDetailsBase.FromTypeAndJson() and CustomFieldDetailsBase.NewCustomDetailsInstance() 
can both end up calling something like ActionButtonLinkDetails.Deserialize(json)
*/

/*
 public string DetailsJson { get; set; }

[NotMapped]
public SomeBaseClass Details
//public dynamic Details
{
    get { return DetailsJson == null ? null : JsonConvert.DeserializeObject(DetailsJson); }
    set { DetailsJson = value == null ? null : JsonConvert.SerializeObject(value); }
}
public T GetDetails<T>() => JsonConvert.DeserializeObject<T>(DetailsJson);
public void SetDetails<T>(T details) { DetailsJson = JsonConvert.SerializeObject(details); }
*/


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
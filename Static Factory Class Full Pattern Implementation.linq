<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var x = Factory.GetDetails(CustomFieldType.ActionButton, CustomActionButtonType.LinkToPrimafact);
	
	x.GetType().Name.Dump();
	x.Dump();
	x.GetLabel().Dump();
	((ActionButtonLinkDetails)x).Title.Dump();
	
	var json = @"{
		'Title': 'My Button Title',
		'Label': 'Button text',
		'Link': 'http://some.link.com'
	}";
	
	Console.WriteLine("\n");
	
	var abl = Factory.GetDetailsFromJson(CustomFieldType.ActionButton, CustomActionButtonType.LinkToPrimafact, json);
	
	abl.GetType().Name.Dump();
	abl.Dump();
	abl.GetLabel().Dump();
	((ActionButtonLinkDetails)abl).Title.Dump();	
}

// Define other methods and classes here
public abstract class CustomActionButtonDetails
{
	public abstract string GetLabel();
}

public class ActionButtonLinkDetails : CustomActionButtonDetails
{
	public override string GetLabel()
	{
		 return this.Label;
	}
	
	public string Title { get; set; }
	public string Label { get; set; }
	public Uri Link { get; set; }
}

public class ActionButtonSnagRecordsDetails : CustomActionButtonDetails
{
	public override string GetLabel()
	{
		throw new NotImplementedException();
	}
}

public class Factory
{
	// The sample on Wikipedia did not make this method static, but it could be either way depending on DI needs.
	public static CustomActionButtonDetails GetDetails(CustomFieldType fieldType, CustomActionButtonType actionButtonType)
	{
		switch (fieldType)
		{
			case CustomFieldType.ActionButton:
				switch (actionButtonType)
				{
					case CustomActionButtonType.LinkToPrimafact:
						return new ActionButtonLinkDetails { Label = "A new Action Button Link", Title = "New from scratch" };
					default:
						throw new NotSupportedException();
				}
				
			default:
				throw new NotSupportedException();
		}
	}

	public static CustomActionButtonDetails GetDetailsFromJson(CustomFieldType fieldType, CustomActionButtonType actionButtonType, string json)
	{
		switch (fieldType)
		{
			case CustomFieldType.ActionButton:
				switch (actionButtonType)
				{
					case CustomActionButtonType.LinkToPrimafact:
						return JsonConvert.DeserializeObject<ActionButtonLinkDetails>(json);
					default:
						throw new NotSupportedException();
				}

			default:
				throw new NotSupportedException();
		}
	}

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

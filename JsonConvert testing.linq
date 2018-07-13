<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var orgID = 32;
	var customProjectType = Days.Monday;
	var details = new { orgID, CustomProjectType = customProjectType.ToString() };	
	JsonConvert.SerializeObject(details, Newtonsoft.Json.Formatting.Indented).Dump();
	
//	Enum.GetName(typeof(Days), customProjectType).Dump();
//	customProjectType.ToString().Dump();
	
	var newObject = new Foo
	{
		ID = 5,
		Name = "FooBar",
		Modified = DateTime.Now,
		AnotherProperty = "This is just another property"
	};

	JsonConvert.SerializeObject(newObject, Newtonsoft.Json.Formatting.Indented).Dump();
	JsonConvert.SerializeObject(new { newObject }, Newtonsoft.Json.Formatting.Indented).Dump();
}

enum Days { Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };
public class Foo 
{
	public int ID { get; set; }
	public string Name { get; set; }
	public DateTime Modified { get; set; }
	public string AnotherProperty { get; set; }
}
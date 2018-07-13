<Query Kind="Program">
  <Connection>
    <ID>2f1a4320-4d25-41e8-b80f-4fceedf63f2f</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Data.EF.dll</CustomAssemblyPath>
    <CustomTypeName>Filevine.Data.FilevineContext</CustomTypeName>
    <AppConfigPath>C:\GitHub\filevine\Filevine\Web.config</AppConfigPath>
    <DisplayName>FilevineContext</DisplayName>
  </Connection>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Common.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Domain.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Filevine</Namespace>
  <Namespace>Filevine.Common</Namespace>
  <Namespace>Filevine.Common.Cache.Objects</Namespace>
  <Namespace>Filevine.Controllers</Namespace>
  <Namespace>Filevine.Domain</Namespace>
  <Namespace>Filevine.Domain.CustomFieldDetails</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var stringList = new StringListDetails
	{
		StringListItems = new List<StringListItem>
		{
			new StringListItem { Value = "hay fever", Position = 0, ID = 1 },
			new StringListItem { Value = "peanuts", Position = 1, ID = 2 },
			new StringListItem { Value = "wheat", Position = 2, ID = 3 }
		}
	};
	
	var customField = CustomFields.Single(f => f.ID == 225360);
//	customField.CustomFieldDetails = stringList;
//	SaveChanges();
	
	customField.GetDetails<StringListDetails>().Dump();
	
//	string json = JsonConvert.SerializeObject(stringList);
//	json.Dump();
//	
//	stringList = JsonConvert.DeserializeObject<StringListDetails>(json ?? string.Empty);
//	Console.WriteLine($"Is stringList null? {stringList == null}");
//	
//	var jsonDetails = JsonConvert.SerializeObject(stringList);
//	Console.WriteLine($"Is jsonDetails null? {jsonDetails == null}");
//	Console.WriteLine($"Is jsonDetails empty? {jsonDetails == string.Empty}");
//	Console.WriteLine($"Is jsonDetails the string 'null'? {jsonDetails == "null"}");
//	jsonDetails.Dump();
//	
//	Console.WriteLine($"Is jsonDetails converted to an object null? {JsonConvert.DeserializeObject<StringListDetails>(jsonDetails) == null}");
//
//	////////////////////
//	Console.WriteLine($"Deserialize the emtpty string == null: {JsonConvert.DeserializeObject<StringListDetails>(string.Empty) == null}");
//	Console.WriteLine($"Deserialize the string 'null' == null: {JsonConvert.DeserializeObject<StringListDetails>("null") == null}");
//	// This fails = cannot pass null to this method!
//	//Console.WriteLine($"Deserialize a null: {JsonConvert.DeserializeObject<StringListDetails>(null)}");
//
//
//	
}

// Define other methods and classes here
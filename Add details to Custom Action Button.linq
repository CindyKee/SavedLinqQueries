<Query Kind="Statements">
  <Connection>
    <ID>2f1a4320-4d25-41e8-b80f-4fceedf63f2f</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\GitHub\filevine\Filevine.Data.EF\bin\Filevine.Data.EF.dll</CustomAssemblyPath>
    <CustomTypeName>Filevine.Data.FilevineContext</CustomTypeName>
    <AppConfigPath>C:\GitHub\filevine\Filevine\Web.config</AppConfigPath>
    <DisplayName>FilevineContext</DisplayName>
  </Connection>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Domain.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Filevine.Domain</Namespace>
  <Namespace>Filevine.Domain.CustomFieldDetails</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

var db = new FilevineContext();
var customField = db.CustomFields.Single(cf => cf.ID == 224330);
//var customField = db.CustomFields.Single(cf => cf.ID == 226384);
customField.Dump();


var abl = new ActionButtonLinkDetails
{
	Title = null,
	Icon = "fa-check",
//	Link = new Uri("http://example.com/{{projectemail}}").ToString(),
//	Link = "mailto:{projectemail}",
	Link = "An completely invalid string that cannot be a URL or an email link",
	OpenInNewTab = true
};
//abl.Dump();

customField.SetDetails(abl);

db.SaveChanges();


/*
dynamic request = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(abl));
Console.WriteLine($"dynamic request object: {request.ToString()}");
Console.WriteLine($"dynamic request object type: {request.GetType().Name}");
Console.WriteLine($"title field from dynamic request object: {request.Title.GetType().Name}");

// Try to convert request object of JObject type to a concrete type
var details = (ActionButtonLinkDetails) request.ToObject<ActionButtonLinkDetails>();
Console.WriteLine(details.GetType().Name);
details.Dump();

//customField.Dump();

*/
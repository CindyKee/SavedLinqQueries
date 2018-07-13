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
  <Namespace>Filevine.Domain.Entities</Namespace>
</Query>

var value = "has";

Projects.Where(p => p.OrgID == 376)
	.Where(p => (value == "has") == p.CustomData.OfType<PersonListCustomDataElement>()
			.Any(e => e.PersonListItems.Any(i => i.Person != null) && e.CustomSectionID == 27806 && e.CustomFieldID == 225367))
	.Dump();
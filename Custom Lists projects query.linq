<Query Kind="Expression">
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

//CustomData.OfType<StringListCustomDataElement>()
//Projects.Where(p => p.FirstPrimaryUserID != null)
//	.Select(p => new { p.ID, p.FirstPrimaryUserID})
//ProjectsQuery(1452, false, Filevine.Common.AccessLevel.Full)
//Orgs.Where(o => o.Name == "Handsome")

Projects.Where(p => p.OrgID == 376 && p.CustomData.OfType<StringListCustomDataElement>().Any())
	.Select(p => new { p.ID, p.UniqueKey, p.FirstPrimaryUserID, p.Client.Fullname, p.CustomData })

Projects.Where(p => p.OrgID == 376 && p.CustomData.OfType<StringListCustomDataElement>().Any(e => e.StringListItems.Any(i => i.StringValue != null)))
	.Select(p => new { p.ID, p.UniqueKey, p.FirstPrimaryUserID, p.Client.Fullname, p.CustomData })
	
Projects.Where(p => p.OrgID == 376 && p.CustomData.OfType<StringListCustomDataElement>().Any(e => e.StringListItems.Any(i => i.StringValue != "This is a first string")))
	.Select(p => new { p.ID, p.UniqueKey, p.FirstPrimaryUserID, p.Client.Fullname, p.CustomData })

Projects.Where(p => p.OrgID == 376 && p.CustomData.OfType<StringListCustomDataElement>().Any(e => e.StringListItems.Any(i => !i.StringValue.Contains("collection"))))
	.Select(p => new { p.ID, p.UniqueKey, p.FirstPrimaryUserID, p.Client.Fullname, p.CustomData })

CustomData.Where(cd => cd.Key.Contains("stringlist"))
CustomData.OfType<StringListCustomDataElement>()
CustomDataStringListItems

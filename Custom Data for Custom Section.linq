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
</Query>

CustomData
	.Where(i => i.ProjectID == 169676 && i.CustomSection.InternalName == "trialdocs263")
	.Where(i => i.CustomSection != null && i.CustomField != null && i.CustomSection.IsHidden == false) //ignore data for deleted sections and fields
	.Include(i => i.CustomField)
	.Include(i => i.CustomSection)
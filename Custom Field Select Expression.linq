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

CustomFields
	.Where(c => 
		c.CustomFieldType == Filevine.Common.CustomFieldType.ActionButton 
		&& !c.IsObsolete
		&& c.CustomActionButtonType == Filevine.Common.CustomActionButtonType.StaticLink)
	.Select(c => new { c.ID, c.Name, c.CustomFieldType, c.CustomActionButtonType, c.CustomFieldDetailsJson })
	

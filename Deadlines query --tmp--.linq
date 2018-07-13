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

Deadlines.Where(d => d.DueDate.HasValue && (!d.DoneDate.HasValue || false))
	.Select(d => new
	{
		Type = "Deadline",
		ID = d.ID,
		Title = d.Name,
		DoneDate = d.DoneDate,
		DueDate = d.DueDate,
		Notes = d.Notes
	})
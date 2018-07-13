<Query Kind="Program">
  <Connection>
    <ID>8c0d8637-4421-4069-88a7-8628df5aae60</ID>
    <Persist>true</Persist>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <Database>EFManyToMany.Data.FilevineContext</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <NuGetReference>AutoMapper</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>AutoMapper</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	this.Audits.Count().Dump();
	
	var items = this.Audits
		.Select(a => CloneAuditLogItem(a))
		.ToList();
		
	this.Audits.InsertAllOnSubmit(items);

	this.SubmitChanges();
	
	this.Audits.Count().Dump();
}

// Define other methods and classes here
public Audit CloneAuditLogItem(Audit audit) 
{
	var json = JsonConvert.SerializeObject(audit,
						Newtonsoft.Json.Formatting.Indented,
						new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
	var clone = JsonConvert.DeserializeObject<Audit>(json);
	clone.ID = 0;
	return clone;
}
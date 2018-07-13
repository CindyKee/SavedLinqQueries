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
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Linq.dll</Reference>
  <NuGetReference>AutoMapper</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>AutoMapper</Namespace>
  <Namespace>Filevine.Common.Cache.Interfaces</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Data.Linq</Namespace>
  <Namespace>System.Data.Linq.Mapping</Namespace>
</Query>

void Main()
{
	const int BatchSize = 100000;
	
	// Config AutoMapper
	Mapper.Reset();
	Mapper.Initialize(cfg =>
	{
		cfg.CreateMap<Audit, AuditLogItem>(MemberList.Source)
			.ForMember(dest => dest.ID, opt => opt.Ignore());
	});
	
	Mapper.AssertConfigurationIsValid();

	// Here is our simulated queue
	var logItems = MockDequeueAll();
	
	// Our simulation of FlushLogItems() from Auditor.cs
	
	var db = new FilevineContext();
	db.Configuration.AutoDetectChangesEnabled = false;
	
	var stopwatch = new Stopwatch();
	stopwatch.Start();

	var batch = 1;
	var items = Mapper.Map<List<AuditLogItem>>(logItems.Take(BatchSize).ToList());
	
	while (items.Any())
	{
		db.AuditLogItems.AddRange(items);
		db.SaveChanges();
		db.Dispose();
		db = new FilevineContext();
		db.Configuration.AutoDetectChangesEnabled = false;
		
		items =  Mapper.Map<List<AuditLogItem>>(logItems.Skip(batch * BatchSize).Take(BatchSize).ToList());
		batch++;
	}
	
	stopwatch.Stop();
	Console.WriteLine($"Batch Size {BatchSize} finished in {stopwatch.ElapsedMilliseconds} ms.");
}

/*
**  This is how Linq-to-SQL Works!!!
*/
public List<Audit> MockDequeueAll()
{
	var db = new CloneContext();
	var audits = db.Audits.ToList();
	return audits;
}

[Database(Name = "EFManyToMany.Data.FilevineContext")]
public class CloneContext : DataContext
{
	public CloneContext() : base(@"data source=(localdb)\MSSqlLocalDb;initial catalog=EFManyToMany.Data.FilevineContext;persist security info=True;Integrated Security=True;Connect Timeout=30;Encrypt=False;")
	{ }
	
	public Table<Audit> Audits;
}

[Table]
public class Audit
{
	[Column( IsPrimaryKey = true, IsDbGenerated = true )]
	public int ID { get; set; }
	
	[Column]public int LogType { get; set; }
	[Column]public int UserID { get; set; }
	[Column]public string IpAddress { get; set; }
	[Column]public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
	[Column]public int Action { get; set; }
	[Column]public string ActionDetail { get; set; }
	[Column]public string AffectedTypeName { get; set; }
	[Column]public int? AffectedOrgID { get; set; }
	[Column]public int? AffectedProjectID { get; set; }
	[Column]public int? AffectedSectionID { get; set; }
	[Column]public Guid? AffectedCollectionItemGuid { get; set; }
	[Column]public int? AffectedFieldID { get; set; }
	[Column]public int? AffectedUserID { get; set; }
	[Column]public int? AffectedNoteID { get; set; }
	[Column]public int? AffectedCommentID { get; set; }
	[Column]public int? AffectedPersonID { get; set; }
	[Column]public int? AffectedDocID { get; set; }
	[Column]public int? AffectedOtherID { get; set; }
	[Column]public string AffectedOtherKey { get; set; }
	[Column]public string Summary { get; set; } //Human-readable text
	[Column]public string DetailsJson { get; set; }
	[Column]public long? ElapsedMilliseconds { get; set; }
}
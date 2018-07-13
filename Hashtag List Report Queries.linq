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
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Data.EF.dll</Reference>
  <Reference>C:\GitHub\filevine\Filevine\bin\Filevine.Services.dll</Reference>
  <Namespace>Filevine.Data</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Filevine.Services.Reports</Namespace>
  <Namespace>Filevine.Services.Reports.Base</Namespace>
  <Namespace>Filevine.Services.Reports.Base.ParameterDefinitions</Namespace>
  <Namespace>Filevine.Services.Reports.Interfaces</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners.ProjectListExtensions</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners.Resources</Namespace>
  <Namespace>Filevine.Common.ClientObjects.ResponseObjects</Namespace>
  <Namespace>Filevine.Common.Cache.Objects</Namespace>
</Query>

void Main()
{
	/////////////////////////////////////////////////////////
	const int userId = 1452;
	var contextProvider = new FilevineContextProvider();
	
	var savedReport = new SavedReport()
	{
		ID = 0,
		ReportID = 2,
		DisplayName = "",
		ColumnOptions = new List<SavedReportColumnOption>
		{
			new SavedReportColumnOption { FieldName = "org", ID = 0, IsHidden = false, Position = 0, SavedReportID = 0 },
			new SavedReportColumnOption { FieldName = "phase", ID = 0, IsHidden = false, Position = 1, SavedReportID = 0 },
		},
		Parameters = new List<SavedReportParameter>
		{
			new SavedReportParameter { FilterType = Filevine.Common.FilterType.Contains, Key = "Project.Name", Sequence = 1, Value = "Calvert" }
		}
	};

	var reportDate = DateTime.UtcNow;
	var tzOffset = 480;
	var vars = new ReportRunVariables(reportDate, tzOffset, userId);
	
	//var runner = new PhaseListReportRunner(userId, savedReport, contextProvider);
	//runner.Dump();
	
	// GetDataSource
	var queryable = Projects
		.Where(p => !p.IsArchived)
		.OrderByDescending(p => p.Hashtags.Count())
		.Take(5)
		.AsNoTracking();

	Console.WriteLine("All non-archived projects:");
	queryable.Count().Dump();
	//queryable.Dump();

	// Apply Project Parameters BEFORE Grouping!
	queryable = queryable.Where(p => p.FirstPrimaryUserID == 1452);
	Console.WriteLine("Projects filtered by FirstPrimaryUserID (1452):");
	queryable.Count().Dump();
	//queryable.Dump();

	// From method GroupBySelectAvailableColumns()
	var groupedBy = queryable
		.SelectMany(p => p.Hashtags, (project, hashtag) => new { project, hashtag.Hashtag } )
		.GroupBy(p => new OrgGroupingKey { Org = p.project.Org, Name = p.Hashtag })
//		;
		.Select(g => new HashtagGroupingForReport
		{
			OrgID = g.Key.Org.ID,
			OrgName = g.Key.Org.Name,
			Hashtag = g.Key.Name,
			ProjectsCount = g.Count(),
			Projects = g.Select(q => q.project).AsQueryable()
		});


	Console.WriteLine("Grouped by Org and Hashtag:");
	groupedBy.Count().Dump();
	groupedBy.Dump();


	Console.WriteLine("Grouped by results filtered by Hashtag = #a-test:");
	groupedBy = groupedBy.Where(data => data.Hashtag.Equals("#a-test"));
	groupedBy = groupedBy.Where(data => data.ProjectsCount >= 2);
	groupedBy.Count().Dump();
	groupedBy.Dump();

	/*
	*/
}

// Define other methods and classes here

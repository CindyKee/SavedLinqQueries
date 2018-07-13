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
  <Namespace>Filevine.Common.Cache.Objects</Namespace>
  <Namespace>Filevine.Common.ClientObjects.ResponseObjects</Namespace>
  <Namespace>Filevine.Data</Namespace>
  <Namespace>Filevine.Domain.Entities</Namespace>
  <Namespace>Filevine.Services.Reports</Namespace>
  <Namespace>Filevine.Services.Reports.Base</Namespace>
  <Namespace>Filevine.Services.Reports.Base.ParameterDefinitions</Namespace>
  <Namespace>Filevine.Services.Reports.Interfaces</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners.ProjectListExtensions</Namespace>
  <Namespace>Filevine.Services.Reports.ReportRunners.Resources</Namespace>
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
		
	// GetDataSource
	var queryable = Projects
		.Where(p => !p.IsArchived)
		.OrderBy(p => p.CustomProjectType.Name)
//		.Take(10)
		.AsNoTracking();

	Console.WriteLine("All non-archived projects:");
	queryable.Count().Dump();
	//queryable.Dump();

	// Apply Parameters (Project and Org parameters)
	var orgList = new List<string> { "Handsome", "Filevine Dev" };
	queryable = queryable.Where(p => orgList.Contains(p.Org.Name));
	queryable = queryable.Where(p => p.Client.Fullname.Contains("Alice") || p.ProjectName.Contains("Alice"));
	Console.WriteLine("Projects filtered by Name:");
	queryable.Count().Dump();
	queryable.Dump();

	// From method GroupBySelectAvailableColumns()
	var groupedBy = queryable
		.GroupBy(p => new OrgGroupingKey 
		{ 
			Org = p.Org, 
			Name = p.CustomProjectType.Name 
		})
		.Select(g => new ProjectTypeGroupingForReport
		{
			OrgID = g.Key.Org.ID,
			OrgName = g.Key.Org.Name,
			ProjectType = g.Key.Name,
			ProjectsCount = g.Count(),
			Projects = g.AsQueryable()
		});

	Console.WriteLine("Grouped by Org and Project Type:");
	groupedBy.Count().Dump();
	groupedBy.Dump();

	// Apply group by parameters here (Phase, Phase IN, and ProjectsCount)
	Console.WriteLine("Grouped by results filtered by ProjectType=Basic:");
	groupedBy = groupedBy.Where(data => data.ProjectType.Equals("Basic"));
	groupedBy.Count().Dump();
	groupedBy.Dump();
}

// Define other methods and classes here